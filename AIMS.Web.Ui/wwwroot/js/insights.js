/* Read-only local insights, plus explicitly reviewed draft purchase orders. */
(() => {
  'use strict';
  const S = window.SS;
  if (!S) return;
  const e = S.escape;
  const DAY = 86400000;
  const number = value => Number.isFinite(Number(value)) ? Number(value) : 0;
  const total = (items, value) => S.round(items.reduce((sum, item) => sum + value(item), 0));
  const matches = (value, query) => String(value).toLowerCase().includes(String(query).trim().toLowerCase());
  const validNumber = value => ['number', 'string'].includes(typeof value) && String(value).trim() !== '' && Number.isFinite(Number(value)) && Number(value) >= 0;
  const shortDate = value => new Date(`${value}T12:00:00`).toLocaleDateString('en-GB', { day: 'numeric', month: 'short' });
  const time = () => new Date().toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' });

  function day(value) {
    if (!/^\d{4}-\d{2}-\d{2}$/.test(String(value))) return NaN;
    const stamp = Date.parse(`${value}T00:00:00Z`);
    return Number.isFinite(stamp) && new Date(stamp).toISOString().slice(0, 10) === value ? stamp / DAY : NaN;
  }
  const isoDay = value => new Date(value * DAY).toISOString().slice(0, 10);
  const numericCell = value => `<span class="font-medium${value < 0 ? ' text-danger' : ''}">${e(S.number(value, 2))}</span>`;
  function itemCell(name, subtitle, icon = 'box', tone = 'green') {
    return `<div class="item-cell"><span class="item-icon tone-${tone}">${S.icon(icon)}</span><span><span class="cell-title">${e(name)}</span><span class="cell-subtitle">${e(subtitle)}</span></span></div>`;
  }
  function productCell(product) {
    const tones = { Electronics: 'blue', Stationery: 'amber', Lifestyle: 'purple', Groceries: 'green', Household: 'rose' };
    return itemCell(product.name, product.sku, product.category === 'Groceries' ? 'leaf' : product.category === 'Stationery' ? 'book' : 'box', tones[product.category] || 'green');
  }
  function statCards(stats) {
    return stats.map(stat => {
      const unit = stat.value.match(/^(.*) (products|units)$/);
      const value = stat.value.startsWith('BDT ') ? `<span class="currency">BDT</span>${e(stat.value.slice(4))}` : unit ? `${e(unit[1])} <span class="insights-stat-unit">${e(unit[2])}</span>` : e(stat.value);
      return `<article class="stat-card"><div class="stat-label">${e(stat.label)}<span class="stat-icon tone-${stat.tone}">${S.icon(stat.icon)}</span></div><div class="stat-value">${value}</div><div class="stat-meta">${e(stat.meta)}</div></article>`;
    }).join('');
  }

  function salesEvents(state) {
    const events = [];
    state.invoices.filter(invoice => !['draft', 'cancelled', 'void'].includes(invoice.status)).forEach(invoice => {
      const customer = S.customer(invoice.customerId, state)?.name || 'Unknown customer';
      if (Number.isFinite(day(invoice.date))) events.push({ document: invoice.number, date: invoice.date, customer, type: 'Sale', gross: number(invoice.total), credit: 0, net: number(invoice.total) });
      // Credits belong to their own return date, including returns against older sales.
      (invoice.returns || []).forEach(entry => {
        if (!Number.isFinite(day(entry.date))) return;
        events.push({ document: `Return / ${invoice.number}`, date: entry.date, customer, type: 'Return', gross: 0, credit: number(entry.credit), net: -number(entry.credit) });
      });
    });
    return events;
  }

  function salesSeries(events, from, to) {
    const start = day(from);
    const span = day(to) - start + 1;
    const width = Math.max(1, Math.ceil(span / 7));
    const bars = Array.from({ length: Math.ceil(span / width) }, (_, index) => {
      const first = isoDay(start + index * width);
      const last = isoDay(Math.min(day(to), start + (index + 1) * width - 1));
      return { label: shortDate(first), detail: first === last ? S.date(first) : `${S.date(first)} to ${S.date(last)}`, value: 0 };
    });
    events.forEach(event => {
      const index = Math.floor((day(event.date) - start) / width);
      if (bars[index]) bars[index].value = S.round(bars[index].value + event.net);
    });
    return { bars, note: width === 1 ? 'Daily totals. Days with no recorded transactions remain zero.' : `${width}-day groups, labeled by their first date. The final group may be shorter.` };
  }

  function groupedBars(items, label, value) {
    const groups = new Map();
    items.forEach(item => groups.set(label(item), (groups.get(label(item)) || 0) + value(item)));
    const entries = Array.from(groups, ([name, amount]) => ({ label: name, value: S.round(amount) })).sort((a, b) => b.value - a.value);
    if (entries.length <= 7) return entries;
    return [...entries.slice(0, 6), { label: 'Other', value: total(entries.slice(6), entry => entry.value) }];
  }

  function chartMarkup(bars, title, emptyMessage) {
    if (!bars.length || bars.every(bar => bar.value === 0)) return `<div class="insights-chart-empty">${S.icon('chart')}<strong>No values to plot</strong><p>${e(emptyMessage)}</p></div>`;
    const low = Math.min(0, ...bars.map(bar => bar.value));
    const high = Math.max(0, ...bars.map(bar => bar.value));
    const maximum = high === low ? high + 1 : high;
    const y = value => 23 + (maximum - value) / (maximum - low) * 172;
    const zero = y(0);
    const step = 640 / bars.length;
    const barWidth = Math.min(52, step * 0.58);
    const compact = value => Math.abs(value) >= 1000 ? `${S.number(value / 1000, 1)}k` : S.number(value, 1);
    const ticks = [maximum, (maximum + low) / 2, low];
    const description = bars.map(bar => `${bar.detail || bar.label}: ${S.money(bar.value)}`).join('; ');
    return `<svg class="insights-chart" viewBox="0 0 720 240" role="img" aria-labelledby="report-svg-title report-svg-description"><title id="report-svg-title">${e(title)}</title><desc id="report-svg-description">${e(description)}</desc>${ticks.map(value => `<path class="insights-chart-grid" d="M54 ${y(value)}H703"></path><text x="44" y="${y(value) + 4}" text-anchor="end">${e(compact(value))}</text>`).join('')}<path class="insights-chart-zero" d="M54 ${zero}H703"></path>${bars.map((bar, index) => {
      const center = 59 + step * (index + 0.5);
      const label = bar.label.length > 14 ? `${bar.label.slice(0, 12)}...` : bar.label;
      return `<g><title>${e(`${bar.detail || bar.label}: ${S.money(bar.value)}`)}</title><rect class="${bar.value < 0 ? 'is-negative' : 'is-positive'}${index === bars.length - 1 ? ' is-last' : ''}" x="${center - barWidth / 2}" y="${Math.min(y(bar.value), zero)}" width="${barWidth}" height="${Math.abs(y(bar.value) - zero)}" rx="4"></rect><text x="${center}" y="222" text-anchor="middle">${e(label)}</text></g>`;
    }).join('')}</svg>`;
  }

  function remainingOrderValue(order) {
    return total(order.items || [], item => Math.max(0, number(item.quantity) - number(item.received)) * number(item.unitCost));
  }

  function buildReport(type, filters, state = S.state) {
    const snapshot = `Snapshot as of ${S.date(S.today())}`;
    const base = {
      rows: [], stats: [], bars: [], numeric: [], scope: snapshot,
      chartLegend: 'Value in BDT', chartNote: 'Current values, not historical balances.',
      emptyChart: 'No non-zero balances match this view.',
      source: 'Browser-local sample data. Current snapshots cannot be reconstructed for past dates.'
    };
    if (type === 'sales') {
      const records = salesEvents(state).filter(event => event.date >= filters.from && event.date <= filters.to && matches(`${event.document} ${event.customer} ${event.type}`, filters.query)).sort((a, b) => b.date.localeCompare(a.date) || a.document.localeCompare(b.document));
      const invoices = records.filter(event => event.type === 'Sale');
      const returns = records.filter(event => event.type === 'Return');
      const series = salesSeries(records, filters.from, filters.to);
      const missingDates = state.invoices.reduce((sum, invoice) => sum + (Number.isFinite(day(invoice.date)) ? 0 : 1) + (invoice.returns || []).filter(entry => !Number.isFinite(day(entry.date))).length, 0);
      return {
        ...base, title: 'Sales transactions', columns: ['Document', 'Date', 'Customer', 'Type', 'Invoiced (BDT)', 'Credits (BDT)', 'Net sales (BDT)'], numeric: [4, 5, 6],
        rows: records.map(event => ({ html: [itemCell(event.document, event.type === 'Return' ? 'Return-date credit' : 'Original invoice total', 'receipt', event.type === 'Return' ? 'amber' : 'green'), e(S.date(event.date)), e(event.customer), S.badge(event.type === 'Return' ? 'returned' : 'paid', event.type), numericCell(event.gross), numericCell(event.credit), numericCell(event.net)], csv: [event.document, event.date, event.customer, event.type, event.gross, event.credit, event.net] })),
        stats: [
          { label: 'Net sales', value: S.money(total(records, event => event.net)), meta: 'Invoiced sales less dated return credits', icon: 'chart', tone: 'green' },
          { label: 'Invoiced sales', value: S.money(total(records, event => event.gross)), meta: `${S.number(invoices.length)} invoices in the applied period`, icon: 'receipt', tone: 'blue' },
          { label: 'Return credits', value: S.money(total(records, event => event.credit)), meta: 'Recognized on each return date', icon: 'refresh', tone: 'amber' },
          { label: 'Transactions', value: S.number(records.length), meta: `${S.number(invoices.length)} invoices and ${S.number(returns.length)} returns`, icon: 'layers', tone: 'purple' }
        ], bars: series.bars, chartTitle: 'Sales at a glance', chartSubtitle: 'Net sales by transaction date, in BDT', chartLegend: 'Net sales', chartNote: series.note,
        emptyChart: 'All date groups have zero net sales for the applied period and search. Matching transactions, if any, remain in the table.', scope: `${S.date(filters.from)} to ${S.date(filters.to)} / transaction dates`,
        contextTitle: 'A little context goes a long way.', contextDescription: 'Sales follow invoice dates. Return credits follow their own return dates, even when the original sale is outside the period.', contextLabel: 'Reporting basis', contextValue: 'Transaction dates', contextNote: 'Not cash flow. Original invoice totals include any invoice discount. This view does not imply receipts or payments on the same date.',
        source: `Browser-local sample invoices and dated returns. Opening stock is an independent illustrative snapshot, not a reconciled ledger.${missingDates ? ` ${missingDates} transaction records lack a valid date and are excluded rather than assigned a date.` : ''}`
      };
    }
    if (type === 'stock') {
      const records = state.products.filter(product => matches(`${product.name} ${product.sku} ${product.category} ${product.status}`, filters.query)).sort((a, b) => number(b.stock) * number(b.cost) - number(a.stock) * number(a.cost));
      const value = product => S.round(number(product.stock) * number(product.cost));
      return {
        ...base, title: 'Current stock valuation', columns: ['Product', 'Category', 'On hand', 'Unit cost (BDT)', 'Stock value (BDT)', 'Stock status'], numeric: [2, 3, 4],
        rows: records.map(product => ({ html: [productCell(product), e(product.category), numericCell(number(product.stock)), numericCell(number(product.cost)), numericCell(value(product)), S.badge(product.status !== 'active' ? product.status : S.stockStatus(product))], csv: [`${product.name} (${product.sku})`, product.category, number(product.stock), number(product.cost), value(product), product.status === 'active' ? S.stockStatus(product) : product.status] })),
        stats: [
          { label: 'Inventory value', value: S.money(total(records, value)), meta: 'On-hand quantity x current unit cost', icon: 'box', tone: 'green' },
          { label: 'Units on hand', value: S.number(total(records, product => number(product.stock)), 2), meta: 'Includes stock on archived records', icon: 'layers', tone: 'blue' },
          { label: 'Stocked products', value: S.number(records.filter(product => number(product.stock) > 0).length), meta: `${S.number(records.length)} matching catalog records`, icon: 'grid', tone: 'purple' },
          { label: 'At reorder level', value: S.number(records.filter(product => product.status === 'active' && number(product.stock) <= number(product.reorder)).length), meta: 'Active products, including stockouts', icon: 'alert', tone: 'amber' }
        ], bars: groupedBars(records, product => product.category || 'Uncategorized', value), chartTitle: 'Where your inventory value sits', chartSubtitle: 'On-hand cost value by category, in BDT',
        contextTitle: 'Value what is on the shelf.', contextDescription: 'This report uses each product\'s current recorded cost, not its selling price or next purchase cost. Approved orders and drafts are not stock.', contextLabel: 'Valuation basis', contextValue: 'On hand x unit cost', contextNote: 'All matching product records are included, even archived products with remaining stock. Product-level totals are not a historical valuation ledger.'
      };
    }
    if (type === 'suppliers') {
      const records = state.suppliers.filter(supplier => matches(`${supplier.name} ${supplier.contact} ${supplier.status}`, filters.query)).sort((a, b) => number(b.payable) - number(a.payable));
      const poValue = (supplier, statuses) => total(state.purchaseOrders.filter(order => order.supplierId === supplier.id && statuses.includes(order.status)), remainingOrderValue);
      const balances = total(records, supplier => number(supplier.payable));
      return {
        ...base, title: 'Supplier payable balances', columns: ['Supplier', 'Payable (BDT)', 'Open approved POs (BDT)', 'Unapproved drafts (BDT)', 'Account status'], numeric: [1, 2, 3],
        rows: records.map(supplier => ({ html: [itemCell(supplier.name, supplier.contact || 'Supplier account', 'truck', 'blue'), numericCell(number(supplier.payable)), numericCell(poValue(supplier, ['approved', 'partial'])), numericCell(poValue(supplier, ['draft'])), S.badge(supplier.status)], csv: [supplier.name, number(supplier.payable), poValue(supplier, ['approved', 'partial']), poValue(supplier, ['draft']), supplier.status] })),
        stats: [
          { label: 'Total payable', value: S.money(balances), meta: 'Stored supplier balances, including openings', icon: 'credit-card', tone: 'green' },
          { label: 'Suppliers with a balance', value: S.number(records.filter(supplier => number(supplier.payable) > 0).length), meta: `${S.number(records.length)} matching supplier accounts`, icon: 'truck', tone: 'blue' },
          { label: 'Largest balance', value: S.money(Math.max(0, ...records.map(supplier => number(supplier.payable)))), meta: records[0]?.name || 'No matching supplier', icon: 'chart', tone: 'amber' },
          { label: 'Zero-balance accounts', value: S.number(records.filter(supplier => number(supplier.payable) === 0).length), meta: 'Current snapshot, not a payment history', icon: 'check-circle', tone: 'purple' }
        ], bars: groupedBars(records, supplier => supplier.name, supplier => number(supplier.payable)), chartTitle: 'Payables by supplier', chartSubtitle: 'Current outstanding balances, in BDT',
        contextTitle: 'Commitments are not balances.', contextDescription: 'Supplier payables include sample opening balances and received goods, less recorded payments. Unreceived purchase orders are shown separately.', contextLabel: 'Balance basis', contextValue: 'Current supplier account', contextNote: 'PO values show remaining quantities at order cost. They are not added to payables. Invoice-level due dates and supplier aging are not available in this preview.'
      };
    }
    if (type === 'customers') {
      const records = state.customers.filter(customer => matches(`${customer.name} ${customer.email} ${customer.phone} ${customer.status}`, filters.query)).sort((a, b) => number(b.due) - number(a.due));
      const invoiceDues = customer => total(state.invoices.filter(invoice => invoice.customerId === customer.id), S.invoiceDue);
      return {
        ...base, title: 'Customer due balances', columns: ['Customer', 'Invoice dues (BDT)', 'Opening due (BDT)', 'Current due (BDT)', 'Credit limit (BDT)', 'Credit status'], numeric: [1, 2, 3, 4],
        rows: records.map(customer => {
          const status = number(customer.due) > number(customer.creditLimit) ? 'out' : number(customer.due) > 0 ? 'low' : 'paid';
          const label = number(customer.due) > number(customer.creditLimit) ? 'Over limit' : number(customer.due) > 0 ? 'Balance due' : 'Clear';
          return { html: [itemCell(customer.name, customer.status === 'active' ? customer.phone || 'Customer account' : 'Archived account', 'users', 'purple'), numericCell(invoiceDues(customer)), numericCell(number(customer.openingDue)), numericCell(number(customer.due)), numericCell(number(customer.creditLimit)), S.badge(status, label)], csv: [customer.name, invoiceDues(customer), number(customer.openingDue), number(customer.due), number(customer.creditLimit), label] };
        }),
        stats: [
          { label: 'Outstanding dues', value: S.money(total(records, customer => number(customer.due))), meta: 'Current customer account balances', icon: 'credit-card', tone: 'green' },
          { label: 'Customers with dues', value: S.number(records.filter(customer => number(customer.due) > 0).length), meta: `${S.number(records.length)} matching customer accounts`, icon: 'users', tone: 'blue' },
          { label: 'Opening balances', value: S.money(total(records, customer => number(customer.openingDue))), meta: 'Remaining opening debt, separate from invoices', icon: 'layers', tone: 'purple' },
          { label: 'Over credit limit', value: S.number(records.filter(customer => number(customer.due) > number(customer.creditLimit)).length), meta: 'Current due exceeds the recorded limit', icon: 'alert', tone: 'amber' }
        ], bars: groupedBars(records, customer => customer.name, customer => number(customer.due)), chartTitle: 'Who has an outstanding balance?', chartSubtitle: 'Current customer dues, in BDT', emptyChart: 'These matching customer accounts have no outstanding dues. Zero balances are shown in the table below.',
        contextTitle: 'A current view of customer credit.', contextDescription: 'Invoice dues use original totals less recorded receipts and return credits. Remaining opening debt is shown separately from invoices.', contextLabel: 'Balance basis', contextValue: 'Current customer account', contextNote: 'Current due is the stored customer balance. This snapshot includes archived accounts with residual debt. It is not a historical aging report.'
      };
    }
    const records = state.products.filter(product => number(product.stock) > 0 && Number.isFinite(day(product.expiry)) && day(product.expiry) - day(S.today()) <= filters.expiryDays && matches(`${product.name} ${product.sku} ${product.category}`, filters.query)).sort((a, b) => a.expiry.localeCompare(b.expiry));
    const value = product => S.round(number(product.stock) * number(product.cost));
    const expired = records.filter(product => product.expiry < S.today());
    return {
      ...base, title: 'Product expiry watch', columns: ['Product', 'On hand', 'Expiry date', 'Time remaining', 'At-risk value (BDT)', 'Status'], numeric: [1, 4],
      rows: records.map(product => {
        const days = day(product.expiry) - day(S.today());
        const remaining = days < 0 ? `${Math.abs(days)} days overdue` : days === 0 ? 'Expires today' : `${days} days`;
        const label = days < 0 ? 'Expired' : days <= 7 ? 'Due soon' : 'Upcoming';
        return { html: [productCell(product), numericCell(number(product.stock)), e(S.date(product.expiry)), e(remaining), numericCell(value(product)), S.badge(days < 0 ? 'out' : days <= 7 ? 'low' : 'info', label)], csv: [`${product.name} (${product.sku})`, number(product.stock), product.expiry, remaining, value(product), label] };
      }),
      stats: [
        { label: 'Value to review', value: S.money(total(records, value)), meta: `Expired or expiring within ${filters.expiryDays} days`, icon: 'box', tone: 'green' },
        { label: 'Units to review', value: S.number(total(records, product => number(product.stock)), 2), meta: 'On-hand quantities with an in-window date', icon: 'layers', tone: 'blue' },
        { label: 'Products to review', value: S.number(records.length), meta: 'Stocked, dated product records only', icon: 'calendar', tone: 'purple' },
        { label: 'Already expired', value: S.number(expired.length), meta: `${S.number(total(expired, product => number(product.stock)), 2)} on-hand units across expired products`, icon: 'alert', tone: 'amber' }
      ], bars: groupedBars(records, product => product.name, value), chartTitle: 'Value approaching expiry', chartSubtitle: 'Current cost value of matching dated products, in BDT', scope: `${snapshot} / expired + next ${filters.expiryDays} days`, emptyChart: 'No stocked, dated products match this expiry window and search.',
      contextTitle: 'Make time for the stock that matters.', contextDescription: 'Only products with on-hand stock and a recorded expiry date are assessed. Expired products remain visible alongside the selected upcoming window.', contextLabel: records.length ? 'Earliest date in this view' : 'Selected expiry window', contextValue: records.length ? S.date(records[0].expiry) : `Next ${filters.expiryDays} days`, contextNote: 'Product-level dates only, not batch-level expiry allocation. No expiry date is inferred and no dead-stock prediction is made.'
    };
  }

  function initReports(root) {
    let type = 'sales';
    const filters = { from: S.daysFromToday(-6), to: S.today(), query: '', expiryDays: 30 };
    const byId = id => root.querySelector(`#${id}`);
    const from = byId('report-from');
    const to = byId('report-to');
    const period = byId('report-period');
    const error = byId('report-filter-error');
    from.value = filters.from;
    to.value = filters.to;

    function render() {
      const report = buildReport(type, filters);
      byId('report-date-form').hidden = type !== 'sales';
      byId('report-snapshot-controls').hidden = type === 'sales';
      byId('report-expiry-field').hidden = type !== 'expiry';
      byId('report-snapshot-label').textContent = `Current snapshot / ${S.date(S.today())}`;
      byId('report-stats').innerHTML = statCards(report.stats);
      byId('report-chart-title').textContent = report.chartTitle;
      byId('report-chart-subtitle').textContent = report.chartSubtitle;
      byId('report-chart').innerHTML = chartMarkup(report.bars, report.chartTitle, report.emptyChart);
      byId('report-chart-footnote').textContent = report.chartNote;
      root.querySelector('.insights-chart-legend').innerHTML = `<span aria-hidden="true"></span>${e(report.chartLegend)}`;
      byId('report-context-title').textContent = report.contextTitle;
      byId('report-context-description').textContent = report.contextDescription;
      byId('report-context-label').textContent = report.contextLabel;
      byId('report-context-value').textContent = report.contextValue;
      byId('report-context-note').textContent = report.contextNote;
      byId('report-results-title').textContent = report.title;
      byId('report-results-subtitle').textContent = `${report.scope}${filters.query ? ` / search: ${filters.query}` : ''}`;
      byId('report-table-caption').textContent = `${report.title}. ${report.scope}. Browser-local sample data.`;
      byId('report-table-head').innerHTML = `<tr>${report.columns.map((column, index) => `<th scope="col"${report.numeric.includes(index) ? ' class="text-right"' : ''}>${e(column)}</th>`).join('')}</tr>`;
      byId('report-table-body').innerHTML = report.rows.length ? report.rows.map(row => `<tr>${row.html.map((cell, index) => `<td${report.numeric.includes(index) ? ' class="text-right"' : ''}>${cell}</td>`).join('')}</tr>`).join('') : S.emptyRow(report.columns.length, 'No records match the applied report filters.');
      byId('report-row-count').textContent = `${S.number(report.rows.length)} matching ${report.rows.length === 1 ? 'row' : 'rows'} / all matching rows shown`;
      byId('report-scope').textContent = type === 'sales' ? 'Transaction-date history' : 'Current snapshot';
      byId('report-source-note').innerHTML = `${S.icon('info')}<span>${e(report.source)} CSV contains the current filtered view; Print opens the browser print dialog.</span>`;
      root.querySelectorAll('[data-report-type]').forEach(button => {
        const active = button.dataset.reportType === type;
        button.classList.toggle('is-active', active);
        button.setAttribute('aria-selected', String(active));
        button.tabIndex = active ? 0 : -1;
      });
      byId('report-panel').setAttribute('aria-labelledby', `report-tab-${type}`);
    }
    function activate(button) {
      type = button.dataset.reportType;
      filters.query = '';
      byId('report-search').value = '';
      error.hidden = true;
      render();
    }
    byId('report-tabs').addEventListener('click', event => {
      const button = event.target.closest('[data-report-type]');
      if (button) activate(button);
    });
    byId('report-tabs').addEventListener('keydown', event => {
      if (!['ArrowLeft', 'ArrowRight', 'Home', 'End'].includes(event.key)) return;
      const buttons = [...root.querySelectorAll('[data-report-type]')];
      const index = buttons.indexOf(document.activeElement);
      if (index < 0) return;
      event.preventDefault();
      const next = event.key === 'Home' ? 0 : event.key === 'End' ? buttons.length - 1 : (index + (event.key === 'ArrowRight' ? 1 : -1) + buttons.length) % buttons.length;
      activate(buttons[next]);
      buttons[next].focus();
    });
    byId('report-date-form').addEventListener('submit', event => {
      event.preventDefault();
      const start = day(from.value);
      const end = day(to.value);
      if (!Number.isFinite(start) || !Number.isFinite(end) || start > end || end - start > 365) {
        error.textContent = 'Choose valid dates in order, with a range no longer than 366 days. The previous applied report is unchanged.';
        error.hidden = false;
        return;
      }
      filters.from = from.value;
      filters.to = to.value;
      error.hidden = true;
      render();
    });
    [from, to].forEach(input => input.addEventListener('input', () => { period.value = 'custom'; }));
    period.addEventListener('change', () => {
      if (period.value === 'custom') { from.focus(); return; }
      const days = Number(period.value);
      if (![7, 30, 90].includes(days)) return;
      filters.from = S.daysFromToday(-(days - 1));
      filters.to = S.today();
      from.value = filters.from;
      to.value = filters.to;
      error.hidden = true;
      render();
    });
    byId('report-expiry-days').addEventListener('change', event => {
      const days = Number(event.target.value);
      if ([7, 30, 90, 180].includes(days)) { filters.expiryDays = days; render(); }
    });
    byId('report-search').addEventListener('input', event => { filters.query = event.target.value; render(); });
    byId('report-export').addEventListener('click', () => {
      const report = buildReport(type, filters);
      const scope = type === 'sales' ? `${filters.from}_to_${filters.to}` : `snapshot_${S.today()}`;
      S.downloadCSV(`smartstock-${type}-${scope}.csv`, [[report.title], ['Scope', report.scope], ['Source', report.source], ['Search', filters.query || 'All records'], [], report.columns, ...report.rows.map(row => row.csv)]);
    });
    byId('report-print').addEventListener('click', () => { render(); window.print(); });
    S.onChange(render);
    render();
  }

  function reorderRows(state, horizon) {
    return state.products.filter(product => product.status === 'active').map(product => {
      const supplier = S.supplier(product.supplierId, state);
      const problems = [];
      if (!supplier || supplier.status !== 'active') problems.push('An active preferred supplier is required.');
      if (!validNumber(product.stock)) problems.push('On-hand stock is invalid.');
      if (!validNumber(product.reorder)) problems.push('A valid reorder level is required.');
      if (!validNumber(product.dailyDemand)) problems.push('Daily demand is not configured.');
      if (!supplier || !validNumber(supplier.leadTime)) problems.push('Supplier lead time is not configured.');
      if (!validNumber(product.purchaseCost) || Number(product.purchaseCost) <= 0) problems.push('A positive default purchase cost is required.');
      let incoming = 0;
      let drafts = 0;
      let invalidOrder = false;
      state.purchaseOrders.filter(order => ['approved', 'partial', 'draft'].includes(order.status)).forEach(order => {
        (order.items || []).filter(item => item.productId === product.id).forEach(item => {
          if (!validNumber(item.quantity) || !validNumber(item.received ?? 0) || Number(item.received || 0) > Number(item.quantity)) { invalidOrder = true; return; }
          const remaining = Math.max(0, Number(item.quantity) - Number(item.received || 0));
          if (order.status === 'draft') drafts += remaining;
          else incoming += remaining;
        });
      });
      if (invalidOrder) problems.push('An open purchase order has invalid quantities.');
      const stock = number(product.stock);
      const demand = number(product.dailyDemand);
      const leadTime = number(supplier?.leadTime);
      const unitCost = number(product.purchaseCost);
      const coverage = validNumber(product.stock) && validNumber(product.dailyDemand) && demand > 0 ? stock / demand : null;
      const calculatedTarget = Math.max(Math.ceil(number(product.reorder)), Math.ceil(demand * (horizon + leadTime)));
      const calculatedQuantity = Math.max(0, Math.ceil(calculatedTarget - stock - incoming - drafts));
      if (!problems.length && (!Number.isSafeInteger(calculatedTarget) || !Number.isSafeInteger(calculatedQuantity) || !Number.isFinite(S.round(calculatedQuantity * unitCost)) || !Number.isFinite(incoming + drafts))) problems.push('Reorder inputs exceed a safe numeric range.');
      if (supplier && validNumber(supplier.leadTime) && !Number.isFinite(day(S.daysFromToday(Math.ceil(leadTime))))) problems.push('Supplier lead time is outside a valid date range.');
      const target = problems.length ? null : calculatedTarget;
      const need = target === null ? 0 : Math.max(0, target - stock - incoming);
      // Draft quantities prevent duplicate conversions, but never improve stock coverage.
      const quantity = problems.length ? 0 : Math.max(0, Math.ceil(need - drafts));
      const risk = !problems.length && (stock === 0 || coverage !== null && coverage <= leadTime);
      const priority = problems.length ? 'blocked' : stock === 0 ? 'out' : risk ? 'risk' : drafts > 0 && need > 0 && quantity === 0 ? 'pending' : quantity > 0 ? 'plan' : incoming > 0 && stock < target ? 'incoming' : 'covered';
      const row = { product, supplier, problems, stock, demand, leadTime, unitCost, coverage, target, need, incoming, drafts, quantity, risk, priority, cost: S.round(quantity * unitCost) };
      row.fingerprint = JSON.stringify([product.id, product.supplierId, stock, product.reorder, product.dailyDemand, supplier?.leadTime, product.purchaseCost, incoming, drafts, quantity, supplier?.status]);
      return row;
    }).sort((a, b) => {
      const ranks = { out: 0, risk: 1, blocked: 2, pending: 3, plan: 4, incoming: 5, covered: 6 };
      return ranks[a.priority] - ranks[b.priority] || (a.coverage ?? Infinity) - (b.coverage ?? Infinity) || a.product.name.localeCompare(b.product.name);
    });
  }

  function priorityBadge(row) {
    const states = { blocked: ['low', 'Input needed'], out: ['out', 'Out of stock'], risk: ['low', 'Lead-time risk'], pending: ['pending', 'Draft pending'], plan: ['info', 'Plan ahead'], incoming: ['approved', 'Approved incoming'], covered: ['healthy', 'In range'] };
    return S.badge(...states[row.priority]);
  }
  const needsReview = row => row.quantity > 0 || row.problems.length > 0 || row.need > 0 || row.risk;
  const canSelect = row => !row.problems.length && row.quantity > 0;

  function initReorder(root) {
    let horizon = 14;
    let view = 'needs';
    let query = '';
    let rows = [];
    let visible = [];
    const selected = new Set();
    const byId = id => root.querySelector(`#${id}`);
    function renderSelection() {
      const chosen = rows.filter(row => selected.has(row.product.id) && canSelect(row));
      byId('reorder-selection-count').textContent = chosen.length ? `${S.number(chosen.length)} ${chosen.length === 1 ? 'product' : 'products'} selected across all views` : 'No products selected';
      byId('reorder-selection-value').textContent = chosen.length ? `${S.money(total(chosen, row => row.cost))} estimated / ${new Set(chosen.map(row => row.supplier.id)).size} supplier-grouped local drafts` : 'Select eligible rows to review supplier-grouped draft orders.';
      root.querySelectorAll('[data-reorder-review]').forEach(button => {
        button.disabled = !chosen.length;
        button.querySelector('span').textContent = chosen.length ? `Review ${chosen.length} selected` : 'Review selected';
      });
      const eligible = visible.filter(canSelect);
      const selectedVisible = eligible.filter(row => selected.has(row.product.id)).length;
      const all = byId('reorder-select-all');
      all.disabled = !eligible.length;
      all.checked = eligible.length > 0 && selectedVisible === eligible.length;
      all.indeterminate = selectedVisible > 0 && selectedVisible < eligible.length;
      root.querySelectorAll('[data-reorder-select]').forEach(input => {
        input.checked = selected.has(input.dataset.reorderSelect);
        input.closest('tr').classList.toggle('is-selected', input.checked);
      });
    }
    function render() {
      rows = reorderRows(S.state, horizon);
      selected.forEach(id => { if (!rows.some(row => row.product.id === id && canSelect(row))) selected.delete(id); });
      visible = rows.filter(row => (view === 'all' || view === 'drafts' && row.drafts > 0 || view === 'needs' && needsReview(row)) && matches(`${row.product.name} ${row.product.sku} ${row.supplier?.name || ''}`, query));
      const ready = rows.filter(canSelect);
      byId('reorder-stats').innerHTML = statCards([
        { label: 'Ready for review', value: `${S.number(ready.length)} products`, meta: 'Additional quantities, after existing drafts', icon: 'box', tone: 'green' },
        { label: 'Estimated draft value', value: S.money(total(ready, row => row.cost)), meta: 'At current default purchase costs', icon: 'cart', tone: 'blue' },
        { label: 'Lead-time risks', value: `${S.number(rows.filter(row => row.risk).length)} products`, meta: 'On-hand coverage at or below usual lead time', icon: 'alert', tone: 'amber' },
        { label: 'Already in drafts', value: `${S.number(total(rows, row => row.drafts), 2)} units`, meta: 'Unapproved; not counted as incoming stock', icon: 'layers', tone: 'purple' }
      ]);
      byId('reorder-summary').textContent = `${horizon}-day horizon / metrics cover all ${rows.length} active products${rows.some(row => row.problems.length) ? ` / ${rows.filter(row => row.problems.length).length} need input` : ''}`;
      root.querySelectorAll('[data-reorder-view]').forEach(button => {
        const active = button.dataset.reorderView === view;
        button.classList.toggle('is-active', active);
        button.setAttribute('aria-pressed', String(active));
      });
      const counts = { needs: rows.filter(needsReview).length, drafts: rows.filter(row => row.drafts > 0).length, all: rows.length };
      root.querySelectorAll('[data-reorder-count]').forEach(element => { element.textContent = counts[element.dataset.reorderCount]; });
      byId('reorder-table-body').innerHTML = visible.length ? visible.map(row => {
        const enabled = canSelect(row);
        const draftText = row.problems.length ? 'Input needed' : row.quantity > 0 ? `${S.number(row.quantity)} units` : row.need > 0 && row.drafts > 0 ? 'Covered by existing draft' : 'No additional draft';
        const detail = row.problems.length ? row.problems.join(' ') : row.quantity > 0 ? `${S.money(row.cost)} estimated${row.drafts ? ` / ${S.number(row.drafts)} already drafted` : ''}` : row.drafts > 0 ? 'Review unapproved orders in Purchases' : row.incoming > 0 ? 'Approved orders cover the target' : 'On-hand stock covers the target';
        return `<tr${row.problems.length ? ' class="is-blocked"' : ''}><td><input type="checkbox" class="insights-checkbox" data-reorder-select="${e(row.product.id)}" aria-label="${e(enabled ? `Select ${row.product.name}` : `${row.product.name}: ${draftText}`)}"${enabled ? '' : ' disabled'}></td><td>${productCell(row.product)}</td><td><span class="cell-title">${e(row.supplier?.name || 'Supplier required')}</span><span class="cell-subtitle">${row.supplier && validNumber(row.supplier.leadTime) ? `${e(S.number(row.leadTime, 1))}-day lead time` : 'Lead time unavailable'}${row.supplier && row.supplier.status !== 'active' ? ' / inactive' : ''}</span></td><td class="insights-supply"><span class="cell-title">${validNumber(row.product.stock) ? e(S.number(row.stock, 2)) : 'Invalid'} on hand</span><span class="cell-subtitle">${e(S.number(row.incoming, 2))} approved incoming</span>${row.drafts ? `<span class="cell-subtitle insights-draft-warning">${e(S.number(row.drafts, 2))} in unapproved drafts</span>` : ''}</td><td>${validNumber(row.product.dailyDemand) ? `${e(S.number(row.demand, 2))} / day` : '<span class="text-muted">Not set</span>'}</td><td><span class="cell-title">${row.coverage === null ? validNumber(row.product.dailyDemand) && row.demand === 0 ? 'No daily draw' : 'Unavailable' : `${e(S.number(row.coverage, 1))} days`}</span><span class="cell-subtitle">Before any receipts</span></td><td class="insights-new-draft"><span class="cell-title">${e(draftText)}</span><span class="cell-subtitle insights-row-note">${e(detail)}</span></td><td>${priorityBadge(row)}${row.target !== null ? `<span class="cell-subtitle insights-row-note">Target: ${e(S.number(row.target))} units</span>` : ''}</td></tr>`;
      }).join('') : S.emptyRow(8, 'No active products match this view. Try All products or clear the search.');
      byId('reorder-visible-count').textContent = `${visible.length} matching products / selection uses only eligible rows`;
      byId('reorder-updated').textContent = `Calculated ${S.date(S.today())}, ${time()} / local data`;
      renderSelection();
    }
    function review() {
      const chosen = rows.filter(row => selected.has(row.product.id) && canSelect(row));
      if (!chosen.length) return;
      const reviewedHorizon = horizon;
      const groups = new Map();
      chosen.forEach(row => {
        if (!groups.has(row.supplier.id)) groups.set(row.supplier.id, []);
        groups.get(row.supplier.id).push(row);
      });
      S.openModal({
        title: 'Review your draft purchase orders', wide: true, submitLabel: `Create ${groups.size} local ${groups.size === 1 ? 'draft' : 'drafts'}`,
        body: `<div class="insights-review"><div class="notice notice-info"><strong>${reviewedHorizon}-day rule-based plan</strong><p>These are unapproved local drafts, grouped by active supplier. Existing draft quantities have already been excluded from the additional quantities below. No stock, payable or supplier message is created.</p></div>${[...groups.values()].map(group => `<section class="insights-review-group"><header><h3>${e(group[0].supplier.name)}</h3><span>${e(S.number(group[0].leadTime, 1))}-day usual lead time / estimated cost only</span></header><div class="table-wrap" tabindex="0" role="region" aria-label="Draft items for ${e(group[0].supplier.name)}"><table class="data-table"><thead><tr><th scope="col">Product</th><th scope="col" class="text-right">Quantity</th><th scope="col" class="text-right">Unit cost</th><th scope="col" class="text-right">Line total</th></tr></thead><tbody>${group.map(row => `<tr><td><span class="cell-title">${e(row.product.name)}</span><span class="cell-subtitle">${e(row.product.sku)}</span></td><td class="text-right">${e(S.number(row.quantity))}</td><td class="text-right">${e(S.money(row.unitCost))}</td><td class="text-right">${e(S.money(row.cost))}</td></tr>`).join('')}</tbody></table></div></section>`).join('')}<div class="insights-review-total"><span>Estimated total / ${chosen.length} products</span><strong>${e(S.money(total(chosen, row => row.cost)))}</strong></div><label class="insights-review-ack"><input type="checkbox" name="acknowledge" class="insights-checkbox" required><span>I understand these are browser-local drafts only. I will review and approve purchase orders separately before receiving any stock.</span></label></div>`,
        onSubmit(formData) {
          if (!formData.get('acknowledge')) throw new Error('Confirm that these are local drafts before continuing.');
          const created = [];
          S.update(state => {
            const fresh = reorderRows(state, reviewedHorizon);
            chosen.forEach(previous => {
              const current = fresh.find(row => row.product.id === previous.product.id);
              if (!current || !canSelect(current) || current.fingerprint !== previous.fingerprint) throw new Error('A selected product, supplier or purchase order changed after this review. Close this dialog and review the refreshed suggestions before creating drafts.');
            });
            groups.forEach(group => {
              const order = { id: S.uid('po'), number: S.nextNumber(state, 'purchase'), supplierId: group[0].supplier.id, date: S.today(), expected: S.daysFromToday(Math.ceil(group[0].leadTime)), status: 'draft', items: group.map(row => ({ productId: row.product.id, quantity: row.quantity, received: 0, unitCost: row.unitCost })), paid: 0, notes: `Prepared from the rule-based Smart Reorder preview. ${reviewedHorizon}-day horizon; review before approval. No supplier message sent.` };
              state.purchaseOrders.push(order);
              created.push(order.number);
            });
          }, 'Reorder drafts created', 'Purchases', `${groups.size} supplier-grouped local drafts from ${chosen.length} products; ${reviewedHorizon}-day horizon. No stock or payable posted.`);
          selected.clear();
          render();
          byId('reorder-success').innerHTML = `<strong>${e(created.join(', '))} created as ${created.length === 1 ? 'a local draft' : 'local drafts'}.</strong> No stock or payable changed. <a href="purchases.html">Review purchase orders</a>`;
          byId('reorder-success').hidden = false;
          S.toast(`${created.length} local purchase ${created.length === 1 ? 'draft' : 'drafts'} created. No supplier was contacted.`);
        }
      });
    }
    byId('reorder-tabs').addEventListener('click', event => {
      const button = event.target.closest('[data-reorder-view]');
      if (button) { view = button.dataset.reorderView; render(); }
    });
    byId('reorder-search').addEventListener('input', event => { query = event.target.value; render(); });
    byId('reorder-horizon').addEventListener('change', event => {
      const value = Number(event.target.value);
      if (![7, 14, 30].includes(value)) return;
      horizon = value;
      selected.clear();
      render();
    });
    byId('reorder-select-all').addEventListener('change', event => {
      visible.filter(canSelect).forEach(row => event.target.checked ? selected.add(row.product.id) : selected.delete(row.product.id));
      renderSelection();
    });
    byId('reorder-table-body').addEventListener('change', event => {
      const input = event.target.closest('[data-reorder-select]');
      if (!input || !rows.some(row => row.product.id === input.dataset.reorderSelect && canSelect(row))) return;
      if (input.checked) selected.add(input.dataset.reorderSelect);
      else selected.delete(input.dataset.reorderSelect);
      renderSelection();
    });
    root.querySelectorAll('[data-reorder-review]').forEach(button => button.addEventListener('click', review));
    byId('reorder-refresh').addEventListener('click', () => { render(); S.toast('Suggestions recalculated from the current local workspace.'); });
    S.onChange(render);
    render();
  }

  function assistantSalesWindow(query) {
    const explicitDays = query.match(/\b(\d+)\s+days?\b/);
    if (explicitDays && ![7, 30].includes(Number(explicitDays[1]))) return null;
    const windows = [/\btoday\b/.test(query), /\byesterday\b/.test(query), /\bthis\s+week\b/.test(query), !!explicitDays];
    if (windows.filter(Boolean).length > 1 || [...query.matchAll(/\b\d+\s+days?\b/g)].length > 1) return null;
    if (/\b(?:years?|quarters?|months?|hours?|january|february|march|april|june|july|august|september|october|november|december|compare|comparison|growth|versus|vs|tomorrow|next|future)\b|\b(?:in|for|during|of)\s+may\b|\blast\s+week\b|\b\d{4}\b|\b\d{1,2}[/-]\d{1,2}\b/.test(query)) return null;
    if (/\byesterday\b/.test(query)) return { from: S.daysFromToday(-1), to: S.daysFromToday(-1), label: 'Yesterday' };
    if (/\btoday\b/.test(query)) return { from: S.today(), to: S.today(), label: 'Today' };
    if (/\bthis\s+week\b/.test(query)) {
      const weekday = (new Date(`${S.today()}T12:00:00`).getDay() + 6) % 7;
      return { from: S.daysFromToday(-weekday), to: S.today(), label: 'This week, Monday through today' };
    }
    const days = explicitDays ? Number(explicitDays[1]) : /\b30\s+days?\b/.test(query) ? 30 : 7;
    return { from: S.daysFromToday(-(days - 1)), to: S.today(), label: `Last ${days} days` };
  }

  function assistantAnswer(prompt) {
    const query = prompt.toLowerCase();
    const state = S.state;
    const source = `Read ${S.date(S.today())} at ${time()}. Source: this browser's local demo workspace.`;
    const base = { title: 'A small, read-only preview', paragraphs: [], metrics: [], headers: [], rows: [], source };
    if (/\b(create|delete|approve|cancel|send|email|place|record|adjust|update|remove|change|add|pay|buy)\b/.test(query)) return { ...base, title: 'Business records stay in your control.', paragraphs: ['This scripted preview cannot create, change, approve or pay business records, and it cannot contact suppliers. I can only read the current sample data.', 'Use the appropriate dashboard page for a reviewed local action. Try asking for stock alerts, a sales summary, supplier balances or reorder guidance.'] };
    if (/\b(reorder|restock|replenish)\b/.test(query)) {
      const requested = query.match(/\b(\d+)\s*days?\b/);
      if (requested && ![7, 14, 30].includes(Number(requested[1]))) return { ...base, paragraphs: ['Reorder estimates support a 7, 14 or 30-day planning horizon. No forecast model is connected.'], href: 'smart-reorder.html', link: 'Choose a horizon in Smart Reorder' };
      const horizon = requested ? Number(requested[1]) : 14;
      const rows = reorderRows(state, horizon);
      const ready = rows.filter(canSelect);
      const blocked = rows.filter(row => row.problems.length);
      return { ...base, title: `A ${horizon}-day reorder starting point`, paragraphs: [
        `${ready.length} products have additional quantities ready for draft review. The estimate uses daily demand x (lead time + horizon), with the reorder level as a minimum, less on-hand and outstanding approved quantities.`,
        `${S.number(total(rows, row => row.drafts), 2)} units already sit in unapproved drafts. Those units prevent duplicate draft suggestions, but are not assured supply.${blocked.length ? ` ${blocked.length} products are blocked by missing or invalid inputs.` : ''} No orders were created.`,
        `This is rule-based, not machine learning or a calibrated forecast.${ready.length > 8 ? ' Showing the first 8 eligible products; totals cover the full shortlist.' : ''}`
      ], metrics: [{ label: 'Ready for review', value: `${ready.length} products` }, { label: 'Estimated draft value', value: S.money(total(ready, row => row.cost)) }, { label: 'Lead-time risks', value: `${rows.filter(row => row.risk).length} products` }], headers: ['Product', 'Additional units', 'Estimated cost'], rows: ready.slice(0, 8).map(row => [row.product.name, S.number(row.quantity), S.money(row.cost)]), href: 'smart-reorder.html', link: 'Review the full reorder shortlist', source: `${source} Coverage uses on-hand stock only; incoming delivery dates are not guaranteed.` };
    }
    if (/\b(profit|margin|forecast|predict|prediction|confidence|tax|cashflow|valuation|weather)\b|cash\s+flow|best.?sell|top.?sell|sales\s+by\s+product/.test(query)) return { ...base, paragraphs: ['That analysis is not supported by this scripted preview, and I will not invent an answer. I can show stock alerts, dated sales totals, supplier balances or rule-based reorder estimates.'], href: 'reports.html', link: 'Explore the available reports' };
    if (/\b(customers?|receivables?)\b/.test(query)) return { ...base, paragraphs: ['Customer-specific answers are not implemented in this scripted preview. The Customer dues report shows current invoice balances, opening dues and credit limits without inferring missing information.'], href: 'reports.html', link: 'Open Reports for customer dues' };
    if (/\b(stock|inventory|alerts?|expiry|expired|expiring)\b/.test(query)) {
      if (/expir/.test(query)) {
        const days = validNumber(state.settings.expiryDays) ? Number(state.settings.expiryDays) : 30;
        const products = state.products.filter(product => number(product.stock) > 0 && Number.isFinite(day(product.expiry)) && day(product.expiry) - day(S.today()) <= days).sort((a, b) => a.expiry.localeCompare(b.expiry));
        return { ...base, title: 'Keep an eye on these expiry dates.', paragraphs: [`${products.length} stocked products are expired or expire within the workspace's ${days}-day warning window. Undated records are not assessed.${products.length > 8 ? ' Showing the first 8 by expiry date.' : ''}`], headers: ['Product', 'On hand', 'Expiry'], rows: products.slice(0, 8).map(product => [product.name, S.number(product.stock, 2), S.date(product.expiry)]), href: 'reports.html', link: 'Open Reports for expiry filters', source: `${source} Current product-level dates, not batch-level expiry or a historical view.` };
      }
      const products = state.products.filter(product => product.status === 'active' && number(product.stock) <= number(product.reorder)).sort((a, b) => number(a.stock) - number(b.stock));
      const out = products.filter(product => number(product.stock) === 0);
      return { ...base, title: products.length ? 'These products need a closer look.' : 'No current low-stock alerts.', paragraphs: [`${products.length} active products are at or below their reorder level; ${out.length} are out of stock. These are on-hand alerts, not a claim that no purchase order exists.${products.length > 8 ? ' Showing the first 8; open Stock for the rest.' : ''}`], headers: ['Product', 'On hand', 'Reorder level', 'Status'], rows: products.slice(0, 8).map(product => [product.name, S.number(product.stock, 2), S.number(product.reorder, 2), number(product.stock) === 0 ? 'Out of stock' : 'Low stock']), href: 'stock.html', link: 'Review stock and incoming orders', source: `${source} Current product snapshot. Opening stock and illustrative invoice history are independent.` };
    }
    if (/\b(suppliers?|vendors?|payables?|owe)\b/.test(query)) {
      const suppliers = state.suppliers.filter(supplier => number(supplier.payable) > 0).sort((a, b) => number(b.payable) - number(a.payable));
      return { ...base, title: 'Your current supplier balances', paragraphs: [`${S.money(total(suppliers, supplier => number(supplier.payable)))} is recorded as payable across ${suppliers.length} supplier accounts, including opening balances and archived accounts with residual debt.`, `Unreceived purchase orders and unapproved drafts are not included in those balances. No payment has been made.${suppliers.length > 8 ? ' Showing the 8 largest accounts; the total includes all accounts.' : ''}`], headers: ['Supplier', 'Current payable', 'Account status'], rows: suppliers.slice(0, 8).map(supplier => [supplier.name, S.money(supplier.payable), supplier.status]), href: 'suppliers.html', link: 'Review supplier accounts', source: `${source} Current stored payables only; invoice aging and due dates are unavailable.` };
    }
    if (/\b(sales?|revenue|sold|invoices?|returns?)\b/.test(query)) {
      const window = assistantSalesWindow(query);
      if (!window) return { ...base, title: 'Choose a supported sales window.', paragraphs: ['I can summarize today, yesterday, this week (Monday through today), or the last 7 or 30 days. Custom dates and period comparisons belong in Reports; I will not guess the period you mean.'], href: 'reports.html', link: 'Set a custom period in Reports' };
      const report = buildReport('sales', { ...window, query: '', expiryDays: 30 }, state);
      const events = salesEvents(state).filter(event => event.date >= window.from && event.date <= window.to);
      const recent = report.rows.slice(0, 5);
      return { ...base, title: `${window.label}: recorded sales`, paragraphs: [`${S.date(window.from)} to ${S.date(window.to)}. ${events.filter(event => event.type === 'Sale').length} invoices and ${events.filter(event => event.type === 'Return').length} dated return credits are included. Sales follow invoice dates; credits follow return dates, even for older sales.`, recent.length ? `The table shows the ${recent.length} most recent matching transactions. This is sales activity, not cash collected.` : 'No transactions match this period. No sales value has been inferred.'], metrics: [{ label: 'Net sales', value: S.money(total(events, event => event.net)) }, { label: 'Invoiced sales', value: S.money(total(events, event => event.gross)) }, { label: 'Return credits', value: S.money(total(events, event => event.credit)) }], headers: ['Document', 'Date', 'Type', 'Net (BDT)'], rows: recent.map(row => [row.csv[0], S.date(row.csv[1]), row.csv[3], S.number(row.csv[6], 2)]), href: 'reports.html', link: 'Explore the full sales report', source: `${source} ${report.source}` };
    }
    return { ...base, paragraphs: ['I do not have a connected AI model or general knowledge service. This local preview recognizes a small set of workspace questions; it will not guess answers outside them.', 'Try "Show low-stock alerts", "Summarize sales today", "What do we owe suppliers?" or "Explain reorder suggestions for 14 days". No business records are changed.'] };
  }

  function answerMarkup(answer) {
    return `<h3>${e(answer.title)}</h3>${answer.paragraphs.map(paragraph => `<p>${e(paragraph)}</p>`).join('')}${answer.metrics.length ? `<div class="assistant-answer-metrics">${answer.metrics.map(metric => `<div><span>${e(metric.label)}</span><strong>${e(metric.value)}</strong></div>`).join('')}</div>` : ''}${answer.rows.length ? `<div class="table-wrap" tabindex="0" role="region" aria-label="${e(answer.title)} result table"><table class="assistant-result-table"><thead><tr>${answer.headers.map(header => `<th scope="col">${e(header)}</th>`).join('')}</tr></thead><tbody>${answer.rows.map(row => `<tr>${row.map(cell => `<td>${e(cell)}</td>`).join('')}</tr>`).join('')}</tbody></table></div>` : ''}${answer.href ? `<a class="assistant-answer-link" href="${answer.href}">${e(answer.link)}${S.icon('arrow-right')}</a>` : ''}<p class="assistant-answer-source">${e(answer.source)}</p>`;
  }

  function initAssistant(root) {
    const input = root.querySelector('#assistant-input');
    const send = root.querySelector('#assistant-send');
    const messages = root.querySelector('#assistant-messages');
    const history = root.querySelector('#assistant-history-count');
    const context = root.querySelector('#assistant-data-context');
    const welcome = messages.innerHTML;
    let exchanges = 0;
    function updateContext(changed = false) {
      context.textContent = `${S.state.products.length} products / ${S.state.invoices.length} invoice records / browser-local data.${changed && exchanges ? ' Workspace changed; ask again for a fresh snapshot. Earlier answers are unchanged.' : ' Answers are read-only snapshots, not live model responses.'}`;
    }
    function ask(value) {
      const prompt = value.trim();
      if (!prompt) { input.focus(); return; }
      if (prompt.length > 600) { S.toast('Keep questions to 600 characters or fewer.', 'warning'); input.focus(); return; }
      const answer = assistantAnswer(prompt);
      if (!exchanges) {
        messages.innerHTML = '';
        messages.hidden = false;
        history.parentElement.hidden = false;
        root.classList.add('has-conversation');
      }
      const stamp = time();
      messages.insertAdjacentHTML('beforeend', `<article class="assistant-message assistant-message-user"><span class="assistant-message-avatar" aria-hidden="true">${e(S.initials(S.state.settings.ownerName))}</span><div class="assistant-message-body"><div class="assistant-message-meta"><strong>You</strong><span>${e(stamp)}</span></div><p>${e(prompt)}</p></div></article><article class="assistant-message assistant-message-reply"><span class="assistant-message-avatar" aria-hidden="true">${S.icon('sparkles')}</span><div class="assistant-message-body"><div class="assistant-message-meta"><strong>SmartStock preview</strong><span>${e(stamp)} / scripted response</span></div>${answerMarkup(answer)}</div></article>`);
      exchanges += 1;
      while (messages.children.length > 16) messages.firstElementChild.remove();
      history.textContent = `${exchanges > 8 ? 'Latest 8' : exchanges} ${exchanges === 1 ? 'interaction' : 'interactions'} / this tab only`;
      input.value = '';
      send.disabled = true;
      updateContext();
      input.focus({ preventScroll: true });
      messages.lastElementChild.scrollIntoView({ block: 'nearest', behavior: window.matchMedia('(prefers-reduced-motion: reduce)').matches ? 'auto' : 'smooth' });
    }
    root.querySelector('#assistant-form').addEventListener('submit', event => { event.preventDefault(); ask(input.value); });
    input.addEventListener('input', () => { send.disabled = !input.value.trim(); });
    input.addEventListener('keydown', event => {
      if (event.key === 'Enter' && !event.shiftKey && !event.isComposing) { event.preventDefault(); ask(input.value); }
    });
    root.querySelectorAll('[data-assistant-prompt]').forEach(button => button.addEventListener('click', () => ask(button.dataset.assistantPrompt)));
    root.querySelector('#assistant-reset').addEventListener('click', () => {
      exchanges = 0;
      messages.innerHTML = welcome;
      messages.hidden = true;
      history.parentElement.hidden = true;
      root.classList.remove('has-conversation');
      history.textContent = 'This tab only';
      input.value = '';
      send.disabled = true;
      updateContext();
      input.focus();
      S.toast('The conversation in this tab was cleared.');
    });
    S.onChange(() => updateContext(true));
    messages.hidden = true;
    history.parentElement.hidden = true;
    updateContext();
  }

  S.ready(() => {
    const ids = { reports: 'reports-page', 'smart-reorder': 'reorder-page', 'ai-assistant': 'assistant-page' };
    const root = document.getElementById(ids[S.page]);
    if (!root || root.dataset.insightsBound) return;
    root.dataset.insightsBound = 'true';
    if (S.page === 'reports') initReports(root);
    else if (S.page === 'smart-reorder') initReorder(root);
    else initAssistant(root);
  });
})();
