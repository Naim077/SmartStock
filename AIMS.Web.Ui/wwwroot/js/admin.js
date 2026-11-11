/* SmartStock administration: local demo records, never authentication or money movement. */
(() => {
  'use strict';
  const SS = window.SS;
  if (!SS || !['suppliers', 'customers', 'users', 'audit-log', 'settings'].includes(SS.page)) return;

  SS.ready(() => {
    const root = document.querySelector(`[data-admin-page="${SS.page}"]`);
    if (!root || root.dataset.adminBound) return;
    root.dataset.adminBound = 'true';

    const e = SS.escape;
    const roles = ['Owner', 'Manager', 'Sales Staff', 'Storekeeper'];
    const tones = ['green', 'blue', 'purple', 'amber', 'rose'];
    const supplierPage = SS.page === 'suppliers';
    const directoryPage = supplierPage || SS.page === 'customers';
    const kind = supplierPage ? 'supplier' : 'customer';
    const listKey = supplierPage ? 'suppliers' : 'customers';
    const moduleName = supplierPage ? 'Suppliers' : 'Customers';
    const balanceKey = supplierPage ? 'payable' : 'due';
    const view = {
      query: new URLSearchParams(window.location.search).get('q') || '',
      status: 'all', balance: 'all', role: 'all', module: 'all', action: 'all', from: '', to: '',
      page: 1, tab: SS.page === 'settings' ? 'business' : 'team', dirty: new Set()
    };

    function textAt(selector, value) {
      root.querySelectorAll(selector).forEach(element => { element.textContent = value; });
    }

    function stat(name, value) { textAt(`[data-stat="${name}"]`, value); }

    function avatar(name, index = 0) {
      return `<span class="avatar tone-${tones[index % tones.length]}" aria-hidden="true">${e(SS.initials(name))}</span>`;
    }

    function actionButton(action, id, icon, label, unavailable = '') {
      return `<button type="button" class="icon-button" data-admin-action="${action}" data-id="${e(id)}" title="${e(unavailable || label)}" aria-label="${e(unavailable || label)}"${unavailable ? ' disabled' : ''}>${SS.icon(icon)}</button>`;
    }

    function inputField(label, name, value, attributes = '', help = '') {
      return `<div class="field"><label for="admin-${name}">${e(label)}</label><input id="admin-${name}" name="${name}" value="${e(value)}" ${attributes}${help ? ` aria-describedby="admin-${name}-help"` : ''}>${help ? `<small id="admin-${name}-help" class="admin-help">${e(help)}</small>` : ''}</div>`;
    }

    function readText(data, name, label, max = 100, required = false) {
      const value = String(data.get(name) || '').trim();
      if (required && !value) throw new Error(`${label} is required.`);
      if (value.length > max) throw new Error(`${label} must be ${max} characters or fewer.`);
      return value;
    }

    function readAmount(data, name, label, { min = 0, max = 1000000000, integer = false } = {}) {
      const raw = String(data.get(name) ?? '').trim();
      const value = Number(raw);
      if (!raw || !Number.isFinite(value) || value < min || value > max || (integer && !Number.isInteger(value))) {
        throw new Error(`${label} must be ${integer ? 'a whole number ' : ''}between ${SS.number(min, 2)} and ${SS.number(max, 2)}.`);
      }
      if (!integer && Math.abs(SS.round(value) - value) > 0.000001) throw new Error(`${label} can have at most two decimal places.`);
      return integer ? value : SS.round(value);
    }

    function readContact(data, emailRequired = false) {
      const email = readText(data, 'email', 'Email address', 160, emailRequired);
      const phone = readText(data, 'phone', 'Phone number', 30);
      if (email && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) throw new Error('Enter a valid email address.');
      if (phone && (!/^[+\d ().-]+$/.test(phone) || phone.replace(/\D/g, '').length < 7 || phone.replace(/\D/g, '').length > 20)) {
        throw new Error('Enter a phone number with 7 to 20 digits; spaces, +, brackets and dashes are allowed.');
      }
      return { email, phone };
    }

    function checkDuplicateEmail(rows, email, id) {
      if (email && rows.some(row => row.id !== id && String(row.email).toLowerCase() === email.toLowerCase())) {
        throw new Error('This email address already belongs to another profile, including archived or inactive profiles.');
      }
    }

    function recordDate(value) {
      const date = new Date(`${value}T00:00:00Z`);
      if (!/^\d{4}-\d{2}-\d{2}$/.test(value) || !Number.isFinite(date.getTime()) || date.toISOString().slice(0, 10) !== value || value < '2000-01-01' || value > SS.today()) {
        throw new Error('Choose a valid payment date between 1 January 2000 and today.');
      }
      return value;
    }

    function pageRows(rows, noun) {
      const pageSize = SS.page === 'audit-log' ? 10 : 8;
      const pages = Math.max(1, Math.ceil(rows.length / pageSize));
      view.page = Math.min(Math.max(1, view.page), pages);
      const start = (view.page - 1) * pageSize;
      textAt('[data-admin-summary]', rows.length ? `Showing ${start + 1}-${Math.min(start + pageSize, rows.length)} of ${SS.number(rows.length)} ${noun}` : `No ${noun} match these filters`);
      const pagination = root.querySelector('[data-admin-pagination]');
      if (pagination) pagination.innerHTML = `<button type="button" class="icon-button" data-admin-action="previous" aria-label="Previous page"${view.page === 1 ? ' disabled' : ''}>${SS.icon('chevron-left')}</button><span>Page ${view.page} of ${pages}</span><button type="button" class="icon-button" data-admin-action="next" aria-label="Next page"${view.page === pages ? ' disabled' : ''}>${SS.icon('chevron-right')}</button>`;
      return rows.slice(start, start + pageSize);
    }

    function directoryRows() {
      const query = view.query.trim().toLowerCase();
      return SS.state[listKey].filter(row => {
        const matchesQuery = [row.id, row.name, row.contact, row.email, row.phone, row.address].join(' ').toLowerCase().includes(query);
        return matchesQuery && (view.status === 'all' || row.status === view.status)
          && (view.balance === 'all' || (view.balance === 'owed' ? Number(row[balanceKey]) > 0 : Number(row[balanceKey]) <= 0));
      }).sort((a, b) => Number(b.id === 'C001') - Number(a.id === 'C001') || a.name.localeCompare(b.name));
    }

    function renderDirectory() {
      const rows = SS.state[listKey];
      const active = rows.filter(row => row.status === 'active');
      stat('total', SS.number(rows.length));
      stat('active', SS.number(active.length));
      stat('balance', SS.money(rows.reduce((sum, row) => sum + Number(row[balanceKey] || 0), 0)));
      stat('extra', supplierPage
        ? `${SS.number(active.length ? active.reduce((sum, row) => sum + Number(row.leadTime || 0), 0) / active.length : 0, 1)} days`
        : SS.money(active.reduce((sum, row) => sum + Number(row.creditLimit || 0), 0)));
      const visible = pageRows(directoryRows(), listKey);
      root.querySelector('[data-admin-rows]').innerHTML = visible.length ? visible.map((row, index) => {
        const walkIn = row.id === 'C001';
        const balance = Number(row[balanceKey] || 0);
        const protectedReason = walkIn ? 'The walk-in customer is a protected system profile.' : '';
        return `<tr>
          <td><div class="item-cell">${avatar(row.name, index)}<span class="admin-table-name"><span class="cell-title">${e(row.name)}</span><span class="cell-subtitle">${e(row.id)}${walkIn ? ' / Protected walk-in' : supplierPage ? ` / ${e(row.contact || 'No contact person')}` : ` / ${e(row.address || 'No address added')}`}</span></span></div></td>
          <td><span class="cell-title">${e(row.email || 'No email added')}</span><span class="cell-subtitle">${e(row.phone || 'No phone added')}</span></td>
          <td>${supplierPage ? `${SS.number(row.leadTime)} days` : SS.money(row.creditLimit)}</td>
          <td class="text-right"><span class="font-medium ${balance > 0 ? 'text-danger' : 'text-success'}">${SS.money(balance)}</span><span class="cell-subtitle">${balance > 0 ? supplierPage ? 'To pay' : 'To collect' : 'Settled'}</span></td>
          <td>${SS.badge(row.status)}</td>
          <td><div class="row-actions">${actionButton('details', row.id, 'eye', `View ${row.name}`)}${actionButton('edit', row.id, 'edit', `Edit ${row.name}`, protectedReason)}${actionButton('payment', row.id, 'credit-card', `Record local payment for ${row.name}`, protectedReason || (balance <= 0 ? `No outstanding balance for ${row.name}.` : ''))}${actionButton('status', row.id, row.status === 'active' ? 'minus' : 'refresh', `${row.status === 'active' ? 'Archive' : 'Reactivate'} ${row.name}`, protectedReason)}</div></td>
        </tr>`;
      }).join('') : SS.emptyRow(6, 'Try a different name, contact, status or balance filter.');
    }

    function editDirectory(id) {
      const existing = id ? SS.state[listKey].find(row => row.id === id) : null;
      if (id && !existing) throw new Error('This profile no longer exists.');
      if (id === 'C001') throw new Error('The walk-in customer cannot be edited.');
      const row = existing || { name: '', contact: '', email: '', phone: '', address: '', leadTime: 3, creditLimit: 15000, openingDue: 0, payable: 0 };
      SS.openModal({
        title: `${existing ? 'Edit' : 'Add'} ${kind}`, wide: true, submitLabel: existing ? 'Save profile' : `Add ${kind}`,
        body: `<div class="admin-dialog stack"><p class="text-muted">Keep contact details and local balances together. Use sample details, not sensitive information.</p><div class="form-grid">
          ${inputField(supplierPage ? 'Supplier / business name' : 'Customer / business name', 'name', row.name, 'type="text" required maxlength="100" autocomplete="off"')}
          ${supplierPage ? inputField('Contact person', 'contact', row.contact, 'type="text" required maxlength="100" autocomplete="off"') : inputField('Credit limit (BDT)', 'creditLimit', row.creditLimit, 'type="number" required min="0" max="1000000000" step="0.01"', 'Must cover the current outstanding balance.')}
          ${inputField('Email address', 'email', row.email, 'type="email" maxlength="160" placeholder="contact@business.example" autocomplete="off"')}
          ${inputField('Phone number', 'phone', row.phone, 'type="tel" maxlength="30" placeholder="01700 000000" autocomplete="off"')}
          <div class="field full"><label for="admin-address">Address</label><textarea id="admin-address" name="address" rows="2" maxlength="240">${e(row.address)}</textarea></div>
          ${supplierPage ? inputField('Lead time (days)', 'leadTime', row.leadTime, 'type="number" required min="0" max="365" step="1"') : ''}
          ${!existing ? inputField(supplierPage ? 'Opening payable (BDT)' : 'Opening due (BDT)', 'openingBalance', 0, 'type="number" required min="0" max="1000000000" step="0.01"', 'An explicit starting balance for this local demo.') : ''}
        </div>${existing ? `<div class="notice notice-info">Current ${supplierPage ? 'payable' : 'due'}: <strong>${SS.money(row[balanceKey])}</strong>. Balances are not editable here. Use Record local payment to settle them.</div>` : '<div class="notice notice-info">This creates a local directory profile only. No invitation or message is sent.</div>'}</div>`,
        onSubmit(data) {
          const name = readText(data, 'name', 'Name', 100, true);
          const contact = readContact(data);
          const address = readText(data, 'address', 'Address', 240);
          const fields = supplierPage
            ? { contact: readText(data, 'contact', 'Contact person', 100, true), leadTime: readAmount(data, 'leadTime', 'Lead time', { max: 365, integer: true }) }
            : { creditLimit: readAmount(data, 'creditLimit', 'Credit limit') };
          const opening = existing ? 0 : readAmount(data, 'openingBalance', 'Opening balance');
          SS.update(state => {
            const current = id ? state[listKey].find(item => item.id === id) : null;
            if (id && !current) throw new Error('This profile was removed. Close the dialog and try again.');
            checkDuplicateEmail(state[listKey], contact.email, id);
            if (!supplierPage && fields.creditLimit < Number(current ? current.due : opening)) throw new Error('The credit limit cannot be lower than the outstanding balance.');
            if (current) Object.assign(current, { name, ...contact, address, ...fields });
            else state[listKey].push({ id: SS.uid(supplierPage ? 'S' : 'C'), name, ...contact, address, ...fields, status: 'active', ...(supplierPage ? { payable: opening } : { due: opening, openingDue: opening }) });
          }, existing ? 'Profile updated' : 'Profile added', moduleName, `${name}${existing ? ': contact profile updated.' : `: local profile created with ${SS.money(opening)} opening balance.`}`);
          SS.toast(`${supplierPage ? 'Supplier' : 'Customer'} profile ${existing ? 'updated' : 'added'} locally.`);
        }
      });
    }

    function changeDirectoryStatus(id) {
      const row = SS.state[listKey].find(item => item.id === id);
      if (!row) throw new Error('This profile no longer exists.');
      if (id === 'C001') throw new Error('The walk-in customer cannot be archived.');
      const archive = row.status === 'active';
      SS.confirm(archive
        ? `Archive ${row.name}? The profile will be excluded from new transactions. Existing history and the ${SS.money(row[balanceKey])} balance are retained; local payments can still settle it.`
        : `Reactivate ${row.name}? The profile will be available for new transactions again.`, () => {
        SS.update(state => {
          const current = state[listKey].find(item => item.id === id);
          if (!current || current.status !== row.status) throw new Error('This profile changed. Close this dialog and review its current status.');
          current.status = archive ? 'archived' : 'active';
        }, archive ? 'Profile archived' : 'Profile reactivated', moduleName, row.name);
        SS.toast(`${row.name} ${archive ? 'archived' : 'reactivated'} locally.`);
      }, { title: `${archive ? 'Archive' : 'Reactivate'} ${kind}`, label: archive ? 'Archive profile' : 'Reactivate profile', danger: archive });
    }

    function recordPayment(id) {
      const row = SS.state[listKey].find(item => item.id === id);
      if (!row) throw new Error('This profile no longer exists.');
      if (id === 'C001') throw new Error('Payments are unavailable for the protected walk-in customer.');
      if (Number(row[balanceKey]) <= 0) throw new Error('There is no outstanding balance to settle.');
      SS.openModal({
        title: supplierPage ? 'Record supplier payment' : 'Record customer payment', submitLabel: 'Record local payment',
        body: `<div class="admin-dialog stack"><div class="notice notice-warning"><strong>Local bookkeeping only</strong><p>No money is transferred, charged or requested. Do not enter bank details or card numbers.</p></div><div class="summary-row"><span>${e(row.name)}</span><strong>${SS.money(row[balanceKey])} outstanding</strong></div><div class="form-grid">
          ${inputField('Amount (BDT)', 'amount', '', `type="number" required min="0.01" max="${Number(row[balanceKey])}" step="0.01" inputmode="decimal"`)}
          ${inputField('Payment date', 'date', SS.today(), `type="date" required min="2000-01-01" max="${SS.today()}"`)}
          <div class="field"><label for="admin-method">Recorded method</label><select id="admin-method" name="method"><option>Cash</option><option>Bank transfer</option><option>Card</option></select></div>
          ${inputField('Reference (optional)', 'reference', '', 'type="text" maxlength="80" placeholder="Counter receipt reference" autocomplete="off"')}
          <div class="field full"><label for="admin-note">Note (optional)</label><textarea id="admin-note" name="note" maxlength="300" rows="2" placeholder="A short bookkeeping note, without financial credentials"></textarea></div>
        </div><p class="admin-help">${supplierPage ? 'This reduces the supplier payable. It does not mark a purchase order as paid.' : 'Applied to the oldest outstanding invoices first, after return credits. Any remainder reduces the opening due.'}</p></div>`,
        onSubmit(data) {
          const amount = readAmount(data, 'amount', 'Payment amount', { min: 0.01 });
          const date = recordDate(String(data.get('date') || ''));
          const method = String(data.get('method'));
          if (!['Cash', 'Bank transfer', 'Card'].includes(method)) throw new Error('Choose a valid recorded payment method.');
          const reference = readText(data, 'reference', 'Reference', 80);
          const note = readText(data, 'note', 'Note', 300);
          let number = '';
          SS.update(state => {
            const current = state[listKey].find(item => item.id === id);
            if (!current || amount > SS.round(current[balanceKey])) throw new Error('The outstanding balance changed. The amount cannot exceed the current balance.');
            const allocations = [];
            let openingAmount = 0;
            if (supplierPage) current.payable = SS.round(current.payable - amount);
            else {
              let remaining = amount;
              const invoices = state.invoices.filter(invoice => invoice.customerId === id)
                .sort((a, b) => a.date.localeCompare(b.date) || a.number.localeCompare(b.number, undefined, { numeric: true }));
              for (const invoice of invoices) {
                const applied = Math.min(remaining, SS.invoiceDue(invoice));
                if (applied <= 0) continue;
                invoice.paid = SS.round(Number(invoice.paid) + applied);
                invoice.status = SS.invoiceDue(invoice) > 0 ? 'partial' : 'paid';
                remaining = SS.round(remaining - applied);
                allocations.push({ invoiceId: invoice.id, amount: applied });
                if (remaining === 0) break;
              }
              if (remaining > Number(current.openingDue || 0)) throw new Error('The invoice and opening balances do not match this customer balance. Review the local records before recording a payment.');
              openingAmount = remaining;
              current.openingDue = SS.round(Number(current.openingDue || 0) - remaining);
              current.due = SS.round(invoices.reduce((sum, invoice) => sum + SS.invoiceDue(invoice), 0) + current.openingDue);
            }
            number = SS.nextNumber(state, 'payment');
            state.payments.push({ id: SS.uid('payment'), number, type: kind, [supplierPage ? 'supplierId' : 'customerId']: id, date, amount, method, reference, note, allocations, openingAmount });
          }, 'Local payment recorded', moduleName, `${row.name}: ${SS.money(amount)} via ${method}. Local bookkeeping only; no money moved.`);
          SS.toast(`${number} recorded locally. No money was moved.`);
        }
      });
    }

    function showDirectoryDetails(id) {
      const row = SS.state[listKey].find(item => item.id === id);
      if (!row) throw new Error('This profile no longer exists.');
      const documents = (supplierPage ? SS.state.purchaseOrders : SS.state.invoices)
        .filter(item => item[supplierPage ? 'supplierId' : 'customerId'] === id)
        .sort((a, b) => b.date.localeCompare(a.date));
      const payments = SS.state.payments.filter(item => item[supplierPage ? 'supplierId' : 'customerId'] === id).sort((a, b) => b.date.localeCompare(a.date));
      const documentRows = documents.map(item => `<tr><td class="font-medium">${e(item.number)}</td><td>${e(SS.date(item.date))}</td><td>${SS.badge(item.status)}</td><td class="text-right">${SS.money(supplierPage ? SS.orderTotal(item) : item.total)}</td><td class="text-right">${SS.money(supplierPage ? item.paid : SS.invoiceDue(item))}</td></tr>`).join('');
      const paymentRows = payments.slice(0, 8).map(item => `<tr><td><span class="cell-title">${e(item.number)}</span><span class="cell-subtitle">${e(item.reference || 'No reference')}</span></td><td>${e(SS.date(item.date))}</td><td>${e(item.method)}<span class="cell-subtitle">${e(item.note || 'Local record')}</span></td><td class="text-right">${SS.money(item.amount)}</td></tr>`).join('');
      SS.openModal({ title: supplierPage ? 'Supplier details' : 'Customer details', wide: true,
        body: `<div class="admin-dialog stack"><div class="admin-profile-heading"><div class="item-cell">${avatar(row.name)}<div><h3>${e(row.name)}</h3><p class="text-muted">${e(row.id)}${id === 'C001' ? ' / Protected walk-in profile' : ''}</p></div></div>${SS.badge(row.status)}</div>
        <div class="admin-detail-grid"><dl class="admin-details"><dt>${supplierPage ? 'Contact person' : 'Credit limit'}</dt><dd>${supplierPage ? e(row.contact || 'Not added') : SS.money(row.creditLimit)}</dd><dt>Email</dt><dd>${e(row.email || 'Not added')}</dd><dt>Phone</dt><dd>${e(row.phone || 'Not added')}</dd><dt>Address</dt><dd>${e(row.address || 'Not added')}</dd></dl><div class="admin-balance-panel"><span class="stat-label">${supplierPage ? 'Outstanding payable' : 'Outstanding due'}</span><strong class="stat-value">${SS.money(row[balanceKey])}</strong><p class="admin-help">${supplierPage ? `${SS.number(row.leadTime)}-day lead time. Purchase order prepayments are separate from directory payments.` : `${SS.money(row.openingDue || 0)} opening due included. Invoice balances account for return credits.`}</p></div></div>
        <section><div class="admin-section-heading"><h3>Related ${supplierPage ? 'purchase orders' : 'invoices'}</h3><span class="text-muted">${SS.number(documents.length)} records</span></div><div class="table-wrap" role="region" aria-label="Related ${supplierPage ? 'purchase orders' : 'invoices'}" tabindex="0"><table class="data-table"><thead><tr><th scope="col">Reference</th><th scope="col">Date</th><th scope="col">Status</th><th scope="col" class="text-right">Total</th><th scope="col" class="text-right">${supplierPage ? 'PO prepayment' : 'Net due'}</th></tr></thead><tbody>${documentRows || SS.emptyRow(5, `No ${supplierPage ? 'purchase orders' : 'invoices'} for this profile yet.`)}</tbody></table></div></section>
        <section><div class="admin-section-heading"><h3>Local payment history</h3><span class="text-muted">${payments.length > 8 ? 'Latest 8 of ' : ''}${SS.number(payments.length)} records</span></div><div class="table-wrap" role="region" aria-label="Local payment history" tabindex="0"><table class="data-table"><thead><tr><th scope="col">Record</th><th scope="col">Date</th><th scope="col">Method / note</th><th scope="col" class="text-right">Amount</th></tr></thead><tbody>${paymentRows || SS.emptyRow(4, 'No local payments recorded for this profile.')}</tbody></table></div></section><p class="admin-help">Browser-local records only. These entries are not bank statements or payment confirmations.</p></div>`
      });
    }

    function userProtection(user, state = SS.state) {
      if (user.id === 'U001') return 'The current Owner profile cannot be deactivated or demoted.';
      if (user.role === 'Owner' && user.status === 'active' && state.users.filter(item => item.role === 'Owner' && item.status === 'active').length <= 1) return 'Keep at least one active Owner profile.';
      return '';
    }

    function userRows() {
      const query = view.query.trim().toLowerCase();
      return SS.state.users.filter(user => [user.name, user.email, user.role].join(' ').toLowerCase().includes(query)
        && (view.status === 'all' || user.status === view.status) && (view.role === 'all' || user.role === view.role));
    }

    function renderUsers() {
      const users = SS.state.users;
      stat('total', SS.number(users.length));
      stat('active', SS.number(users.filter(user => user.status === 'active').length));
      stat('owners', SS.number(users.filter(user => user.role === 'Owner' && user.status === 'active').length));
      stat('roles', SS.number(new Set(users.map(user => user.role)).size));
      roles.forEach(role => textAt(`[data-role-count="${role}"]`, `${users.filter(user => user.role === role && user.status === 'active').length} active`));
      const rows = pageRows(userRows(), 'profiles');
      root.querySelector('[data-admin-rows]').innerHTML = rows.length ? rows.map((user, index) => `<tr><td><div class="item-cell">${avatar(user.name, index)}<span class="admin-table-name"><span class="cell-title">${e(user.name)}${user.id === 'U001' ? ' <span class="admin-you">You</span>' : ''}</span><span class="cell-subtitle">${e(user.email)}</span></span></div></td><td><span class="admin-role-label">${e(user.role)}</span></td><td>${SS.badge(user.status)}</td><td><span class="cell-subtitle">${e(user.lastActive || 'No sign-in tracking in this demo')}</span></td><td><div class="row-actions">${actionButton('edit-user', user.id, 'edit', `Edit ${user.name}`)}${actionButton('user-status', user.id, user.status === 'active' ? 'minus' : 'refresh', `${user.status === 'active' ? 'Deactivate' : 'Reactivate'} ${user.name}`, user.status === 'active' ? userProtection(user) : '')}</div></td></tr>`).join('') : SS.emptyRow(5, 'Try another profile name, email, role or status.');
      renderTabs();
    }

    function editUser(id) {
      const existing = id ? SS.state.users.find(user => user.id === id) : null;
      if (id && !existing) throw new Error('This profile no longer exists.');
      const user = existing || { name: '', email: '', role: 'Sales Staff' };
      const protectedRole = existing ? userProtection(existing) : '';
      SS.openModal({ title: existing ? 'Edit demo profile' : 'Add demo profile', submitLabel: existing ? 'Save profile' : 'Add local profile',
        body: `<div class="admin-dialog stack"><div class="notice notice-info"><strong>A profile, not an account</strong><p>No password, invitation, sign-in or server permission is created. Use sample information only.</p></div><div class="form-grid">${inputField('Full name', 'name', user.name, 'type="text" required maxlength="100" autocomplete="off"')}${inputField('Email address', 'email', user.email, 'type="email" required maxlength="160" placeholder="name@workspace.example" autocomplete="off"')}<div class="field full"><label for="admin-role">Demo role</label><select id="admin-role" name="role"${protectedRole ? ' disabled aria-describedby="admin-role-help"' : ''}>${roles.map(role => `<option${user.role === role ? ' selected' : ''}>${e(role)}</option>`).join('')}</select>${protectedRole ? `<small id="admin-role-help" class="admin-help">${e(protectedRole)}</small>` : '<small class="admin-help">Role labels describe the proposed access model only. They do not restrict this demo.</small>'}</div></div></div>`,
        onSubmit(data) {
          const name = readText(data, 'name', 'Full name', 100, true);
          const email = readContact(data, true).email;
          const role = protectedRole ? user.role : String(data.get('role'));
          if (!roles.includes(role)) throw new Error('Select a valid demo role.');
          SS.update(state => {
            const current = id ? state.users.find(item => item.id === id) : null;
            if (id && !current) throw new Error('This profile was removed. Close the dialog and try again.');
            checkDuplicateEmail(state.users, email, id);
            if (current) {
              if (role !== 'Owner' && userProtection(current, state)) throw new Error(userProtection(current, state));
              Object.assign(current, { name, email, role });
              if (id === 'U001') state.settings.ownerName = name;
            } else state.users.push({ id: SS.uid('user'), name, email, role, status: 'active', lastActive: 'No sign-in tracking (demo)' });
          }, existing ? 'Demo profile updated' : 'Demo profile added', 'Users', `${name}: ${role}. Role label only; no authentication or server permissions changed.`);
          SS.toast(`Demo profile ${existing ? 'saved' : 'added'} locally. No invitation sent.`);
        }
      });
    }

    function changeUserStatus(id) {
      const user = SS.state.users.find(item => item.id === id);
      if (!user) throw new Error('This profile no longer exists.');
      const deactivate = user.status === 'active';
      if (deactivate && userProtection(user)) throw new Error(userProtection(user));
      SS.confirm(`${deactivate ? 'Deactivate' : 'Reactivate'} ${user.name}'s demo profile? This changes a local status label only. It does not revoke or grant access to a server.`, () => {
        SS.update(state => {
          const current = state.users.find(item => item.id === id);
          if (!current || current.status !== user.status) throw new Error('This profile changed. Close the dialog and review its status.');
          if (deactivate && userProtection(current, state)) throw new Error(userProtection(current, state));
          current.status = deactivate ? 'inactive' : 'active';
        }, deactivate ? 'Demo profile deactivated' : 'Demo profile reactivated', 'Users', user.name);
        SS.toast(`Demo profile ${deactivate ? 'deactivated' : 'reactivated'} locally.`);
      }, { title: deactivate ? 'Deactivate demo profile' : 'Reactivate demo profile', label: deactivate ? 'Deactivate profile' : 'Reactivate profile', danger: deactivate });
    }

    function auditDay(value) {
      const date = new Date(value);
      return Number.isFinite(date.getTime()) ? date.toLocaleDateString('en-CA', { timeZone: SS.state.settings.timezone || 'Asia/Dhaka' }) : '';
    }

    function timestamp(value) {
      const date = new Date(value);
      return Number.isFinite(date.getTime()) ? date.toLocaleString('en-GB', { day: 'numeric', month: 'short', year: 'numeric', hour: '2-digit', minute: '2-digit', timeZone: SS.state.settings.timezone || 'Asia/Dhaka' }) : 'Date unavailable';
    }

    function auditRows() {
      const query = view.query.trim().toLowerCase();
      if (view.from && view.to && view.from > view.to) return [];
      return SS.state.audit.filter(entry => {
        const date = auditDay(entry.date);
        return [entry.actor, entry.action, entry.module, entry.detail].join(' ').toLowerCase().includes(query)
          && (view.module === 'all' || entry.module === view.module) && (view.action === 'all' || entry.action === view.action)
          && (!view.from || date >= view.from) && (!view.to || date <= view.to);
      }).sort((a, b) => new Date(b.date) - new Date(a.date));
    }

    function renderAudit() {
      const entries = SS.state.audit;
      stat('total', SS.number(entries.length));
      stat('today', SS.number(entries.filter(entry => auditDay(entry.date) === auditDay(new Date())).length));
      stat('modules', SS.number(new Set(entries.map(entry => entry.module)).size));
      ['module', 'action'].forEach(key => {
        const select = root.querySelector(`[data-admin-filter="${key}"]`);
        const options = Array.from(new Set(entries.map(entry => entry[key]))).sort();
        if (!options.includes(view[key])) view[key] = 'all';
        select.innerHTML = `<option value="all">All ${key === 'module' ? 'modules' : 'actions'}</option>${options.map(value => `<option value="${e(value)}"${view[key] === value ? ' selected' : ''}>${e(value)}</option>`).join('')}`;
      });
      const invalid = Boolean(view.from && view.to && view.from > view.to);
      root.querySelector('[data-audit-range-error]').hidden = !invalid;
      const entriesOnPage = pageRows(auditRows(), 'events');
      root.querySelector('[data-admin-rows]').innerHTML = entriesOnPage.length ? entriesOnPage.map((entry, index) => `<tr><td><time class="admin-timestamp" datetime="${e(entry.date)}">${e(timestamp(entry.date))}</time></td><td><div class="item-cell">${avatar(entry.actor, index)}<span class="cell-title">${e(entry.actor)}</span></div></td><td>${SS.badge('info', entry.module)}</td><td class="font-medium">${e(entry.action)}</td><td><span class="admin-log-excerpt">${e(entry.detail)}</span></td><td>${actionButton('audit-detail', entry.id, 'eye', `View ${entry.action} event details`)}</td></tr>`).join('') : SS.emptyRow(6, invalid ? 'The start date must be on or before the end date.' : 'No local events match this search and date range.');
      textAt('[data-audit-timezone]', `Dates and times shown in ${SS.state.settings.timezone || 'Asia/Dhaka'}.`);
    }

    function showAuditDetail(id) {
      const entry = SS.state.audit.find(item => item.id === id);
      if (!entry) throw new Error('This event is no longer available. The local demo may have been reset.');
      SS.openModal({ title: 'Audit event details', body: `<div class="admin-dialog stack"><dl class="admin-details"><dt>Event ID</dt><dd>${e(entry.id)}</dd><dt>Date and time</dt><dd>${e(timestamp(entry.date))} (${e(SS.state.settings.timezone)})</dd><dt>Actor label</dt><dd>${e(entry.actor)}</dd><dt>Module</dt><dd>${e(entry.module)}</dd><dt>Action</dt><dd>${e(entry.action)}</dd></dl><section><h3>Event detail</h3><p class="admin-full-detail">${e(entry.detail || 'No additional detail.')}</p></section><div class="notice notice-warning">This browser-local log is not tamper-proof. Actor names are demo labels, not verified identities.</div></div>` });
    }

    function renderTabs() {
      root.querySelectorAll('[data-admin-tab]').forEach(tab => {
        const selected = tab.dataset.adminTab === view.tab;
        tab.classList.toggle('is-active', selected);
        tab.setAttribute('aria-selected', String(selected));
        tab.tabIndex = selected ? 0 : -1;
      });
      root.querySelectorAll('[data-admin-panel]').forEach(panel => { panel.hidden = panel.dataset.adminPanel !== view.tab; });
    }

    function renderSettings() {
      const settings = SS.state.settings;
      root.querySelectorAll('[data-setting]').forEach(input => {
        if (input.form && view.dirty.has(input.form.id)) return;
        const value = settings[input.dataset.setting];
        if (input.type === 'checkbox') input.checked = Boolean(value);
        else input.value = value ?? '';
      });
      textAt('[data-settings-business]', settings.businessName);
      textAt('[data-settings-owner]', settings.ownerName);
      textAt('[data-settings-initials]', SS.initials(settings.businessName));
      textAt('[data-settings-email]', settings.email || 'No email added');
      textAt('[data-settings-address]', settings.address || 'No address added');
      textAt('[data-settings-updated]', `Last local change: ${timestamp(SS.state.meta.updatedAt)}`);
      root.querySelectorAll('[data-form-dirty]').forEach(element => { element.hidden = !view.dirty.has(element.dataset.formDirty); });
      renderTabs();
    }

    function saveSettings(form) {
      const error = form.querySelector('[data-settings-error]');
      error.hidden = true;
      const data = new FormData(form);
      try {
        let changes;
        if (form.id === 'admin-business-form') {
          changes = { businessName: readText(data, 'businessName', 'Business name', 100, true), ownerName: readText(data, 'ownerName', 'Owner display name', 100, true), ...readContact(data, true), address: readText(data, 'address', 'Business address', 240) };
        } else {
          const theme = String(data.get('theme'));
          if (!['light', 'dark'].includes(theme)) throw new Error('Choose Light or Dark for the workspace theme.');
          changes = { theme, expiryDays: readAmount(data, 'expiryDays', 'Expiry warning window', { min: 1, max: 365, integer: true }), lowStockEmails: data.get('lowStockEmails') === 'on', currency: 'BDT' };
        }
        SS.update(state => {
          Object.assign(state.settings, changes);
          if (changes.ownerName) {
            const owner = state.users.find(user => user.id === 'U001');
            if (owner) owner.name = changes.ownerName;
          }
        }, 'Settings saved', 'Settings', form.id === 'admin-business-form' ? 'Business details updated locally; shared workspace header refreshed.' : 'Theme, expiry warning window and email preference saved locally. No email service is connected. Currency remains BDT.');
        view.dirty.delete(form.id);
        renderSettings();
        form.querySelector('[data-settings-saved]').textContent = 'Saved in this browser.';
        SS.toast('Settings saved locally.');
      } catch (problem) {
        error.textContent = problem.message || 'Please review the form.';
        error.hidden = false;
        error.focus();
      }
    }

    function resetDemo() {
      SS.openModal({ title: 'Reset the local demo', danger: true, submitLabel: 'Reset local demo',
        body: `<div class="admin-dialog stack"><div class="notice notice-warning"><strong>This cannot be undone.</strong><p>All SmartStock demo changes in this browser, including products, invoices, profiles, payments, settings and audit events, will be replaced with the original sample data.</p></div><p>This affects only the local SmartStock demo. No actual files, documents, databases or external services are touched. Other tabs for this workspace will also receive the reset.</p>${inputField('Type RESET to confirm', 'confirmation', '', 'type="text" required pattern="RESET" autocomplete="off" spellcheck="false"')}</div>`,
        onSubmit(data) {
          if (String(data.get('confirmation')) !== 'RESET') throw new Error('Type RESET exactly to confirm.');
          view.dirty.clear();
          SS.reset();
          root.querySelectorAll('[data-settings-error]').forEach(element => { element.hidden = true; });
          root.querySelectorAll('[data-settings-saved]').forEach(element => { element.textContent = ''; });
        }
      });
    }

    function integrationInfo(name) {
      const descriptions = {
        email: ['Email notifications', 'No email provider or delivery worker is connected. The low-stock email switch remembers a preference only. It cannot send messages or invitations.'],
        payments: ['Payment services', 'No payment gateway is connected. Directory payments are bookkeeping records only. No card, bank or wallet is charged, and no transfers are made.'],
        accounting: ['Accounting sync', 'There is no external accounting sync. Export the filtered CSV files from the relevant pages when you need a local snapshot.'],
        operations: ['Jobs and backups', 'This static frontend has no scheduler, backup server or restore service. Job and backup statuses are Not configured, not health checks. Browser storage can be cleared by the browser and is not a secure backup.']
      };
      const content = descriptions[name];
      if (!content) return;
      SS.openModal({ title: content[0], body: `<div class="admin-dialog stack">${SS.badge('inactive', name === 'operations' ? 'Not configured' : 'Not connected')}<p>${e(content[1])}</p><div class="notice notice-info">No API keys, passwords or financial credentials are accepted or stored by this demo.</div></div>` });
    }

    function exportRows() {
      if (directoryPage) {
        const rows = directoryRows();
        if (!rows.length) return SS.toast('There are no matching profiles to export.', 'warning');
        SS.downloadCSV(`${listKey}-${SS.today()}.csv`, [
          supplierPage ? ['ID', 'Supplier', 'Contact', 'Email', 'Phone', 'Address', 'Lead time (days)', 'Payable (BDT)', 'Status'] : ['ID', 'Customer', 'Email', 'Phone', 'Address', 'Credit limit (BDT)', 'Due (BDT)', 'Opening due (BDT)', 'Status'],
          ...rows.map(row => supplierPage ? [row.id, row.name, row.contact, row.email, row.phone, row.address, row.leadTime, row.payable, row.status] : [row.id, row.name, row.email, row.phone, row.address, row.creditLimit, row.due, row.openingDue, row.status])
        ]);
      } else if (SS.page === 'users') {
        const rows = userRows();
        if (!rows.length) return SS.toast('There are no matching profiles to export.', 'warning');
        SS.downloadCSV(`demo-profiles-${SS.today()}.csv`, [['ID', 'Name', 'Email', 'Demo role (not enforced)', 'Local status'], ...rows.map(user => [user.id, user.name, user.email, user.role, user.status])]);
      } else if (SS.page === 'audit-log') {
        const rows = auditRows();
        if (!rows.length) return SS.toast('There are no matching audit events to export.', 'warning');
        SS.downloadCSV(`local-audit-${SS.today()}.csv`, [['Event ID', 'Timestamp (source)', 'Actor label', 'Module', 'Action', 'Detail'], ...rows.map(entry => [entry.id, entry.date, entry.actor, entry.module, entry.action, entry.detail])]);
      }
    }

    function render() {
      if (directoryPage) renderDirectory();
      else if (SS.page === 'users') renderUsers();
      else if (SS.page === 'audit-log') renderAudit();
      else renderSettings();
    }

    root.addEventListener('click', event => {
      const button = event.target.closest('[data-admin-action]');
      if (!button || button.disabled) return;
      const action = button.dataset.adminAction;
      const id = button.dataset.id;
      try {
        if (action === 'add') editDirectory();
        else if (action === 'edit') editDirectory(id);
        else if (action === 'details') showDirectoryDetails(id);
        else if (action === 'status') changeDirectoryStatus(id);
        else if (action === 'payment') recordPayment(id);
        else if (action === 'add-user') editUser();
        else if (action === 'edit-user') editUser(id);
        else if (action === 'user-status') changeUserStatus(id);
        else if (action === 'audit-detail') showAuditDetail(id);
        else if (action === 'export') exportRows();
        else if (action === 'previous' || action === 'next') {
          view.page += action === 'next' ? 1 : -1;
          render();
          const nextButton = root.querySelector(`[data-admin-action="${action}"]`);
          (nextButton?.disabled ? root.querySelector('[data-admin-pagination] button:not(:disabled)') : nextButton)?.focus();
        }
        else if (action === 'tab') { view.tab = button.dataset.adminTab; renderTabs(); }
        else if (action === 'clear-filters') {
          ['query', 'from', 'to'].forEach(key => { view[key] = ''; });
          ['status', 'balance', 'role', 'module', 'action'].forEach(key => { view[key] = 'all'; });
          root.querySelectorAll('[data-admin-filter]').forEach(input => { input.value = view[input.dataset.adminFilter]; });
          view.page = 1; render();
        } else if (action === 'discard-settings') {
          const form = root.querySelector(`#${button.dataset.form}`);
          view.dirty.delete(form.id);
          form.querySelector('[data-settings-error]').hidden = true;
          form.querySelector('[data-settings-saved]').textContent = 'Unsaved edits discarded.';
          renderSettings();
        } else if (action === 'reset-demo') resetDemo();
        else if (action === 'integration-info') integrationInfo(button.dataset.integration);
        if (document.getElementById('app-modal')?.open) view.returnFocus = { button, action, id };
      } catch (problem) { SS.toast(problem.message || 'This action is currently unavailable.', 'warning'); }
    });

    function handleInput(event) {
      const input = event.target;
      if (input.matches('[data-setting]') && input.form) {
        view.dirty.add(input.form.id);
        const indicator = input.form.querySelector('[data-form-dirty]');
        if (indicator) indicator.hidden = false;
        input.form.querySelector('[data-settings-saved]').textContent = '';
      }
      if (!input.matches('[data-admin-filter]')) return;
      if ((event.type === 'input' && input.tagName === 'SELECT') || (event.type === 'change' && input.tagName !== 'SELECT')) return;
      view[input.dataset.adminFilter] = input.value;
      view.page = 1;
      render();
    }
    root.addEventListener('input', handleInput);
    root.addEventListener('change', handleInput);
    root.addEventListener('submit', event => {
      if (!event.target.matches('[data-admin-settings-form]')) return;
      event.preventDefault();
      saveSettings(event.target);
    });
    root.addEventListener('keydown', event => {
      const tab = event.target.closest('[data-admin-tab]');
      if (!tab || !['ArrowLeft', 'ArrowRight', 'Home', 'End'].includes(event.key)) return;
      event.preventDefault();
      const tabs = Array.from(root.querySelectorAll('[data-admin-tab]'));
      const index = tabs.indexOf(tab);
      const next = event.key === 'Home' ? 0 : event.key === 'End' ? tabs.length - 1 : (index + (event.key === 'ArrowRight' ? 1 : -1) + tabs.length) % tabs.length;
      view.tab = tabs[next].dataset.adminTab;
      renderTabs();
      tabs[next].focus();
    });

    // Saving refreshes table rows, so the shared modal's original trigger may no longer exist.
    document.getElementById('app-modal')?.addEventListener('close', () => {
      const target = view.returnFocus;
      view.returnFocus = null;
      if (!target || target.button.isConnected) return;
      const buttons = Array.from(root.querySelectorAll('[data-admin-action]'));
      const replacement = buttons.find(button => !button.disabled && button.dataset.id === target.id && button.dataset.adminAction === target.action)
        || buttons.find(button => !button.disabled && button.dataset.id === target.id);
      if (replacement) replacement.focus();
      else {
        const heading = root.querySelector('h1');
        heading.tabIndex = -1;
        heading.focus();
      }
    });

    root.querySelectorAll('[data-requires-js]').forEach(element => { element.disabled = false; });
    const search = root.querySelector('[data-admin-filter="query"]');
    if (search) search.value = view.query;
    SS.onChange(render);
    render();
  });
})();
