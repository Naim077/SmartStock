/* SmartStock inventory and commerce. All confirmations update this browser's demo only. */
(() => {
  'use strict';
  const SS = window.SS;
  if (!SS || !['products', 'stock', 'purchases', 'sales'].includes(SS.page)) return;
  const e = SS.escape;
  const byId = id => document.getElementById(id);
  const value = id => byId(id)?.value || '';
  const text = (id, content) => { const node = byId(id); if (node) node.textContent = content; };
  const html = (id, content) => { const node = byId(id); if (node) node.innerHTML = content; };
  const statMoney = (id, total) => html(id, `<span class="currency">BDT </span>${SS.number(total, 2)}`);
  const sum = (rows, fn) => rows.reduce((total, row) => total + fn(row), 0);
  const matches = (query, ...parts) => parts.join(' ').toLowerCase().includes(query.trim().toLowerCase());
  const active = product => product.status === 'active';
  const expired = product => !!product.expiry && product.expiry < SS.today();
  const inventoryValue = () => sum(SS.state.products, product => product.stock * product.cost);
  const productName = (id, state = SS.state) => SS.product(id, state)?.name || 'Unavailable product';
  const stockBadge = product => SS.badge(active(product) ? SS.stockStatus(product) : 'archived');
  const localNotice = '<div class="notice notice-info">Confirming changes this browser-local demo only. No supplier message, real payment, or server transaction is sent.</div>';
  const ui = { pages: {}, stockTab: 'inventory', lineSerial: 0, adjustmentSnapshot: null, saleQuotes: new Map(), modalOrigin: null };

  function requireValue(condition, message) {
    if (!condition) throw new Error(message);
  }
  function quantity(raw, label = 'Quantity', minimum = 1) {
    const result = Number(raw);
    requireValue(String(raw ?? '').trim() !== '' && Number.isSafeInteger(result) && result >= minimum && result <= 10000000,
      `${label} must be a whole number from ${minimum} to 10,000,000.`);
    return result;
  }
  function amount(raw, label, minimum = 0, maximum = 1000000000) {
    const result = Number(raw);
    requireValue(String(raw ?? '').trim() !== '' && Number.isFinite(result) && result >= minimum && result <= maximum,
      `${label} must be between ${minimum} and ${SS.number(maximum, 2)}.`);
    requireValue(result === SS.round(result), `${label} may have at most two decimal places.`);
    return SS.round(result);
  }
  function requiredText(raw, label, maximum = 120) {
    const result = String(raw ?? '').trim();
    requireValue(result.length > 0 && result.length <= maximum, `${label} is required (up to ${maximum} characters).`);
    return result;
  }
  function iconButton(action, icon, label, id) {
    return `<button type="button" class="icon-button" data-action="${e(action)}" data-id="${e(id)}" aria-label="${e(label)}" title="${e(label)}">${SS.icon(icon)}</button>`;
  }
  function button(action, label, id = '', kind = 'secondary') {
    return `<button type="button" class="button button-${e(kind)} button-sm" data-action="${e(action)}" data-id="${e(id)}">${e(label)}</button>`;
  }
  function option(id, label, selected) {
    return `<option value="${e(id)}"${String(id) === String(selected) ? ' selected' : ''}>${e(label)}</option>`;
  }
  function summary(label, content, className = '') {
    return `<div class="summary-row"><span class="text-muted">${e(label)}</span><strong class="${e(className)}">${e(content)}</strong></div>`;
  }
  function paginate(key, rows, noun, size = 8) {
    const pages = Math.max(1, Math.ceil(rows.length / size));
    ui.pages[key] = Math.max(1, Math.min(ui.pages[key] || 1, pages));
    const start = (ui.pages[key] - 1) * size;
    text(`${key}-range`, rows.length ? `Showing ${start + 1}-${Math.min(start + size, rows.length)} of ${SS.number(rows.length)} ${noun}` : `No ${noun} match these filters`);
    text(`${key}-page`, `Page ${ui.pages[key]} of ${pages}`);
    const previous = document.querySelector(`[data-action="${key}-prev"]`);
    const next = document.querySelector(`[data-action="${key}-next"]`);
    if (previous) previous.disabled = ui.pages[key] <= 1;
    if (next) next.disabled = ui.pages[key] >= pages;
    return rows.slice(start, start + size);
  }
  function setOptions(id, rows, prompt) {
    const node = byId(id);
    if (!node) return;
    const selected = node.value;
    const choices = [...rows];
    if (selected && !choices.some(row => String(row.id) === selected)) choices.push({ id: selected, name: selected });
    const markup = option('', prompt, selected) + choices.map(row => option(row.id, row.name, selected)).join('');
    if (node.innerHTML !== markup) node.innerHTML = markup;
    node.value = selected;
  }
  function addMovement(state, product, type, change, reference, note, unitCost) {
    state.movements.unshift({ id: SS.uid('movement'), productId: product.id, date: SS.today(), type,
      quantity: change, balance: product.stock, reference, note, unitCost });
  }
  function latestMovement(state, id) {
    return state.movements.find(movement => movement.productId === id)?.id || '';
  }
  function productRows() {
    const query = value('products-search');
    const category = value('products-category');
    const status = value('products-status');
    return SS.state.products.filter(product => matches(query, product.name, product.sku, product.barcode, product.category)
      && (!category || product.category === category)
      && (!status || (['active', 'archived'].includes(status) ? product.status === status : active(product) && SS.stockStatus(product) === status)));
  }
  function renderProducts() {
    const products = SS.state.products;
    const categories = [...new Set(products.map(product => product.category))].sort();
    setOptions('products-category', categories.map(name => ({ id: name, name })), 'All categories');
    text('products-total', SS.number(products.length));
    text('products-categories', `Across ${categories.length} categories`);
    text('products-active', SS.number(products.filter(active).length));
    text('products-low', SS.number(products.filter(product => active(product) && SS.stockStatus(product) !== 'healthy').length));
    statMoney('products-value', inventoryValue());
    const filtered = productRows();
    text('products-count', `${SS.number(filtered.length)} products`);
    html('products-rows', paginate('products', filtered, 'products').map(product => `<tr>
      <td>${SS.productCell(product)}</td><td>${e(product.category)}</td><td><span class="font-medium">${SS.number(product.stock)}</span> <span class="text-muted">${e(product.unit)}</span></td>
      <td>${SS.money(product.price)}</td><td>${SS.money(product.cost)}</td><td>${stockBadge(product)}</td>
      <td><div class="row-actions">${iconButton('product-detail', 'eye', `View ${product.name}`, product.id)}${iconButton('product-edit', 'edit', `Edit ${product.name}`, product.id)}${iconButton('product-archive', active(product) ? 'trash' : 'refresh', `${active(product) ? 'Archive' : 'Restore'} ${product.name}`, product.id)}</div></td>
    </tr>`).join('') || SS.emptyRow(7));
  }
  function productDetail(id) {
    const product = SS.product(id);
    requireValue(product, 'This product is no longer available.');
    const supplier = SS.supplier(product.supplierId);
    const movements = SS.state.movements.filter(row => row.productId === id).slice(0, 5);
    SS.openModal({ title: product.name, wide: true, body: `<div class="stack">
      <div class="inline">${SS.productCell(product)}${stockBadge(product)}${expired(product) ? SS.badge('out', 'Expired') : ''}</div>
      <div class="form-grid"><div class="stack">${summary('SKU', product.sku)}${summary('Category', product.category)}${summary('Barcode', product.barcode || 'Not set')}${summary('Preferred supplier', supplier?.name || 'Not assigned')}${summary('Expiry date', product.expiry ? SS.date(product.expiry) : 'Not tracked')}</div>
      <div class="stack">${summary('On hand', `${SS.number(product.stock)} ${product.unit}`)}${summary('Reorder level', SS.number(product.reorder))}${summary('Selling price', SS.money(product.price))}${summary('Default purchase cost', SS.money(product.purchaseCost))}${summary('Actual weighted cost / unit', SS.money(product.cost))}${summary('Inventory value', SS.money(product.stock * product.cost))}</div></div>
      <p class="text-muted">${e(product.description || 'No description added yet.')}</p>
      <div><h3 class="card-title">Latest stock movements</h3><p class="card-subtitle">Seed rows are opening snapshots, not reconstructed invoice history.</p></div>
      <div class="table-wrap"><table class="data-table"><thead><tr><th scope="col">Date</th><th scope="col">Reference</th><th scope="col">Change</th><th scope="col">Balance after</th></tr></thead><tbody>${movements.map(row => `<tr><td>${e(SS.date(row.date))}</td><td>${e(row.reference)}</td><td class="${row.quantity < 0 ? 'text-danger' : 'text-success'}">${row.quantity > 0 ? '+' : ''}${SS.number(row.quantity)}</td><td>${SS.number(row.balance)}</td></tr>`).join('') || SS.emptyRow(4, 'No recorded movements for this product yet.')}</tbody></table></div>
      <div class="page-actions">${button('product-edit', 'Edit catalog details', id)}<a href="stock.html" class="button button-secondary button-sm">Stock workspace</a></div>
    </div>` });
  }
  function productForm(id = '') {
    const existing = id ? SS.product(id) : null;
    requireValue(!id || existing, 'This product no longer exists.');
    const suppliers = SS.state.suppliers.filter(supplier => supplier.status === 'active');
    requireValue(suppliers.length, 'Add or restore an active supplier before adding a product.');
    const product = existing || { name: '', sku: '', barcode: '', category: 'Electronics', unit: 'piece', supplierId: suppliers[0].id, purchaseCost: 0, price: 0, reorder: 10, expiry: '', description: '' };
    const editable = ['name', 'sku', 'barcode', 'category', 'unit', 'supplierId', 'purchaseCost', 'price', 'reorder', 'expiry', 'description'];
    const fingerprint = row => JSON.stringify(editable.map(key => row[key]));
    const snapshot = existing ? fingerprint(existing) : '';
    const newId = SS.uid('product');
    const categories = [...new Set(['Electronics', 'Stationery', 'Lifestyle', 'Groceries', 'Household', ...SS.state.products.map(row => row.category)])].sort();
    SS.openModal({ title: existing ? 'Edit product' : 'Add a product', wide: true, submitLabel: existing ? 'Confirm catalog changes' : 'Confirm new product', body: `<div class="stack">
      <div class="notice notice-info">${existing ? 'Stock and actual weighted cost are not catalog-editable. Changing default purchase cost only affects future order drafts.' : 'New products start with zero stock and zero weighted cost. Receive a purchase or approve a stock request to introduce inventory.'}</div>
      <div class="form-grid">
        <div class="field full"><label for="product-name">Product name</label><input id="product-name" name="name" value="${e(product.name)}" maxlength="120" required></div>
        <div class="field"><label for="product-sku">Unique SKU</label><input id="product-sku" name="sku" value="${e(product.sku)}" maxlength="40" required></div>
        <div class="field"><label for="product-barcode">Barcode <span class="text-muted">(optional)</span></label><input id="product-barcode" name="barcode" value="${e(product.barcode)}" maxlength="64"></div>
        <div class="field"><label for="product-category">Category</label><select id="product-category" name="category" required>${categories.map(name => option(name, name, product.category)).join('')}</select></div>
        <div class="field"><label for="product-unit">Unit name</label><input id="product-unit" name="unit" value="${e(product.unit)}" maxlength="30" required><small class="text-muted">Stock is recorded in whole units.</small></div>
        <div class="field full"><label for="product-supplier">Preferred supplier</label><select id="product-supplier" name="supplierId" required>${option('', 'Choose an active supplier', product.supplierId)}${suppliers.map(supplier => option(supplier.id, supplier.name, product.supplierId)).join('')}</select></div>
        <div class="field"><label for="product-purchase-cost">Default purchase cost (BDT)</label><input id="product-purchase-cost" name="purchaseCost" type="number" min="0" max="1000000000" step="0.01" value="${e(product.purchaseCost)}" required></div>
        <div class="field"><label for="product-price">Selling price (BDT)</label><input id="product-price" name="price" type="number" min="0.01" max="1000000000" step="0.01" value="${e(product.price)}" required></div>
        <div class="field"><label for="product-reorder">Reorder level</label><input id="product-reorder" name="reorder" type="number" min="0" max="10000000" step="1" value="${e(product.reorder)}" required></div>
        <div class="field"><label for="product-expiry">Expiry date <span class="text-muted">(optional)</span></label><input id="product-expiry" name="expiry" type="date" value="${e(product.expiry)}"></div>
        <div class="field full"><label for="product-description">Description <span class="text-muted">(optional)</span></label><textarea id="product-description" name="description" rows="3" maxlength="1000">${e(product.description)}</textarea></div>
      </div>${localNotice}</div>`, onSubmit: data => {
      const fields = { name: requiredText(data.get('name'), 'Product name'), sku: requiredText(data.get('sku'), 'SKU', 40).toUpperCase(),
        barcode: String(data.get('barcode') || '').trim(), category: requiredText(data.get('category'), 'Category'), unit: requiredText(data.get('unit'), 'Unit', 30),
        supplierId: String(data.get('supplierId') || ''), purchaseCost: amount(data.get('purchaseCost'), 'Default purchase cost'),
        price: amount(data.get('price'), 'Selling price', 0.01), reorder: quantity(data.get('reorder'), 'Reorder level', 0),
        expiry: String(data.get('expiry') || ''), description: String(data.get('description') || '').trim() };
      requireValue(fields.barcode.length <= 64 && fields.description.length <= 1000, 'Barcode or description is too long.');
      SS.update(state => {
        requireValue(SS.supplier(fields.supplierId, state)?.status === 'active', 'Choose a supplier that is still active.');
        requireValue(!state.products.some(row => row.id !== id && row.sku.trim().toLowerCase() === fields.sku.toLowerCase()), 'This SKU is already used, including archived products. Choose a unique SKU.');
        if (id) {
          const target = SS.product(id, state);
          requireValue(target && fingerprint(target) === snapshot, 'Catalog details changed in another window. Close and reopen this editor.');
          Object.assign(target, fields);
        } else {
          requireValue(!SS.product(newId, state), 'This product has already been added.');
          state.products.unshift({ id: newId, ...fields, stock: 0, cost: 0, dailyDemand: 0, status: 'active' });
        }
      }, existing ? 'Product updated' : 'Product added', 'Products', fields.sku);
      SS.toast(existing ? 'Catalog details updated. Stock and weighted cost were not changed.' : 'Product added with zero stock.');
      return true;
    } });
  }
  function archiveProduct(id) {
    const product = SS.product(id);
    requireValue(product, 'This product is no longer available.');
    const wasActive = active(product);
    SS.confirm(`${wasActive ? 'Archive' : 'Restore'} ${product.name}? ${wasActive ? 'It will be excluded from new sales and new purchase drafts. Existing stock, receipts, and history are retained.' : 'It will be available for new transactions again.'}`, () => {
      SS.update(state => {
        const current = SS.product(id, state);
        requireValue(current && active(current) === wasActive, 'Product status changed. Reopen this action.');
        current.status = wasActive ? 'archived' : 'active';
      }, wasActive ? 'Product archived' : 'Product restored', 'Products', product.sku);
      SS.toast(wasActive ? 'Product archived. Stock and history are retained.' : 'Product restored.');
    }, { title: wasActive ? 'Archive product' : 'Restore product', label: wasActive ? 'Confirm archive' : 'Confirm restore', danger: wasActive });
  }
  function exportProducts() {
    SS.downloadCSV('smartstock-products.csv', [['SKU', 'Name', 'Category', 'Status', 'Stock', 'Unit', 'Reorder level', 'Default purchase cost', 'Weighted cost', 'Selling price', 'Stock value', 'Supplier', 'Expiry'],
      ...productRows().map(row => [row.sku, row.name, row.category, row.status, row.stock, row.unit, row.reorder, row.purchaseCost, row.cost, row.price, SS.round(row.stock * row.cost), SS.supplier(row.supplierId)?.name || '', row.expiry])]);
  }

  function inventoryRows() {
    const status = value('stock-status');
    const expiry = value('stock-expiry');
    return SS.state.products.filter(product => matches(value('stock-search'), product.name, product.sku, product.category)
      && (!status || (status === 'archived' ? !active(product) : active(product) && SS.stockStatus(product) === status))
      && (!expiry || (expiry === 'none' ? !product.expiry : expiry === 'expired' ? expired(product) : product.expiry && !expired(product) && product.expiry <= SS.daysFromToday(SS.state.settings.expiryDays || 30))));
  }
  function ledgerRows() {
    return SS.state.movements.filter(row => matches(value('ledger-search'), productName(row.productId), SS.product(row.productId)?.sku, row.reference, row.note)
      && (!value('ledger-type') || row.type === value('ledger-type')));
  }
  function adjustmentRows() {
    return SS.state.adjustments.filter(row => matches(value('adjustments-search'), productName(row.productId), row.number, row.reason)
      && (!value('adjustments-status') || row.status === value('adjustments-status')));
  }
  function renderStock() {
    const products = SS.state.products;
    const pending = SS.state.adjustments.filter(row => row.status === 'pending').length;
    text('stock-units', SS.number(sum(products, product => product.stock)));
    statMoney('stock-value', inventoryValue());
    text('stock-low', SS.number(products.filter(product => active(product) && SS.stockStatus(product) !== 'healthy').length));
    text('stock-out', `Including ${products.filter(product => active(product) && product.stock === 0).length} out-of-stock products`);
    text('stock-pending', SS.number(pending));
    text('stock-request-count', SS.number(pending));
    html('stock-rows', paginate('inventory', inventoryRows(), 'products').map(product => `<tr><td>${SS.productCell(product)}</td><td class="font-medium">${SS.number(product.stock)} <span class="text-muted">${e(product.unit)}</span></td><td>${SS.number(product.reorder)}</td><td>${SS.money(product.cost)}</td><td>${SS.money(product.stock * product.cost)}</td><td>${product.expiry ? `${e(SS.date(product.expiry))}${expired(product) ? `<span class="cell-subtitle">${SS.badge('out', 'Expired')}</span>` : product.expiry <= SS.daysFromToday(SS.state.settings.expiryDays || 30) ? `<span class="cell-subtitle">${SS.badge('low', 'Expiring soon')}</span>` : ''}` : '<span class="text-muted">Not tracked</span>'}</td><td>${stockBadge(product)}</td><td>${iconButton('product-detail', 'eye', `View ${product.name}`, product.id)}</td></tr>`).join('') || SS.emptyRow(8));
    const movementNames = { opening: 'Opening snapshot', receipt: 'Purchase receipt', sale: 'Sale', return: 'Sales return', adjustment: 'Adjustment' };
    html('ledger-rows', paginate('ledger', ledgerRows(), 'movements').map(row => `<tr><td>${e(SS.date(row.date))}</td><td>${SS.product(row.productId) ? SS.productCell(SS.product(row.productId)) : e(productName(row.productId))}</td><td>${SS.badge(row.type === 'opening' ? 'draft' : 'info', movementNames[row.type] || row.type)}</td><td class="${row.quantity < 0 ? 'text-danger' : 'text-success'}">${row.quantity > 0 ? '+' : ''}${SS.number(row.quantity)}</td><td>${SS.number(row.balance)}</td><td><span class="cell-title">${e(row.reference)}</span><span class="cell-subtitle">${e(row.note)}</span></td></tr>`).join('') || SS.emptyRow(6));
    html('adjustments-rows', paginate('adjustments', adjustmentRows(), 'requests').map(row => `<tr><td><span class="cell-title">${e(row.number)}</span><span class="cell-subtitle">${e(SS.date(row.date))} / ${row.type === 'count' ? 'Count' : 'Adjustment'}</span></td><td>${SS.product(row.productId) ? SS.productCell(SS.product(row.productId)) : e(productName(row.productId))}</td><td>${SS.number(row.expectedStock)}</td><td class="${row.quantity < 0 ? 'text-danger' : 'text-success'}">${row.quantity > 0 ? '+' : ''}${SS.number(row.quantity)}</td><td>${e(row.reason)}</td><td>${SS.badge(row.status)}</td><td>${button('adjustment-detail', row.status === 'pending' ? 'Review' : 'Details', row.id)}</td></tr>`).join('') || SS.emptyRow(7));
  }
  function setStockTab(tab, focus = false) {
    requireValue(['inventory', 'ledger', 'adjustments'].includes(tab), 'Unknown stock view.');
    ui.stockTab = tab;
    document.querySelectorAll('[data-action="stock-tab"]').forEach(node => {
      const selected = node.dataset.tab === tab;
      node.classList.toggle('is-active', selected);
      node.setAttribute('aria-selected', String(selected));
      node.tabIndex = selected ? 0 : -1;
      byId(node.getAttribute('aria-controls')).hidden = !selected;
      if (selected && focus) node.focus();
    });
  }
  function adjustmentSnapshot() {
    const product = SS.product(value('adjustment-product'));
    ui.adjustmentSnapshot = product ? { productId: product.id, stock: product.stock, movementId: latestMovement(SS.state, product.id) } : null;
    text('adjustment-current', product ? `${SS.number(product.stock)} ${product.unit} on hand at request preparation` : 'Select a product to capture current stock.');
    updateAdjustmentPreview();
  }
  function updateAdjustmentPreview() {
    const snapshot = ui.adjustmentSnapshot;
    const isCount = value('adjustment-type') === 'count';
    const input = byId('adjustment-quantity');
    if (!input) return;
    text('adjustment-quantity-label', isCount ? 'Physical count (whole units)' : 'Quantity change (use a minus for removal)');
    if (isCount) input.min = '0'; else input.min = '-10000000';
    const raw = Number(input.value);
    const delta = snapshot ? (isCount ? raw - snapshot.stock : raw) : 0;
    text('adjustment-result', snapshot && Number.isFinite(raw) ? `Proposed change: ${delta > 0 ? '+' : ''}${SS.number(delta)}. Stock after approval: ${SS.number(snapshot.stock + delta)}.` : 'Enter a quantity to review the change.');
    const needsCost = !!snapshot && snapshot.stock === 0 && delta > 0;
    byId('adjustment-cost-field').hidden = !needsCost;
    byId('adjustment-unit-cost').required = needsCost;
    byId('adjustment-unit-cost').disabled = !needsCost;
  }
  function newAdjustment() {
    requireValue(SS.state.products.length, 'Add a product before creating a stock request.');
    const requestId = SS.uid('adjustment');
    SS.openModal({ title: 'Request a stock change', submitLabel: 'Confirm local request', wide: true, body: `<div class="stack">
      <div class="notice notice-info">Requests await a separate review. They do not change stock now. Other than opening stock from zero, adjustments use the current weighted cost.</div>
      <div class="form-grid"><div class="field"><label for="adjustment-type">Request type</label><select id="adjustment-type" name="type"><option value="adjustment">Quantity adjustment</option><option value="count">Physical stock count</option></select></div>
      <div class="field"><label for="adjustment-product">Product</label><select id="adjustment-product" name="productId" required>${SS.state.products.map(product => option(product.id, `${product.name} / ${product.sku}${active(product) ? '' : ' (archived)'}`, '')).join('')}</select></div>
      <div class="field full"><p class="text-muted" id="adjustment-current" aria-live="polite"></p></div>
      <div class="field"><label for="adjustment-quantity" id="adjustment-quantity-label">Quantity change (use a minus for removal)</label><input id="adjustment-quantity" name="quantity" type="number" min="-10000000" max="10000000" step="1" required></div>
      <div class="field" id="adjustment-cost-field" hidden><label for="adjustment-unit-cost">Proposed opening unit cost (BDT)</label><input id="adjustment-unit-cost" name="unitCost" type="number" min="0.01" max="1000000000" step="0.01" disabled><small class="text-muted">Required for adding stock from zero. The reviewer must approve this cost.</small></div>
      <div class="field full"><p class="text-muted" id="adjustment-result" aria-live="polite"></p></div>
      <div class="field full"><label for="adjustment-reason">Reason for this request</label><textarea id="adjustment-reason" name="reason" rows="3" maxlength="500" required placeholder="For example: two units damaged during handling"></textarea></div></div>${localNotice}</div>`, onSubmit: data => {
      const snapshot = ui.adjustmentSnapshot;
      requireValue(snapshot && snapshot.productId === data.get('productId'), 'Select a product to capture its stock snapshot.');
      const isCount = data.get('type') === 'count';
      const entered = quantity(data.get('quantity'), isCount ? 'Physical count' : 'Quantity change', isCount ? 0 : -10000000);
      const delta = isCount ? entered - snapshot.stock : entered;
      requireValue(delta !== 0, 'This request would not change stock. Enter a nonzero adjustment or a different count.');
      requireValue(snapshot.stock + delta >= 0, 'This request would make stock negative.');
      const unitCost = snapshot.stock === 0 && delta > 0 ? amount(data.get('unitCost'), 'Opening unit cost', 0.01) : null;
      const reason = requiredText(data.get('reason'), 'Reason', 500);
      SS.update(state => {
        const product = SS.product(snapshot.productId, state);
        requireValue(product && product.stock === snapshot.stock && latestMovement(state, product.id) === snapshot.movementId, 'Stock changed while preparing this request. Reselect the product to capture a new snapshot.');
        requireValue(!state.adjustments.some(row => row.id === requestId), 'This request has already been submitted.');
        state.adjustments.unshift({ id: requestId, number: SS.nextNumber(state, 'adjustment'), productId: product.id, date: SS.today(),
          type: isCount ? 'count' : 'adjustment', counted: isCount ? entered : null, quantity: delta, expectedStock: snapshot.stock,
          expectedMovementId: snapshot.movementId, unitCost, reason, status: 'pending' });
      }, 'Stock change requested', 'Stock', `${productName(snapshot.productId)}: ${delta > 0 ? '+' : ''}${delta}`);
      setStockTab('adjustments');
      SS.toast('Request submitted. Stock is unchanged until approval.');
      return true;
    } });
    adjustmentSnapshot();
  }
  function adjustmentOverview(request) {
    const product = SS.product(request.productId);
    const changed = !product || product.stock !== request.expectedStock || (Object.hasOwn(request, 'expectedMovementId') && latestMovement(SS.state, request.productId) !== request.expectedMovementId);
    return `<div class="stack"><div class="inline">${SS.badge(request.status)}<strong>${e(productName(request.productId))}</strong></div>
      ${summary('Request', request.number)}${summary('Type', request.type === 'count' ? 'Physical stock count' : 'Quantity adjustment')}${summary('Stock snapshot', SS.number(request.expectedStock))}${summary('Current stock', product ? SS.number(product.stock) : 'Product unavailable')}${summary('Requested change', `${request.quantity > 0 ? '+' : ''}${SS.number(request.quantity)}`)}${summary('Proposed stock', SS.number(request.expectedStock + request.quantity))}
      ${request.unitCost != null ? summary('Proposed opening unit cost', SS.money(request.unitCost)) : ''}<div><h3 class="card-title">Reason</h3><p>${e(request.reason)}</p></div>
      ${request.reviewNote ? `<p><strong>Review note:</strong> ${e(request.reviewNote)}</p>` : ''}
      ${request.status === 'pending' && changed ? '<div class="notice notice-warning">The stock snapshot is stale. Reject this request and submit a fresh one; approval will be blocked.</div>' : ''}</div>`;
  }
  function adjustmentDetail(id) {
    const request = SS.state.adjustments.find(row => row.id === id);
    requireValue(request, 'This request no longer exists.');
    SS.openModal({ title: `Stock request ${request.number}`, body: `<div class="stack">${adjustmentOverview(request)}${request.status === 'pending' ? `<div class="page-actions">${button('adjustment-reject', 'Reject request', id, 'danger')}${button('adjustment-approve', 'Review approval', id, 'primary')}</div>` : ''}</div>` });
  }
  function reviewAdjustment(id, approve) {
    const request = SS.state.adjustments.find(row => row.id === id);
    requireValue(request?.status === 'pending', 'Only a pending request can be reviewed.');
    const needsCost = approve && request.expectedStock === 0 && request.quantity > 0;
    SS.openModal({ title: approve ? 'Approve stock change' : 'Reject stock request', danger: !approve,
      submitLabel: approve ? 'Confirm approval & update stock' : 'Confirm rejection', body: `<div class="stack">${adjustmentOverview(request)}
      ${needsCost ? `<div class="field"><label for="review-unit-cost">Approved opening unit cost (BDT)</label><input id="review-unit-cost" name="unitCost" type="number" min="0.01" max="1000000000" step="0.01" value="${e(request.unitCost ?? '')}" required></div>` : ''}
      <div class="field"><label for="review-note">Review note${approve ? ' (optional)' : ''}</label><textarea id="review-note" name="reviewNote" rows="2" maxlength="500"${approve ? '' : ' required'}></textarea></div>${localNotice}</div>`, onSubmit: data => {
      const reviewNote = approve ? String(data.get('reviewNote') || '').trim() : requiredText(data.get('reviewNote'), 'Rejection reason', 500);
      const approvedCost = needsCost ? amount(data.get('unitCost'), 'Approved unit cost', 0.01) : null;
      SS.update(state => {
        const current = state.adjustments.find(row => row.id === id);
        requireValue(current?.status === 'pending', 'This request has already been reviewed.');
        if (approve) {
          const product = SS.product(current.productId, state);
          requireValue(product && product.stock === current.expectedStock, 'Stock no longer matches this snapshot. Reject the request and submit a fresh one.');
          requireValue(!Object.hasOwn(current, 'expectedMovementId') || latestMovement(state, product.id) === current.expectedMovementId, 'Stock moved after this snapshot. Reject the request and submit a fresh one.');
          const change = quantity(current.quantity, 'Requested change', -10000000);
          requireValue(change !== 0 && product.stock + change >= 0, 'This adjustment would create invalid or negative stock.');
          const cost = product.stock === 0 && change > 0 ? amount(approvedCost, 'Approved opening cost', 0.01) : product.cost;
          product.stock += change;
          product.cost = product.stock === 0 ? 0 : cost;
          current.approvedUnitCost = cost;
          addMovement(state, product, 'adjustment', change, current.number, current.reason, cost);
        }
        current.status = approve ? 'approved' : 'rejected';
        current.reviewedAt = SS.today();
        current.reviewNote = reviewNote;
      }, approve ? 'Stock change approved' : 'Stock request rejected', 'Stock', request.number);
      SS.toast(approve ? 'Stock updated and movement recorded.' : 'Request rejected. Stock was not changed.');
      return true;
    } });
  }
  function exportStock() {
    if (ui.stockTab === 'ledger') {
      SS.downloadCSV('smartstock-stock-ledger.csv', [['Date', 'Product', 'Type', 'Quantity', 'Balance after', 'Reference', 'Note'], ...ledgerRows().map(row => [row.date, productName(row.productId), row.type, row.quantity, row.balance, row.reference, row.note])]);
    } else if (ui.stockTab === 'adjustments') {
      SS.downloadCSV('smartstock-adjustments.csv', [['Request', 'Date', 'Product', 'Type', 'Snapshot', 'Change', 'Reason', 'Status', 'Review note'], ...adjustmentRows().map(row => [row.number, row.date, productName(row.productId), row.type || 'adjustment', row.expectedStock, row.quantity, row.reason, row.status, row.reviewNote || ''])]);
    } else {
      SS.downloadCSV('smartstock-inventory.csv', [['SKU', 'Product', 'Stock', 'Reorder level', 'Weighted cost', 'Stock value', 'Expiry', 'Status'], ...inventoryRows().map(row => [row.sku, row.name, row.stock, row.reorder, row.cost, SS.round(row.stock * row.cost), row.expiry, active(row) ? SS.stockStatus(row) : 'archived'])]);
    }
  }

  const openOrder = order => ['approved', 'partial'].includes(order.status);
  const remainingUnits = item => Math.max(0, item.quantity - item.received);
  function purchaseRows() {
    return SS.state.purchaseOrders.filter(order => matches(value('purchases-search'), order.number, SS.supplier(order.supplierId)?.name, ...order.items.map(item => productName(item.productId)))
      && (!value('purchases-status') || order.status === value('purchases-status'))
      && (!value('purchases-supplier') || order.supplierId === value('purchases-supplier')))
      .sort((a, b) => b.date.localeCompare(a.date) || b.number.localeCompare(a.number, undefined, { numeric: true }));
  }
  function renderPurchases() {
    const orders = SS.state.purchaseOrders;
    setOptions('purchases-supplier', SS.state.suppliers, 'All suppliers');
    text('purchases-drafts', SS.number(orders.filter(order => order.status === 'draft').length));
    text('purchases-open', SS.number(orders.filter(openOrder).length));
    statMoney('purchases-incoming', sum(orders.filter(openOrder), order => sum(order.items, item => remainingUnits(item) * item.unitCost)));
    statMoney('purchases-payable', sum(SS.state.suppliers, supplier => supplier.payable));
    const filtered = purchaseRows();
    text('purchases-count', `${SS.number(filtered.length)} orders`);
    html('purchases-rows', paginate('purchases', filtered, 'orders').map(order => `<tr>
      <td><span class="cell-title">${e(order.number)}</span><span class="cell-subtitle">${e(SS.date(order.date))} / ${order.items.length} product${order.items.length === 1 ? '' : 's'}</span></td>
      <td><div class="item-cell"><span class="item-icon tone-green">${SS.icon('truck')}</span><span class="cell-title">${e(SS.supplier(order.supplierId)?.name || 'Unavailable supplier')}</span></div></td>
      <td>${SS.number(sum(order.items, item => item.received))} / ${SS.number(sum(order.items, item => item.quantity))} units</td><td class="font-medium">${SS.money(SS.orderTotal(order))}</td>
      <td>${e(SS.date(order.expected))}${openOrder(order) && order.expected < SS.today() ? `<span class="cell-subtitle">${SS.badge('overdue', 'Past expected date')}</span>` : ''}</td><td>${SS.badge(order.status, order.status === 'partial' ? 'Partially received' : undefined)}</td>
      <td><div class="row-actions">${iconButton('purchase-detail', 'eye', `View ${order.number}`, order.id)}${order.status === 'draft' ? button('purchase-approve', 'Review', order.id) : openOrder(order) ? button('purchase-receive', 'Receive', order.id) : ''}</div></td></tr>`).join('') || SS.emptyRow(7));
  }
  function purchaseProducts() {
    return SS.state.products.filter(product => active(product) && product.supplierId === value('purchase-supplier'));
  }
  function appendPurchaseLine() {
    const container = byId('purchase-lines');
    if (!container) return;
    requireValue(container.children.length < 25, 'A local draft can contain up to 25 product lines.');
    const index = ++ui.lineSerial;
    const row = document.createElement('div');
    row.className = 'card';
    row.dataset.purchaseLine = String(index);
    row.innerHTML = `<div class="card-body stack"><div class="form-grid">
      <div class="field full"><label for="purchase-product-${index}">Product</label><select id="purchase-product-${index}" name="poProduct" data-purchase-product required>${option('', 'Select a supplier product', '')}${purchaseProducts().map(product => option(product.id, `${product.name} / ${product.sku}`, '')).join('')}</select></div>
      <div class="field"><label for="purchase-quantity-${index}">Units to order</label><input id="purchase-quantity-${index}" name="poQuantity" type="number" min="1" max="10000000" step="1" value="1" required></div>
      <div class="field"><label for="purchase-cost-${index}">Agreed unit cost (BDT)</label><input id="purchase-cost-${index}" name="poCost" type="number" min="0.01" max="1000000000" step="0.01" required></div>
      </div><div class="summary-row"><span class="text-muted" data-purchase-line-total>Line total: BDT 0</span><button type="button" class="button button-ghost button-sm" data-action="purchase-line-remove" aria-label="Remove purchase line ${index}">${SS.icon('trash')}Remove line</button></div></div>`;
    container.append(row);
    updatePurchaseTotal();
  }
  function updatePurchaseTotal() {
    const lines = [...document.querySelectorAll('[data-purchase-line]')];
    let total = 0;
    lines.forEach(row => {
      const lineTotal = Math.max(0, Number(row.querySelector('[name="poQuantity"]').value) || 0) * Math.max(0, Number(row.querySelector('[name="poCost"]').value) || 0);
      total += lineTotal;
      row.querySelector('[data-purchase-line-total]').textContent = `Line total: ${SS.money(lineTotal)}`;
      row.querySelector('[data-action="purchase-line-remove"]').disabled = lines.length <= 1;
    });
    text('purchase-draft-total', SS.money(total));
  }
  function newPurchase() {
    const suppliers = SS.state.suppliers.filter(supplier => supplier.status === 'active');
    requireValue(suppliers.length, 'Add an active supplier before preparing a purchase.');
    const orderId = SS.uid('purchase');
    SS.openModal({ title: 'New purchase order', wide: true, submitLabel: 'Confirm local draft', body: `<div class="stack">
      <div class="form-grid"><div class="field"><label for="purchase-supplier">Supplier</label><select id="purchase-supplier" name="supplierId" required>${suppliers.map(supplier => option(supplier.id, supplier.name, suppliers[0].id)).join('')}</select><small class="text-muted">Changing supplier clears the draft lines.</small></div>
      <div class="field"><label for="purchase-expected">Expected delivery</label><input id="purchase-expected" name="expected" type="date" min="${e(SS.today())}" value="${e(SS.daysFromToday(3))}" required></div></div>
      <div><h3 class="card-title">Order lines</h3><p class="card-subtitle">Active products from this preferred supplier. Prices below are purchase costs, not selling prices.</p></div>
      <div id="purchase-lines" class="stack"></div><div class="page-actions"><button type="button" class="button button-secondary button-sm" data-action="purchase-line-add">${SS.icon('plus')}Add product line</button></div>
      <div class="summary-row"><span>Draft order total</span><strong id="purchase-draft-total" aria-live="polite">BDT 0</strong></div>
      <div class="field"><label for="purchase-notes">Notes (optional)</label><textarea id="purchase-notes" name="notes" rows="2" maxlength="1000"></textarea></div>${localNotice}</div>`, onSubmit: (data, form) => {
      const supplierId = String(data.get('supplierId') || '');
      const expected = String(data.get('expected') || '');
      requireValue(/^\d{4}-\d{2}-\d{2}$/.test(expected) && expected >= SS.today(), 'Choose an expected delivery date on or after today.');
      const items = [...form.querySelectorAll('[data-purchase-line]')].map(row => ({ productId: row.querySelector('[name="poProduct"]').value,
        quantity: quantity(row.querySelector('[name="poQuantity"]').value, 'Order quantity'), received: 0,
        unitCost: amount(row.querySelector('[name="poCost"]').value, 'Purchase unit cost', 0.01) }));
      requireValue(items.length > 0 && items.length <= 25, 'Add between 1 and 25 product lines.');
      requireValue(new Set(items.map(item => item.productId)).size === items.length, 'Combine duplicate products into a single order line.');
      requireValue(sum(items, item => item.quantity * item.unitCost) <= 1000000000, 'Keep the local order value at or below BDT 1,000,000,000.');
      SS.update(state => {
        requireValue(SS.supplier(supplierId, state)?.status === 'active', 'The supplier is no longer active. Reopen this draft.');
        items.forEach(item => {
          const product = SS.product(item.productId, state);
          requireValue(product && active(product) && product.supplierId === supplierId, 'Each line needs an active product from the selected supplier.');
        });
        requireValue(!state.purchaseOrders.some(order => order.id === orderId), 'This draft has already been created.');
        state.purchaseOrders.unshift({ id: orderId, number: SS.nextNumber(state, 'purchase'), supplierId, date: SS.today(), expected,
          status: 'draft', items, paid: 0, notes: String(data.get('notes') || '').trim().slice(0, 1000) });
      }, 'Purchase draft created', 'Purchases', `${items.length} product lines; no stock posted`);
      SS.toast('Draft created. Review and approve it before receiving stock.');
      return true;
    } });
    appendPurchaseLine();
  }
  function purchaseMarkup(order) {
    const supplier = SS.supplier(order.supplierId);
    return `<div class="stack"><div class="inline"><strong>${e(order.number)}</strong>${SS.badge(order.status, order.status === 'partial' ? 'Partially received' : undefined)}</div>
      ${summary('Supplier', supplier?.name || 'Unavailable supplier')}${summary('Order date', SS.date(order.date))}${summary('Expected delivery', SS.date(order.expected))}
      <div class="table-wrap"><table class="data-table"><thead><tr><th scope="col">Product</th><th scope="col">Ordered</th><th scope="col">Received</th><th scope="col">Remaining</th><th scope="col">Unit cost</th><th scope="col">Order value</th></tr></thead><tbody>${order.items.map(item => `<tr><td><span class="cell-title">${e(productName(item.productId))}</span><span class="cell-subtitle">${e(SS.product(item.productId)?.sku || item.productId)}</span></td><td>${SS.number(item.quantity)}</td><td>${SS.number(item.received)}</td><td>${SS.number(remainingUnits(item))}</td><td>${SS.money(item.unitCost)}</td><td>${SS.money(item.quantity * item.unitCost)}</td></tr>`).join('')}</tbody></table></div>
      ${summary('Full order value', SS.money(SS.orderTotal(order)))}${summary('Received value', SS.money(sum(order.items, item => item.received * item.unitCost)))}
      ${order.notes ? `<div><h3 class="card-title">Notes</h3><p>${e(order.notes)}</p></div>` : ''}
      ${(order.receipts || []).length ? `<div><h3 class="card-title">Receipt history</h3>${order.receipts.map(receipt => summary(`${SS.date(receipt.date)} / ${SS.number(sum(receipt.items, item => item.quantity))} units`, SS.money(receipt.total))).join('')}</div>` : '<p class="text-muted">No local receipts recorded for this order.</p>'}
      <p class="text-muted">An order is not a supplier payment. Receipts add accepted goods to inventory and supplier payable. This is not a transmitted order or tax document.</p></div>`;
  }
  function purchaseDetail(id) {
    const order = SS.state.purchaseOrders.find(row => row.id === id);
    requireValue(order, 'This purchase order no longer exists.');
    const canCancel = ['draft', 'approved'].includes(order.status) && order.items.every(item => item.received === 0);
    SS.openModal({ title: order.number, wide: true, body: `<div class="stack">${purchaseMarkup(order)}<div class="page-actions">
      ${button('purchase-print', 'Print order', id)}${button('purchase-export-one', 'Export lines', id)}
      ${canCancel ? button('purchase-cancel', 'Cancel order', id, 'danger') : ''}${order.status === 'draft' ? button('purchase-approve', 'Review approval', id, 'primary') : ''}${openOrder(order) ? button('purchase-receive', 'Receive goods', id, 'primary') : ''}
      </div>${order.status === 'partial' ? '<div class="notice">Partially received orders cannot be cancelled in this demo. Posted receipts remain in stock and the ledger.</div>' : ''}</div>` });
  }
  function approvePurchase(id) {
    const order = SS.state.purchaseOrders.find(row => row.id === id);
    requireValue(order?.status === 'draft', 'Only draft orders can be approved.');
    SS.openModal({ title: `Approve ${order.number}`, wide: true, submitLabel: 'Confirm local approval', body: `<div class="stack">${purchaseMarkup(order)}${localNotice}</div>`, onSubmit: () => {
      SS.update(state => {
        const current = state.purchaseOrders.find(row => row.id === id);
        requireValue(current?.status === 'draft', 'This order is no longer a draft.');
        requireValue(SS.supplier(current.supplierId, state)?.status === 'active', 'Restore the supplier before approving this order.');
        requireValue(current.items.length > 0, 'An order must have at least one product.');
        current.items.forEach(item => {
          const product = SS.product(item.productId, state);
          requireValue(product && active(product), 'Restore archived products before approving this order.');
          quantity(item.quantity, 'Ordered quantity');
          amount(item.unitCost, 'Purchase unit cost');
          requireValue(item.received === 0, 'A draft cannot already contain received stock.');
        });
        current.status = 'approved';
        current.approvedAt = SS.today();
      }, 'Purchase order approved', 'Purchases', order.number);
      SS.toast('Order approved locally. Stock and payable are unchanged until receipt.');
      return true;
    } });
  }
  function cancelPurchase(id) {
    const order = SS.state.purchaseOrders.find(row => row.id === id);
    requireValue(order, 'This purchase order no longer exists.');
    SS.confirm(`Cancel ${order.number}? Only an unreceived draft or approved order can be cancelled. Its history is retained.`, () => {
      SS.update(state => {
        const current = state.purchaseOrders.find(row => row.id === id);
        requireValue(current && ['draft', 'approved'].includes(current.status) && current.items.every(item => item.received === 0), 'This order has changed or contains received goods and cannot be cancelled.');
        current.status = 'cancelled';
      }, 'Purchase order cancelled', 'Purchases', order.number);
      SS.toast('Order cancelled. Stock and payable were not changed.');
    }, { title: 'Cancel purchase order', label: 'Confirm cancellation', danger: true });
  }
  function updateReceiptTotal() {
    const inputs = [...document.querySelectorAll('[data-receipt-quantity]')];
    text('receipt-total', SS.money(sum(inputs, input => Math.max(0, Number(input.value) || 0) * Number(input.dataset.cost))));
  }
  function receivePurchase(id) {
    const order = SS.state.purchaseOrders.find(row => row.id === id);
    requireValue(order && openOrder(order), 'Only approved or partially received orders can accept a receipt.');
    const snapshot = order.items.map(item => ({ ...item }));
    SS.openModal({ title: `Receive ${order.number}`, wide: true, submitLabel: 'Confirm receipt & update stock', body: `<div class="stack">
      <div class="notice notice-info">Record only the units physically received. Remaining quantities are checked again on confirmation; another receipt makes this form stale.</div>
      <div class="table-wrap"><table class="data-table"><thead><tr><th scope="col">Product</th><th scope="col">Ordered</th><th scope="col">Received</th><th scope="col">Remaining</th><th scope="col">Unit cost</th><th scope="col">Receive now</th></tr></thead><tbody>${snapshot.map((item, index) => `<tr><td>${e(productName(item.productId))}</td><td>${SS.number(item.quantity)}</td><td>${SS.number(item.received)}</td><td>${SS.number(remainingUnits(item))}</td><td>${SS.money(item.unitCost)}</td><td><div class="field"><label for="receive-${index}">Units for line ${index + 1}</label><input id="receive-${index}" name="receive-${index}" type="number" min="0" max="${remainingUnits(item)}" step="1" value="0" data-receipt-quantity data-cost="${e(item.unitCost)}"${remainingUnits(item) === 0 ? ' readonly' : ''} required></div></td></tr>`).join('')}</tbody></table></div>
      <div class="summary-row"><span>Receipt value added to payable</span><strong id="receipt-total" aria-live="polite">BDT 0</strong></div>
      <p class="text-muted">Weighted cost uses current on-hand quantity and actual cost plus these received costs. Product-level expiry dates remain in the catalog; this demo does not track delivery batches.</p>${localNotice}</div>`, onSubmit: data => {
      const quantities = snapshot.map((item, index) => quantity(data.get(`receive-${index}`), `Received quantity for line ${index + 1}`, 0));
      requireValue(quantities.some(count => count > 0), 'Enter at least one received unit.');
      SS.update(state => {
        const current = state.purchaseOrders.find(row => row.id === id);
        requireValue(current && openOrder(current), 'This order has already been completed or cancelled.');
        requireValue(current.items.length === snapshot.length && current.items.every((item, index) => item.productId === snapshot[index].productId && item.quantity === snapshot[index].quantity && item.unitCost === snapshot[index].unitCost && item.received === snapshot[index].received), 'This order was received or changed while the form was open. Close it and review the remaining quantities.');
        const supplier = SS.supplier(current.supplierId, state);
        requireValue(supplier, 'The original supplier is no longer available.');
        const receipt = { id: SS.uid('receipt'), date: SS.today(), total: 0, items: [] };
        current.items.forEach((item, index) => {
          const count = quantities[index];
          requireValue(count <= remainingUnits(item), 'Received quantity exceeds the remaining order quantity.');
          if (!count) return;
          const product = SS.product(item.productId, state);
          requireValue(product, 'A product on this order no longer exists.');
          const unitCost = amount(item.unitCost, 'Agreed purchase cost');
          const totalStock = product.stock + count;
          requireValue(Number.isSafeInteger(totalStock), 'The resulting stock quantity is too large.');
          product.cost = SS.round((product.stock * product.cost + count * unitCost) / totalStock);
          product.stock = totalStock;
          item.received += count;
          receipt.total += count * unitCost;
          receipt.items.push({ productId: item.productId, quantity: count, unitCost });
          addMovement(state, product, 'receipt', count, current.number, `Local purchase receipt ${receipt.id}`, unitCost);
        });
        receipt.total = SS.round(receipt.total);
        supplier.payable = SS.round(supplier.payable + receipt.total);
        (current.receipts || (current.receipts = [])).unshift(receipt);
        current.status = current.items.every(item => item.received === item.quantity) ? 'received' : 'partial';
      }, 'Purchase receipt posted', 'Purchases', order.number);
      SS.toast('Receipt posted. Stock, weighted cost, ledger, and supplier payable updated.');
      return true;
    } });
  }
  function exportPurchases(id = '') {
    if (id) {
      const order = SS.state.purchaseOrders.find(row => row.id === id);
      requireValue(order, 'This order no longer exists.');
      SS.downloadCSV(`${order.number}-lines.csv`, [['Order', 'Supplier', 'Status', 'SKU', 'Product', 'Ordered', 'Received', 'Remaining', 'Unit cost', 'Line value'], ...order.items.map(item => [order.number, SS.supplier(order.supplierId)?.name || '', order.status, SS.product(item.productId)?.sku || '', productName(item.productId), item.quantity, item.received, remainingUnits(item), item.unitCost, SS.round(item.quantity * item.unitCost)])]);
    } else {
      SS.downloadCSV('smartstock-purchase-orders.csv', [['Order', 'Date', 'Supplier', 'Status', 'Expected', 'Order value', 'Ordered units', 'Received units', 'Received value'], ...purchaseRows().map(order => [order.number, order.date, SS.supplier(order.supplierId)?.name || '', order.status, order.expected, SS.orderTotal(order), sum(order.items, item => item.quantity), sum(order.items, item => item.received), SS.round(sum(order.items, item => item.received * item.unitCost))])]);
    }
  }
  function printDocument(title, body) {
    const popup = window.open('', '_blank', 'width=1000,height=760');
    requireValue(popup, 'The print window was blocked. Allow pop-ups for this local page and try again.');
    popup.opener = null;
    popup.document.open();
    popup.document.write(`<!doctype html><html lang="en"><head><meta charset="utf-8"><title>${e(title)}</title><style>body{font:14px/1.6 "Segoe UI",Arial,sans-serif;color:#24362e;margin:36px auto;padding:0 24px;max-width:960px}h1{font-size:28px;margin-bottom:4px}h2,h3{margin:16px 0 6px}.text-muted,.cell-subtitle{color:#63736a}.cell-title,.cell-subtitle{display:block}.summary-row{display:flex;gap:24px;justify-content:space-between;padding:6px 0;border-bottom:1px solid #e4e9e4}.inline{display:flex;gap:16px;align-items:center}.status{font-size:12px;border:1px solid #d7dfd9;border-radius:12px;padding:2px 8px}table{width:100%;border-collapse:collapse;margin:18px 0}td,th{text-align:left;border-bottom:1px solid #d7dfd9;padding:9px 7px}th{background:#f5f7f4}small{color:#63736a}.notice{border:1px solid #d7dfd9;padding:12px;margin:18px 0}thead{display:table-header-group}tr{break-inside:avoid}@media print{body{margin:0;max-width:none;padding:0}@page{margin:16mm}a{color:inherit;text-decoration:none}}</style></head><body><header><p>${e(SS.state.settings.businessName)}</p><h1>${e(title)}</h1><p class="text-muted">${e(SS.state.settings.address)}<br>${e(SS.state.settings.phone)}</p><small>Browser-local demonstration. Not a transmitted order, tax invoice, or proof of a real payment.</small></header>${body}</body></html>`);
    popup.document.close();
    popup.focus();
    popup.print();
  }

  function saleRows() {
    const status = value('sales-status');
    const period = value('sales-period');
    const start = period === 'today' ? SS.today() : period === 'week' ? SS.daysFromToday(-6) : period === 'month' ? SS.daysFromToday(-29) : '';
    return SS.state.invoices.filter(invoice => matches(value('sales-search'), invoice.number, SS.customer(invoice.customerId)?.name, ...invoice.items.map(item => productName(item.productId)))
      && (!status || (status === 'returns' ? invoice.returns.length > 0 : status === 'partial' ? SS.invoiceDue(invoice) > 0 : status === 'paid' ? SS.invoiceDue(invoice) === 0 && invoice.status !== 'returned' : invoice.status === status))
      && (!start || invoice.date >= start && invoice.date <= SS.today()))
      .sort((a, b) => b.date.localeCompare(a.date) || b.number.localeCompare(a.number, undefined, { numeric: true }));
  }
  function renderSales() {
    const invoices = SS.state.invoices;
    const today = invoices.filter(invoice => invoice.date === SS.today());
    const creditsToday = sum(invoices, invoice => sum(invoice.returns.filter(entry => entry.date === SS.today()), entry => entry.credit));
    statMoney('sales-today', sum(today, invoice => invoice.total) - creditsToday);
    text('sales-invoices', SS.number(today.length));
    text('sales-total-count', `${SS.number(invoices.length)} invoices in this workspace`);
    statMoney('sales-due', sum(SS.state.customers, customer => customer.due));
    statMoney('sales-returns', creditsToday);
    const filtered = saleRows();
    text('sales-count', `${SS.number(filtered.length)} invoices`);
    html('sales-rows', paginate('sales', filtered, 'invoices').map(invoice => `<tr><td><span class="cell-title">${e(invoice.number)}</span><span class="cell-subtitle">${e(SS.date(invoice.date))}</span></td>
      <td>${e(SS.customer(invoice.customerId)?.name || 'Unavailable customer')}</td><td class="font-medium">${SS.money(invoice.total)}${SS.invoiceCredit(invoice) ? `<span class="cell-subtitle">${SS.money(SS.invoiceCredit(invoice))} returned</span>` : ''}</td>
      <td class="${SS.invoiceDue(invoice) ? 'text-danger' : 'text-muted'}">${SS.money(SS.invoiceDue(invoice))}</td><td>${SS.badge(invoice.status === 'returned' ? 'returned' : SS.invoiceDue(invoice) > 0 ? 'partial' : 'paid', SS.invoiceDue(invoice) > 0 ? 'Balance due' : undefined)}${invoice.returns.length && invoice.status !== 'returned' ? `<span class="cell-subtitle">${SS.badge('returned', 'Part return')}</span>` : ''}</td>
      <td><div class="row-actions">${iconButton('sale-detail', 'eye', `View ${invoice.number}`, invoice.id)}${iconButton('sale-print', 'printer', `Print ${invoice.number}`, invoice.id)}</div></td></tr>`).join('') || SS.emptyRow(6));
    const watch = SS.state.products.filter(product => active(product) && product.stock > 0 && (SS.stockStatus(product) === 'low' || expired(product))).sort((a, b) => a.stock - b.stock).slice(0, 3);
    html('sales-stock-watch', watch.map(product => `<div class="summary-row"><span>${e(product.name)}</span>${SS.badge(expired(product) ? 'out' : 'low', expired(product) ? 'Expired' : `${SS.number(product.stock)} left`)}</div>`).join('') || '<p class="text-muted">No low-stock products currently on hand.</p>');
  }
  function discountLimit(state = SS.state) {
    return Math.max(0, Math.min(100, Number(state.settings.discountLimit) || 0));
  }
  function allocateDiscount(items, discount) {
    const lineCents = items.map(item => Math.round(item.quantity * item.unitPrice * 100));
    const totalCents = sum(lineCents, cents => cents);
    const discountCents = Math.min(totalCents, Math.max(0, Math.round(discount * 100)));
    if (!totalCents) return lineCents.map(() => 0);
    const shares = lineCents.map(cents => cents / totalCents * discountCents);
    const allocated = shares.map(Math.floor);
    // Largest remainders keep cent allocations nonnegative, including tiny lines.
    const ranked = shares.map((share, index) => ({ index, remainder: share - allocated[index] })).sort((a, b) => b.remainder - a.remainder || a.index - b.index);
    const left = discountCents - sum(allocated, cents => cents);
    for (let index = 0; index < left; index++) allocated[ranked[index].index]++;
    return allocated.map(cents => cents / 100);
  }
  function appendSaleLine() {
    const container = byId('sale-lines');
    if (!container) return;
    requireValue(container.children.length < 25, 'A local sale can contain up to 25 product lines.');
    const index = ++ui.lineSerial;
    const available = SS.state.products.filter(product => active(product) && product.stock > 0 && !expired(product));
    const row = document.createElement('div');
    row.className = 'card';
    row.dataset.saleLine = String(index);
    row.innerHTML = `<div class="card-body stack"><div class="form-grid">
      <div class="field full"><label for="sale-product-${index}">Product</label><select id="sale-product-${index}" name="saleProduct" data-sale-product required>${option('', 'Choose an available product', '')}${available.map(product => option(product.id, `${product.name} / ${product.sku} / ${SS.number(product.stock)} left`, '')).join('')}</select></div>
      <div class="field"><label for="sale-quantity-${index}">Quantity</label><input id="sale-quantity-${index}" name="saleQuantity" type="number" min="1" max="10000000" step="1" value="1" required></div>
      <div class="field"><label for="sale-price-${index}">Unit selling price (BDT)</label><input id="sale-price-${index}" name="salePrice" type="number" value="0" readonly aria-describedby="sale-availability-${index}"><small class="text-muted" id="sale-availability-${index}" data-sale-availability>Choose a product to see availability.</small></div>
      </div><div class="summary-row"><span class="text-muted" data-sale-line-total>Line total: BDT 0</span><button type="button" class="button button-ghost button-sm" data-action="sale-line-remove" aria-label="Remove sale line ${index}">${SS.icon('trash')}Remove line</button></div></div>`;
    container.append(row);
    updateSaleTotal();
  }
  function saleDraft(strict = false) {
    const items = [...document.querySelectorAll('[data-sale-line]')].map(row => {
      const productId = row.querySelector('[name="saleProduct"]').value;
      const quote = ui.saleQuotes.get(row.dataset.saleLine);
      if (strict) requireValue(quote && quote.productId === productId, 'Select a product on every sale line.');
      const count = strict ? quantity(row.querySelector('[name="saleQuantity"]').value, 'Sale quantity') : Math.max(0, Number(row.querySelector('[name="saleQuantity"]').value) || 0);
      return { productId, quantity: count, unitPrice: quote?.price || 0 };
    });
    const subtotal = SS.round(sum(items, item => item.quantity * item.unitPrice));
    const percent = strict ? amount(value('sale-discount'), 'Discount percentage', 0, discountLimit()) : Math.max(0, Math.min(100, Number(value('sale-discount')) || 0));
    const discount = SS.round(subtotal * percent / 100);
    const total = SS.round(subtotal - discount);
    const paid = value('sale-payment-mode') === 'full' ? total : strict ? amount(value('sale-paid'), 'Amount paid', 0, total) : Math.max(0, Number(value('sale-paid')) || 0);
    return { items, subtotal, percent, discount, total, paid, due: SS.round(total - paid) };
  }
  function updateSaleTotal() {
    if (!byId('sale-lines')) return;
    const customer = SS.customer(value('sale-customer'));
    const walkIn = customer?.id === 'C001';
    const mode = byId('sale-payment-mode');
    if (walkIn) mode.value = 'full';
    mode.querySelector('option[value="partial"]').disabled = walkIn;
    const draft = saleDraft();
    const paidInput = byId('sale-paid');
    paidInput.readOnly = mode.value === 'full';
    paidInput.max = String(Math.max(0, draft.total));
    if (mode.value === 'full') paidInput.value = draft.total.toFixed(2);
    text('sale-subtotal', SS.money(draft.subtotal));
    text('sale-discount-total', SS.money(draft.discount));
    text('sale-grand-total', SS.money(draft.total));
    text('sale-balance', SS.money(Math.max(0, draft.due)));
    text('sale-credit-note', customer ? walkIn ? 'Walk-in sales must be fully paid. Payment is recorded locally, not processed.' : `Current balance: ${SS.money(customer.due)}. Credit limit: ${SS.money(customer.creditLimit)}. Available credit: ${SS.money(Math.max(0, customer.creditLimit - customer.due))}.` : 'Select a customer.');
    const lines = [...document.querySelectorAll('[data-sale-line]')];
    lines.forEach((row, index) => {
      row.querySelector('[data-sale-line-total]').textContent = `Line total: ${SS.money(draft.items[index].quantity * draft.items[index].unitPrice)}`;
      row.querySelector('[data-action="sale-line-remove"]').disabled = lines.length <= 1;
    });
  }
  function newSale() {
    const customers = SS.state.customers.filter(customer => customer.status === 'active');
    requireValue(customers.length, 'Add an active customer before recording a sale.');
    requireValue(SS.state.products.some(product => active(product) && product.stock > 0 && !expired(product)), 'There is no active, unexpired stock to sell. Receive inventory first.');
    const invoiceId = SS.uid('invoice');
    ui.saleQuotes.clear();
    SS.openModal({ title: 'New sale / local checkout', wide: true, submitLabel: 'Confirm sale & record payment', body: `<div class="stack">
      <div class="form-grid"><div class="field"><label for="sale-customer">Customer</label><select id="sale-customer" name="customerId" required>${customers.map(customer => option(customer.id, customer.name, 'C001')).join('')}</select></div>
      <div class="field"><label for="sale-method">Payment method (record only)</label><select id="sale-method" name="method"><option>Cash</option><option>Card</option><option>Bank transfer</option></select></div></div>
      <div id="sale-lines" class="stack"></div><div class="page-actions"><button type="button" class="button button-secondary button-sm" data-action="sale-line-add">${SS.icon('plus')}Add product line</button></div>
      <div class="form-grid"><div class="field"><label for="sale-discount">Discount (%)</label><input id="sale-discount" name="discountPercent" type="number" min="0" max="${discountLimit()}" step="0.01" value="0" required><small class="text-muted">Current local limit: ${SS.number(discountLimit(), 2)}%.</small></div>
      <div class="field"><label for="sale-payment-mode">Payment terms</label><select id="sale-payment-mode" name="paymentMode"><option value="full">Fully paid now</option><option value="partial">Part payment / customer credit</option></select></div>
      <div class="field"><label for="sale-paid">Amount paid (BDT)</label><input id="sale-paid" name="paid" type="number" min="0" max="0" step="0.01" value="0" readonly required></div><div class="field"><p class="text-muted" id="sale-credit-note" aria-live="polite"></p></div></div>
      <div class="stack"><div class="summary-row"><span>Subtotal</span><strong id="sale-subtotal">BDT 0</strong></div><div class="summary-row"><span>Discount</span><strong id="sale-discount-total">BDT 0</strong></div><div class="summary-row"><span>Total due</span><strong id="sale-grand-total" aria-live="polite">BDT 0</strong></div><div class="summary-row"><span>Customer balance added</span><strong id="sale-balance">BDT 0</strong></div></div>
      <div class="notice notice-warning">Confirmation reduces stock immediately. Prices are checked again; expired, archived, or insufficient inventory is blocked. Credit is available only within a named customer's limit.</div>${localNotice}</div>`, onSubmit: data => {
      const draft = saleDraft(true);
      requireValue(draft.items.length > 0 && draft.items.length <= 25, 'Add between 1 and 25 sale lines.');
      requireValue(new Set(draft.items.map(item => item.productId)).size === draft.items.length, 'Combine duplicate products into a single sale line.');
      requireValue(draft.subtotal > 0 && draft.subtotal <= 1000000000, 'The sale subtotal must be positive and at most BDT 1,000,000,000.');
      const customerId = String(data.get('customerId') || '');
      const method = String(data.get('method') || '');
      requireValue(['Cash', 'Card', 'Bank transfer'].includes(method), 'Choose a valid payment method.');
      let invoiceNumber = '';
      SS.update(state => {
        requireValue(!state.invoices.some(invoice => invoice.id === invoiceId), 'This sale has already been recorded.');
        const customer = SS.customer(customerId, state);
        requireValue(customer?.status === 'active', 'This customer is no longer active. Select an active customer.');
        requireValue(draft.percent <= discountLimit(state), 'The discount limit changed. Review the current settings and discount.');
        requireValue(draft.paid <= draft.total && draft.due >= 0, 'Payment cannot exceed the sale total.');
        requireValue(customerId !== 'C001' || draft.due === 0, 'Walk-in sales must be fully paid. Choose a named customer for credit.');
        requireValue(draft.due === 0 || SS.round(customer.due + draft.due) <= customer.creditLimit, 'This sale would exceed the customer credit limit. Increase the payment or reduce the basket.');
        const discounts = allocateDiscount(draft.items, draft.discount);
        invoiceNumber = SS.nextNumber(state, 'sale');
        const items = draft.items.map((item, index) => {
          const product = SS.product(item.productId, state);
          requireValue(product && active(product), 'A product is now archived or unavailable. Reopen checkout.');
          requireValue(!expired(product), `${product.name} has expired and cannot be sold.`);
          requireValue(item.quantity <= product.stock, `Only ${SS.number(product.stock)} units of ${product.name} remain. Reduce the quantity.`);
          requireValue(product.price === item.unitPrice, `The price of ${product.name} changed. Reselect the product to review its new price.`);
          const snapshot = { productId: product.id, quantity: item.quantity, unitPrice: product.price, unitCost: product.cost, discountAmount: discounts[index] };
          product.stock -= item.quantity;
          if (product.stock === 0) product.cost = 0;
          addMovement(state, product, 'sale', -item.quantity, invoiceNumber, 'Confirmed local sale', snapshot.unitCost);
          return snapshot;
        });
        state.invoices.unshift({ id: invoiceId, number: invoiceNumber, customerId, date: SS.today(), status: draft.due > 0 ? 'partial' : 'paid',
          items, discount: draft.discount, total: draft.total, paid: draft.paid, method: draft.paid === 0 && draft.due > 0 ? 'Credit' : method, returns: [] });
        customer.due = SS.round(customer.due + draft.due);
      }, 'Sale confirmed', 'Sales', `${draft.items.length} lines; ${SS.money(draft.total)}; local payment record only`);
      SS.toast(`${invoiceNumber} recorded. Stock and customer balance updated locally.`);
      return true;
    } });
    appendSaleLine();
  }
  function returnedQuantity(invoice, lineIndex) {
    return sum(invoice.returns || [], entry => sum(entry.items || [], item => item.lineIndex === lineIndex ? item.quantity : 0));
  }
  function lineDiscounts(invoice) {
    return invoice.items.every(item => Number.isFinite(item.discountAmount)) ? invoice.items.map(item => item.discountAmount) : allocateDiscount(invoice.items, invoice.discount);
  }
  function invoiceMarkup(invoice, printing = false) {
    const discounts = lineDiscounts(invoice);
    const customer = SS.customer(invoice.customerId);
    return `<div class="stack"><div class="inline"><strong>${e(invoice.number)}</strong>${SS.badge(invoice.status === 'returned' ? 'returned' : SS.invoiceDue(invoice) > 0 ? 'partial' : 'paid')}</div>
      ${summary('Customer', customer?.name || 'Unavailable customer')}${summary('Invoice date', SS.date(invoice.date))}${summary('Recorded payment method', invoice.method)}
      <div class="table-wrap"><table class="data-table"><thead><tr><th scope="col">Product</th><th scope="col">Sold</th><th scope="col">Returned</th><th scope="col">Unit price</th>${printing ? '' : '<th scope="col">Sale-time WAC</th>'}<th scope="col">Line discount</th><th scope="col">Net line</th></tr></thead><tbody>${invoice.items.map((item, index) => `<tr><td><span class="cell-title">${e(productName(item.productId))}</span><span class="cell-subtitle">${e(SS.product(item.productId)?.sku || item.productId)}</span></td><td>${SS.number(item.quantity)}</td><td>${SS.number(returnedQuantity(invoice, index))}</td><td>${SS.money(item.unitPrice)}</td>${printing ? '' : `<td>${SS.money(item.unitCost)}</td>`}<td>${SS.money(discounts[index])}</td><td>${SS.money(item.quantity * item.unitPrice - discounts[index])}</td></tr>`).join('')}</tbody></table></div>
      ${summary('Original subtotal', SS.money(sum(invoice.items, item => item.quantity * item.unitPrice)))}${summary('Original discount', SS.money(invoice.discount))}${summary('Original invoice total', SS.money(invoice.total))}${summary('Payment records to date', SS.money(invoice.paid))}${summary('Return credits', SS.money(SS.invoiceCredit(invoice)))}${summary('Remaining invoice balance', SS.money(SS.invoiceDue(invoice)))}
      ${invoice.returns.length ? `<div><h3 class="card-title">Return records</h3><p class="card-subtitle">Credits are recognized on the return date. Original sale amounts remain unchanged.</p>${invoice.returns.map(entry => `<div class="notice"><strong>${e(entry.number || entry.id)} / ${e(SS.date(entry.date))}</strong><p>${e(entry.reason)}</p>${summary('Units returned', SS.number(sum(entry.items, item => item.quantity)))}${summary('Credit', SS.money(entry.credit))}${summary('Invoice balance reduced', SS.money(entry.dueReduction))}${summary('Refund recorded (demo only)', SS.money(entry.refund))}</div>`).join('')}</div>` : ''}
      <p class="text-muted">No real payment or refund is executed. Original prices, costs, and discounts are retained for local return calculations.</p></div>`;
  }
  function saleDetail(id) {
    const invoice = SS.state.invoices.find(row => row.id === id);
    requireValue(invoice, 'This invoice no longer exists.');
    const canReturn = invoice.items.some((item, index) => returnedQuantity(invoice, index) < item.quantity);
    SS.openModal({ title: invoice.number, wide: true, body: `<div class="stack">${invoiceMarkup(invoice)}<div class="page-actions">${button('sale-print', 'Print invoice', id)}${button('sale-export-one', 'Export invoice lines', id)}${canReturn ? button('sale-return', 'Review a return', id) : ''}${SS.invoiceDue(invoice) > 0 ? '<a href="customers.html" class="button button-secondary button-sm">Customer payment records</a>' : ''}</div></div>` });
  }
  function returnPreview(invoice, counts) {
    const discounts = lineDiscounts(invoice);
    const items = counts.map((count, index) => {
      const original = invoice.items[index];
      const returned = returnedQuantity(invoice, index);
      const netCents = Math.round((original.quantity * original.unitPrice - discounts[index]) * 100);
      // Cumulative cent rounding gives the final returned unit any residual cent.
      const credit = (Math.round(netCents * (returned + count) / original.quantity) - Math.round(netCents * returned / original.quantity)) / 100;
      return { lineIndex: index, productId: original.productId, quantity: count, unitPrice: original.unitPrice, unitCost: original.unitCost, credit };
    }).filter(item => item.quantity > 0);
    const credit = SS.round(sum(items, item => item.credit));
    const dueReduction = Math.min(SS.invoiceDue(invoice), credit);
    return { items, credit, dueReduction, refund: SS.round(credit - dueReduction), restockValue: SS.round(sum(items, item => item.quantity * item.unitCost)) };
  }
  function updateReturnTotal() {
    const preview = byId('return-totals');
    if (!preview) return;
    const invoice = SS.state.invoices.find(row => row.id === preview.dataset.invoice);
    if (!invoice) return;
    const counts = invoice.items.map((item, index) => Math.max(0, Math.min(item.quantity - returnedQuantity(invoice, index), Math.floor(Number(value(`return-${index}`)) || 0))));
    const result = returnPreview(invoice, counts);
    preview.innerHTML = `${summary('Return credit (after original discount)', SS.money(result.credit))}${summary('Invoice balance reduced first', SS.money(result.dueReduction))}${summary('Refund to record locally', SS.money(result.refund))}`;
  }
  function returnSale(id) {
    const invoice = SS.state.invoices.find(row => row.id === id);
    requireValue(invoice, 'This invoice no longer exists.');
    requireValue(invoice.items.some((item, index) => returnedQuantity(invoice, index) < item.quantity), 'All quantities on this invoice have already been returned.');
    const snapshot = invoice.items.map((item, index) => returnedQuantity(invoice, index));
    const originalItems = JSON.stringify(invoice.items);
    const paidSnapshot = invoice.paid;
    SS.openModal({ title: `Return items / ${invoice.number}`, wide: true, submitLabel: 'Confirm return & local credit', body: `<div class="stack">
      <div class="notice notice-info">Choose the quantities being returned. Credit uses original selling prices less the original allocated discount. Stock is restored at the original sale-time cost, blended with current stock.</div>
      <div class="table-wrap"><table class="data-table"><thead><tr><th scope="col">Product</th><th scope="col">Sold</th><th scope="col">Already returned</th><th scope="col">Original price</th><th scope="col">Return now</th></tr></thead><tbody>${invoice.items.map((item, index) => `<tr><td>${e(productName(item.productId))}</td><td>${SS.number(item.quantity)}</td><td>${SS.number(snapshot[index])}</td><td>${SS.money(item.unitPrice)}</td><td><div class="field"><label for="return-${index}">Units for line ${index + 1}</label><input id="return-${index}" name="return-${index}" type="number" min="0" max="${item.quantity - snapshot[index]}" step="1" value="0" data-return-quantity${item.quantity === snapshot[index] ? ' readonly' : ''} required></div></td></tr>`).join('')}</tbody></table></div>
      <div id="return-totals" class="stack" data-invoice="${e(id)}" aria-live="polite"></div><div class="field"><label for="return-reason">Return reason</label><textarea id="return-reason" name="reason" rows="2" maxlength="500" required></textarea></div>
      <div class="notice notice-warning">This demo returns units to tracked stock only, without damage grading or batch handling. Expired or archived products remain blocked from checkout. Any refund is a local record, not a money transfer.</div>${localNotice}</div>`, onSubmit: data => {
      const counts = invoice.items.map((item, index) => quantity(data.get(`return-${index}`), `Return quantity for line ${index + 1}`, 0));
      requireValue(counts.some(count => count > 0), 'Enter at least one unit to return.');
      const reason = requiredText(data.get('reason'), 'Return reason', 500);
      SS.update(state => {
        const current = state.invoices.find(row => row.id === id);
        requireValue(current && JSON.stringify(current.items) === originalItems && current.paid === paidSnapshot && current.items.every((item, index) => returnedQuantity(current, index) === snapshot[index]), 'This invoice received a payment or return while the form was open. Close it and review the latest balance.');
        counts.forEach((count, index) => requireValue(count <= current.items[index].quantity - returnedQuantity(current, index), 'A return quantity exceeds the units remaining on the original sale.'));
        const customer = SS.customer(current.customerId, state);
        requireValue(customer, 'The original customer record is no longer available.');
        const result = returnPreview(current, counts);
        requireValue(customer.due >= result.dueReduction, 'The customer balance is inconsistent with this invoice. Review the customer account before returning.');
        const entry = { id: SS.uid('return'), number: `${current.number}-R${current.returns.length + 1}`, date: SS.today(), ...result, reason };
        result.items.forEach(item => {
          const product = SS.product(item.productId, state);
          requireValue(product, 'The original product no longer exists.');
          const totalStock = product.stock + item.quantity;
          requireValue(Number.isSafeInteger(totalStock), 'The resulting stock quantity is too large.');
          product.cost = SS.round((product.stock * product.cost + item.quantity * item.unitCost) / totalStock);
          product.stock = totalStock;
          addMovement(state, product, 'return', item.quantity, entry.number, reason, item.unitCost);
        });
        current.returns.push(entry);
        customer.due = SS.round(customer.due - result.dueReduction);
        current.status = current.items.every((item, index) => returnedQuantity(current, index) === item.quantity) ? 'returned' : SS.invoiceDue(current) > 0 ? 'partial' : 'paid';
      }, 'Sales return recorded', 'Sales', invoice.number);
      SS.toast('Return recorded. Stock and local credit updated; no money was transferred.');
      return true;
    } });
    updateReturnTotal();
  }
  function exportSales(id = '') {
    if (id) {
      const invoice = SS.state.invoices.find(row => row.id === id);
      requireValue(invoice, 'This invoice no longer exists.');
      const discounts = lineDiscounts(invoice);
      SS.downloadCSV(`${invoice.number}-lines.csv`, [['Invoice', 'Date', 'Customer', 'SKU', 'Product', 'Sold', 'Returned', 'Unit price', 'Sale-time unit cost', 'Line discount', 'Original net line'], ...invoice.items.map((item, index) => [invoice.number, invoice.date, SS.customer(invoice.customerId)?.name || '', SS.product(item.productId)?.sku || '', productName(item.productId), item.quantity, returnedQuantity(invoice, index), item.unitPrice, item.unitCost, discounts[index], SS.round(item.quantity * item.unitPrice - discounts[index])])]);
    } else {
      SS.downloadCSV('smartstock-invoices.csv', [['Invoice', 'Date', 'Customer', 'Status', 'Original total', 'Recorded payments', 'Return credit', 'Remaining due', 'Recorded refunds', 'Method'], ...saleRows().map(invoice => [invoice.number, invoice.date, SS.customer(invoice.customerId)?.name || '', invoice.status, invoice.total, invoice.paid, SS.invoiceCredit(invoice), SS.invoiceDue(invoice), SS.round(sum(invoice.returns, entry => entry.refund || 0)), invoice.method])]);
    }
  }

  function restoreActionFocus(origin) {
    if (!origin) return;
    const controls = [...document.querySelectorAll('button[data-action]')].filter(node => !node.disabled && !node.closest('dialog'));
    let target = origin.node.isConnected && !origin.node.disabled ? origin.node : controls.find(node => node.dataset.action === origin.action && node.dataset.id === origin.id);
    const page = /^(products|inventory|ledger|adjustments|purchases|sales)-(prev|next)$/.exec(origin.action);
    if (!target && page) target = controls.find(node => node.dataset.action === `${page[1]}-${page[2] === 'next' ? 'prev' : 'next'}`);
    if (!target) target = byId(SS.page === 'stock' ? ui.stockTab === 'inventory' ? 'stock-search' : `${ui.stockTab}-search` : `${SS.page}-search`);
    target?.focus({ preventScroll: true });
  }
  function render() {
    const focused = document.activeElement;
    const origin = focused?.matches('button[data-action]') ? { node: focused, action: focused.dataset.action, id: focused.dataset.id } : null;
    if (SS.page === 'products') renderProducts();
    else if (SS.page === 'stock') renderStock();
    else if (SS.page === 'purchases') renderPurchases();
    else if (SS.page === 'sales') renderSales();
    if (origin && (!focused.isConnected || focused.disabled)) restoreActionFocus(origin);
  }
  const actions = {
    'product-add': () => productForm(),
    'product-detail': node => productDetail(node.dataset.id),
    'product-edit': node => productForm(node.dataset.id),
    'product-archive': node => archiveProduct(node.dataset.id),
    'product-export': exportProducts,
    'stock-tab': node => setStockTab(node.dataset.tab),
    'stock-export': exportStock,
    'adjustment-add': newAdjustment,
    'adjustment-detail': node => adjustmentDetail(node.dataset.id),
    'adjustment-approve': node => reviewAdjustment(node.dataset.id, true),
    'adjustment-reject': node => reviewAdjustment(node.dataset.id, false),
    'purchase-add': newPurchase,
    'purchase-detail': node => purchaseDetail(node.dataset.id),
    'purchase-approve': node => approvePurchase(node.dataset.id),
    'purchase-cancel': node => cancelPurchase(node.dataset.id),
    'purchase-receive': node => receivePurchase(node.dataset.id),
    'purchase-export': () => exportPurchases(),
    'purchase-export-one': node => exportPurchases(node.dataset.id),
    'purchase-print': node => {
      const order = SS.state.purchaseOrders.find(row => row.id === node.dataset.id);
      requireValue(order, 'This order no longer exists.');
      printDocument(`Purchase order ${order.number}`, purchaseMarkup(order));
    },
    'purchase-line-add': appendPurchaseLine,
    'purchase-line-remove': node => {
      requireValue(document.querySelectorAll('[data-purchase-line]').length > 1, 'Keep at least one product line.');
      node.closest('[data-purchase-line]').remove();
      updatePurchaseTotal();
    },
    'sale-add': newSale,
    'sale-detail': node => saleDetail(node.dataset.id),
    'sale-return': node => returnSale(node.dataset.id),
    'sale-export': () => exportSales(),
    'sale-export-one': node => exportSales(node.dataset.id),
    'sale-print': node => {
      const invoice = SS.state.invoices.find(row => row.id === node.dataset.id);
      requireValue(invoice, 'This invoice no longer exists.');
      printDocument(`Invoice ${invoice.number}`, invoiceMarkup(invoice, true));
    },
    'sale-line-add': appendSaleLine,
    'sale-line-remove': node => {
      requireValue(document.querySelectorAll('[data-sale-line]').length > 1, 'Keep at least one product line.');
      const row = node.closest('[data-sale-line]');
      ui.saleQuotes.delete(row.dataset.saleLine);
      row.remove();
      updateSaleTotal();
    }
  };
  const filters = {
    'products-search': 'products', 'products-category': 'products', 'products-status': 'products',
    'stock-search': 'inventory', 'stock-status': 'inventory', 'stock-expiry': 'inventory',
    'ledger-search': 'ledger', 'ledger-type': 'ledger', 'adjustments-search': 'adjustments', 'adjustments-status': 'adjustments',
    'purchases-search': 'purchases', 'purchases-status': 'purchases', 'purchases-supplier': 'purchases',
    'sales-search': 'sales', 'sales-status': 'sales', 'sales-period': 'sales'
  };
  SS.ready(() => {
    if (SS.page === 'products' && byId('products-search')) byId('products-search').value = new URLSearchParams(location.search).get('q') || '';
    const dialog = byId('app-modal');
    dialog?.addEventListener('close', () => { restoreActionFocus(ui.modalOrigin); ui.modalOrigin = null; });
    document.addEventListener('click', event => {
      const node = event.target.closest('button[data-action]');
      if (!node || node.disabled) return;
      const action = node.dataset.action;
      const pageAction = /^(products|inventory|ledger|adjustments|purchases|sales)-(prev|next)$/.exec(action);
      if (!actions[action] && !pageAction) return;
      event.preventDefault();
      const wasOpen = dialog?.open;
      try {
        if (pageAction) {
          ui.pages[pageAction[1]] = (ui.pages[pageAction[1]] || 1) + (pageAction[2] === 'next' ? 1 : -1);
          render();
        } else actions[action](node);
        if (!wasOpen && dialog?.open) ui.modalOrigin = { node, action, id: node.dataset.id };
      } catch (error) { SS.toast(error.message || 'This action could not be completed.', 'warning'); }
    });
    document.addEventListener('input', event => {
      const node = event.target;
      if (filters[node.id] && node.tagName === 'INPUT') { ui.pages[filters[node.id]] = 1; render(); }
      if (node.id === 'adjustment-quantity') updateAdjustmentPreview();
      if (node.closest('[data-purchase-line]')) updatePurchaseTotal();
      if (node.hasAttribute('data-receipt-quantity')) updateReceiptTotal();
      if (node.closest('[data-sale-line]') || ['sale-discount', 'sale-paid'].includes(node.id)) updateSaleTotal();
      if (node.hasAttribute('data-return-quantity')) updateReturnTotal();
    });
    document.addEventListener('change', event => {
      const node = event.target;
      if (filters[node.id]) { ui.pages[filters[node.id]] = 1; render(); }
      if (node.id === 'adjustment-product') adjustmentSnapshot();
      if (node.id === 'adjustment-type') updateAdjustmentPreview();
      if (node.id === 'purchase-supplier') { byId('purchase-lines').replaceChildren(); appendPurchaseLine(); }
      if (node.hasAttribute('data-purchase-product')) {
        const product = SS.product(node.value);
        node.closest('[data-purchase-line]').querySelector('[name="poCost"]').value = product ? product.purchaseCost : '';
        updatePurchaseTotal();
      }
      if (node.hasAttribute('data-sale-product')) {
        const product = SS.product(node.value);
        const row = node.closest('[data-sale-line]');
        if (product) ui.saleQuotes.set(row.dataset.saleLine, { productId: product.id, price: product.price });
        else ui.saleQuotes.delete(row.dataset.saleLine);
        row.querySelector('[name="salePrice"]').value = product ? product.price : 0;
        row.querySelector('[name="saleQuantity"]').max = product ? product.stock : 10000000;
        row.querySelector('[data-sale-availability]').textContent = product ? `${SS.number(product.stock)} ${product.unit} available when selected. Rechecked on confirmation.` : 'Choose a product to see availability.';
        updateSaleTotal();
      }
      if (['sale-customer', 'sale-payment-mode'].includes(node.id)) updateSaleTotal();
    });
    document.addEventListener('keydown', event => {
      const tab = event.target.closest('[data-action="stock-tab"]');
      if (!tab || !['ArrowLeft', 'ArrowRight', 'Home', 'End'].includes(event.key)) return;
      const tabs = ['inventory', 'ledger', 'adjustments'];
      const current = tabs.indexOf(tab.dataset.tab);
      const index = event.key === 'Home' ? 0 : event.key === 'End' ? 2 : (current + (event.key === 'ArrowRight' ? 1 : -1) + tabs.length) % tabs.length;
      event.preventDefault();
      setStockTab(tabs[index], true);
    });
    SS.onChange(render);
    render();
  });
})();
