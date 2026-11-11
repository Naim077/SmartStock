/* SmartStock UI preview. Browser-local sample data, not an authenticated backend. */
(() => {
  'use strict';
  const KEY = 'smartstock-ui-preview-v1';
  const copy = value => JSON.parse(JSON.stringify(value));
  const today = () => daysFromToday(0);
  const daysFromToday = amount => {
    const date = new Date();
    date.setDate(date.getDate() + amount);
    return `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}-${String(date.getDate()).padStart(2, '0')}`;
  };
  const uid = (prefix = 'id') => `${prefix}-${Date.now().toString(36)}-${Math.random().toString(36).slice(2, 8)}`;
  const escape = value => String(value ?? '').replace(/[&<>"']/g, character => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[character]));
  const number = (value, decimals = 0) => Number(value || 0).toLocaleString('en-US', { maximumFractionDigits: decimals });
  const money = value => `BDT ${number(value, 2)}`;
  const round = value => Math.round((Number(value) + Number.EPSILON) * 100) / 100;

  function seed() {
    const products = [
      ['P1001', 'EL-001', 'Wireless Keyboard', 'Electronics', 42, 10, 1250, 1850, 'S001', 2.8],
      ['P1002', 'EL-002', 'USB-C Fast Charger', 'Electronics', 8, 15, 680, 990, 'S001', 3.6],
      ['P1003', 'ST-001', 'A5 Hardcover Notebook', 'Stationery', 120, 25, 145, 249, 'S002', 5.2],
      ['P1004', 'LS-001', 'Stainless Steel Bottle', 'Lifestyle', 34, 12, 520, 850, 'S003', 2.1],
      ['P1005', 'GR-001', 'Organic Arabica Coffee', 'Groceries', 12, 20, 580, 890, 'S004', 3.3],
      ['P1006', 'GR-002', 'Premium Jasmine Rice 5kg', 'Groceries', 65, 20, 520, 680, 'S004', 4.8],
      ['P1007', 'EL-003', 'Wireless Mouse', 'Electronics', 0, 15, 480, 790, 'S001', 2.3],
      ['P1008', 'HH-001', 'Liquid Hand Wash', 'Household', 56, 15, 140, 220, 'S003', 3],
      ['P1009', 'ST-002', 'Ballpoint Pen Set', 'Stationery', 9, 20, 65, 120, 'S002', 3.8],
      ['P1010', 'LS-002', 'Cotton Tote Bag', 'Lifestyle', 80, 20, 160, 290, 'S003', 2],
      ['P1011', 'EL-004', 'LED Desk Lamp', 'Electronics', 23, 8, 720, 1190, 'S001', 1.3],
      ['P1012', 'LS-003', 'Ceramic Coffee Mug', 'Lifestyle', 0, 10, 210, 390, 'S003', 1.7]
    ].map(([id, sku, name, category, stock, reorder, cost, price, supplierId, dailyDemand], index) => ({
      id, sku, name, category, stock, reorder, cost, purchaseCost: cost, price, supplierId, dailyDemand,
      unit: 'piece', barcode: `89010000${String(index + 1).padStart(4, '0')}`, status: 'active',
      expiry: category === 'Groceries' ? daysFromToday(index === 4 ? 18 : 120) : '', description: ''
    }));
    const suppliers = [
      { id: 'S001', name: 'Orbit Electronics', contact: 'Rafi Hassan', email: 'orders@orbit.example', phone: '01700 000101', address: 'Gulshan, Dhaka', leadTime: 5, payable: 18400, status: 'active' },
      { id: 'S002', name: 'Paper & Co.', contact: 'Mira Khan', email: 'hello@paperco.example', phone: '01700 000102', address: 'Dhanmondi, Dhaka', leadTime: 3, payable: 9200, status: 'active' },
      { id: 'S003', name: 'Everyday Essentials', contact: 'Samir Ali', email: 'supply@everyday.example', phone: '01700 000103', address: 'Uttara, Dhaka', leadTime: 4, payable: 6500, status: 'active' },
      { id: 'S004', name: 'Greenfield Foods', contact: 'Nadia Islam', email: 'trade@greenfield.example', phone: '01700 000104', address: 'Tejgaon, Dhaka', leadTime: 2, payable: 12600, status: 'active' }
    ];
    const customers = [
      { id: 'C001', name: 'Walk-in Customer', email: '', phone: '', address: '', creditLimit: 0, due: 0, openingDue: 0, status: 'active' },
      { id: 'C002', name: 'Maple Creative Studio', email: 'team@maple.example', phone: '01800 000201', address: 'Banani, Dhaka', creditLimit: 30000, due: 0, openingDue: 0, status: 'active' },
      { id: 'C003', name: 'Maya Rahman', email: 'maya@customer.example', phone: '01800 000202', address: 'Mohakhali, Dhaka', creditLimit: 15000, due: 0, openingDue: 0, status: 'active' },
      { id: 'C004', name: 'Northstar Office', email: 'hello@northstar.example', phone: '01800 000203', address: 'Motijheel, Dhaka', creditLimit: 50000, due: 0, openingDue: 0, status: 'active' },
      { id: 'C005', name: 'Daniel Ahmed', email: 'daniel@customer.example', phone: '01800 000204', address: 'Bashundhara, Dhaka', creditLimit: 10000, due: 0, openingDue: 0, status: 'active' }
    ];
    const invoices = Array.from({ length: 24 }, (_, index) => {
      const item = products[(index * 5 + 2) % products.length];
      const second = products[(index * 3 + 4) % products.length];
      const items = [{ productId: item.id, quantity: 1 + index % 4, unitPrice: item.price, unitCost: item.cost }];
      if (second.id !== item.id) items.push({ productId: second.id, quantity: 1 + index % 2, unitPrice: second.price, unitCost: second.cost });
      const total = round(items.reduce((sum, row) => sum + row.quantity * row.unitPrice, 0));
      const customerId = customers[index % customers.length].id;
      const paid = customerId !== 'C001' && index % 6 === 0 ? round(total * 0.5) : total;
      return { id: `I${index + 1}`, number: `INV-${2075 + index}`, customerId, date: daysFromToday(-Math.floor(index / 4)),
        status: paid < total ? 'partial' : 'paid', items, discount: 0, total, paid, method: index % 3 ? 'Cash' : 'Card', returns: [] };
    });
    customers.forEach(customer => { customer.due = round(invoices.filter(invoice => invoice.customerId === customer.id).reduce((sum, invoice) => sum + invoice.total - invoice.paid, 0)); });
    const purchaseOrders = [
      { id: 'PO1', number: 'PO-1041', supplierId: 'S001', date: daysFromToday(-3), expected: daysFromToday(3), status: 'approved', items: [{ productId: 'P1002', quantity: 40, received: 0, unitCost: 680 }, { productId: 'P1007', quantity: 30, received: 0, unitCost: 480 }], paid: 0, notes: 'Restock our fastest-moving accessories.' },
      { id: 'PO2', number: 'PO-1042', supplierId: 'S004', date: daysFromToday(-2), expected: daysFromToday(5), status: 'draft', items: [{ productId: 'P1005', quantity: 60, received: 0, unitCost: 580 }], paid: 0, notes: '' },
      { id: 'PO3', number: 'PO-1043', supplierId: 'S002', date: daysFromToday(-4), expected: daysFromToday(1), status: 'approved', items: [{ productId: 'P1009', quantity: 100, received: 0, unitCost: 65 }, { productId: 'P1003', quantity: 80, received: 0, unitCost: 145 }], paid: 0, notes: '' },
      { id: 'PO4', number: 'PO-1044', supplierId: 'S003', date: daysFromToday(-1), expected: daysFromToday(4), status: 'draft', items: [{ productId: 'P1012', quantity: 48, received: 0, unitCost: 210 }], paid: 0, notes: '' }
    ];
    return {
      meta: { version: 1, createdAt: new Date().toISOString(), updatedAt: new Date().toISOString(), counters: { purchase: 1044, sale: 2098, adjustment: 3011, payment: 4010 } },
      settings: { businessName: 'Evergreen Trading', ownerName: 'Ava Rahman', email: 'ava@evergreen.example', phone: '01700 000100', address: '24 Green Road, Dhaka 1205', currency: 'BDT', timezone: 'Asia/Dhaka', lowStockEmails: false, expiryDays: 30, theme: 'light', invoicePrefix: 'INV', discountLimit: 10 },
      products, suppliers, customers, invoices, purchaseOrders,
      movements: products.filter(product => product.stock > 0).map((product, index) => ({ id: `M${index}`, productId: product.id, date: daysFromToday(-7), type: 'opening', quantity: product.stock, balance: product.stock, reference: 'OPENING-SNAPSHOT', note: 'Demo opening snapshot. Earlier invoice examples are illustrative history.' })),
      adjustments: [{ id: 'A1', number: 'ADJ-3011', productId: 'P1003', date: daysFromToday(-1), quantity: -2, reason: 'Two notebooks damaged during handling', status: 'pending', expectedStock: 120 }],
      payments: [],
      users: [
        { id: 'U001', name: 'Ava Rahman', email: 'ava@evergreen.example', role: 'Owner', status: 'active', lastActive: 'Current demo profile' },
        { id: 'U002', name: 'Omar Faruk', email: 'omar@evergreen.example', role: 'Manager', status: 'active', lastActive: 'Sample activity' },
        { id: 'U003', name: 'Nora Islam', email: 'nora@evergreen.example', role: 'Sales Staff', status: 'active', lastActive: 'Sample activity' },
        { id: 'U004', name: 'Ethan Khan', email: 'ethan@evergreen.example', role: 'Storekeeper', status: 'active', lastActive: 'Sample activity' },
        { id: 'U005', name: 'Zara Ahmed', email: 'zara@evergreen.example', role: 'Sales Staff', status: 'inactive', lastActive: 'Sample activity' }
      ],
      audit: [
        { id: 'L1', date: new Date().toISOString(), actor: 'Ava Rahman', action: 'Workspace initialized', module: 'System', detail: 'Local sample workspace created. This log is not a protected server audit.' },
        { id: 'L2', date: `${daysFromToday(-1)}T11:30:00`, actor: 'Ethan Khan', action: 'Adjustment requested', module: 'Stock', detail: 'ADJ-3011: two damaged notebooks awaiting review.' },
        { id: 'L3', date: `${daysFromToday(-2)}T09:15:00`, actor: 'Omar Faruk', action: 'Purchase order prepared', module: 'Purchases', detail: 'Sample PO-1042 prepared for Greenfield Foods.' }
      ]
    };
  }

  let memory;
  let storageAvailable = true;
  function read() {
    if (!storageAvailable && memory) return copy(memory);
    try {
      const raw = localStorage.getItem(KEY);
      if (raw) {
        const value = JSON.parse(raw);
        if (value.meta?.version === 1 && Array.isArray(value.products) && Array.isArray(value.invoices) && Array.isArray(value.users)) return value;
      }
    } catch (_) { storageAvailable = false; }
    if (!memory) memory = seed();
    return copy(memory);
  }
  function persist(value) {
    try { localStorage.setItem(KEY, JSON.stringify(value)); storageAvailable = true; }
    catch (_) { storageAvailable = false; }
    memory = copy(value);
  }
  let state = read();
  persist(state);

  function validate(next) {
    const skus = new Set();
    next.products.forEach(product => {
      const sku = String(product.sku).trim().toLowerCase();
      if (!sku || skus.has(sku)) throw new Error('Every product needs a unique SKU, including archived products.');
      skus.add(sku);
      for (const field of ['stock', 'cost', 'price', 'reorder']) {
        if (!Number.isFinite(Number(product[field])) || Number(product[field]) < 0) throw new Error(`Invalid ${field} for ${product.name}.`);
      }
    });
    if (!next.users.some(user => user.role === 'Owner' && user.status === 'active')) throw new Error('Keep at least one active Owner profile.');
  }
  function update(mutator, action = 'Workspace updated', module = 'System', detail = '') {
    const next = read();
    mutator(next);
    validate(next);
    next.meta.updatedAt = new Date().toISOString();
    next.audit.unshift({ id: uid('log'), date: next.meta.updatedAt, actor: next.settings.ownerName, action, module, detail });
    persist(next);
    state = next;
    window.dispatchEvent(new CustomEvent('smartstock:change'));
    if (!storageAvailable) toast('Browser storage is unavailable. Changes last only on this page.', 'warning');
    return next;
  }
  function nextNumber(target, type) {
    const prefixes = { purchase: 'PO', sale: 'INV', adjustment: 'ADJ', payment: 'PAY' };
    target.meta.counters[type] = (target.meta.counters[type] || 0) + 1;
    return `${prefixes[type] || type.toUpperCase()}-${target.meta.counters[type]}`;
  }
  const icon = name => `<svg class="icon" aria-hidden="true"><use href="#i-${escape(name)}"></use></svg>`;
  const initials = name => String(name || '?').split(/\s+/).slice(0, 2).map(word => word[0]).join('').toUpperCase();
  const tones = { Electronics: 'blue', Stationery: 'amber', Lifestyle: 'purple', Groceries: 'green', Household: 'rose' };
  function productCell(product, subtitle = product.sku) {
    return `<div class="item-cell"><span class="item-icon tone-${tones[product.category] || 'green'}">${icon(product.category === 'Groceries' ? 'leaf' : product.category === 'Stationery' ? 'book' : 'box')}</span><span><span class="cell-title">${escape(product.name)}</span><span class="cell-subtitle">${escape(subtitle)}</span></span></div>`;
  }
  function badge(status, label) {
    const classes = { active: 'success', healthy: 'success', paid: 'success', received: 'success', approved: 'success', completed: 'success', low: 'warning', partial: 'warning', pending: 'warning', draft: 'neutral', inactive: 'neutral', archived: 'neutral', out: 'danger', overdue: 'danger', rejected: 'danger', cancelled: 'neutral', returned: 'purple', info: 'info' };
    const labels = { healthy: 'In stock', low: 'Low stock', out: 'Out of stock', partial: 'Partial' };
    return `<span class="status status-${classes[status] || 'neutral'}">${escape(label || labels[status] || String(status).replace(/^./, character => character.toUpperCase()))}</span>`;
  }
  const stockStatus = product => product.stock <= 0 ? 'out' : product.stock <= product.reorder ? 'low' : 'healthy';
  const date = value => value ? new Date(String(value).length === 10 ? `${value}T12:00:00` : value).toLocaleDateString('en-GB', { day: 'numeric', month: 'short', year: 'numeric' }) : '-';
  const onChange = callback => window.addEventListener('smartstock:change', callback);
  const ready = callback => document.readyState === 'loading' ? document.addEventListener('DOMContentLoaded', callback, { once: true }) : callback();
  const emptyRow = (columns, message = 'No results found. Try another search or filter.') => `<tr><td colspan="${Number(columns)}"><div class="empty-state">${icon('search')}<strong>Nothing to show yet</strong><p>${escape(message)}</p></div></td></tr>`;

  function toast(message, type = 'success') {
    const host = document.getElementById('toast-region');
    if (!host) return;
    const element = document.createElement('div');
    element.className = `toast toast-${type}`;
    element.innerHTML = `${icon(type === 'success' ? 'check-circle' : 'info')}<span>${escape(message)}</span><button class="icon-button" aria-label="Dismiss notification">${icon('close')}</button>`;
    host.append(element);
    element.querySelector('button').addEventListener('click', () => element.remove());
    setTimeout(() => element.remove(), 5500);
  }
  let modalSubmit = null;
  let modalFocus = null;
  function openModal({ title, body, submitLabel = 'Save changes', onSubmit = null, wide = false, danger = false }) {
    const dialog = document.getElementById('app-modal');
    if (!dialog) return;
    modalFocus = document.activeElement;
    modalSubmit = onSubmit;
    dialog.classList.toggle('modal-wide', wide);
    document.getElementById('modal-title').textContent = title;
    document.getElementById('modal-body').innerHTML = body;
    document.getElementById('modal-error').hidden = true;
    const submit = document.getElementById('modal-submit');
    submit.textContent = submitLabel;
    submit.hidden = !onSubmit;
    submit.className = danger ? 'button button-danger' : 'button button-primary';
    document.getElementById('modal-cancel').textContent = onSubmit ? 'Cancel' : 'Close';
    if (!dialog.open) dialog.showModal();
    requestAnimationFrame(() => (dialog.querySelector('input:not([type="hidden"]),select,textarea') || dialog.querySelector('[data-close-modal]')).focus());
  }
  function closeModal() { document.getElementById('app-modal')?.close(); }
  function confirm(message, onConfirm, options = {}) {
    openModal({ title: options.title || 'Confirm this action', body: `<div class="confirmation"><span class="confirmation-icon">${icon(options.danger ? 'alert' : 'check-circle')}</span><p>${escape(message)}</p><small>This changes the local demo only.</small></div>`, submitLabel: options.label || 'Confirm', danger: !!options.danger, onSubmit: () => onConfirm() });
  }
  function downloadCSV(filename, rows) {
    const csv = rows.map(row => row.map(value => {
      let text = String(value ?? '');
      if (typeof value === 'string' && (/^[\s\u0000-\u001f]*[=+\-@]/.test(text) || /^[\t\r\n]/.test(text))) text = `'${text}`;
      return `"${text.replace(/"/g, '""')}"`;
    }).join(',')).join('\r\n');
    const url = URL.createObjectURL(new Blob(['\uFEFF', csv], { type: 'text/csv;charset=utf-8;' }));
    const link = document.createElement('a');
    link.href = url; link.download = filename; document.body.append(link); link.click(); link.remove();
    setTimeout(() => URL.revokeObjectURL(url), 1000);
    toast('CSV export is ready.');
  }
  function getAlerts() {
    const alerts = [];
    state.products.filter(product => product.status === 'active').forEach(product => {
      if (stockStatus(product) !== 'healthy') alerts.push({ title: product.name, detail: product.stock ? `${number(product.stock)} left, below the ${product.reorder}-unit reorder level` : 'Out of stock. Review your next purchase.', type: product.stock ? 'warning' : 'danger', href: 'stock.html' });
      if (product.expiry && product.stock && product.expiry <= daysFromToday(state.settings.expiryDays || 30)) {
        const expired = product.expiry < today();
        alerts.push({ title: `${product.name}: ${expired ? 'expired stock' : 'expiry approaching'}`, detail: `Expiry date ${date(product.expiry)}${expired ? '. Not available for new sales.' : ''}`, type: expired ? 'danger' : 'warning', href: 'stock.html' });
      }
    });
    return alerts;
  }
  function refreshShell() {
    document.querySelectorAll('[data-business-name]').forEach(element => { element.textContent = state.settings.businessName; });
    document.querySelectorAll('[data-owner-name]').forEach(element => { element.textContent = state.settings.ownerName; });
    document.querySelectorAll('[data-owner-initials]').forEach(element => { element.textContent = initials(state.settings.ownerName); });
    const count = getAlerts().length;
    document.querySelectorAll('[data-alert-count]').forEach(element => { element.textContent = count; element.hidden = !count; });
    document.querySelectorAll('[data-low-count]').forEach(element => { element.textContent = state.products.filter(product => product.status === 'active' && stockStatus(product) !== 'healthy').length; });
    document.documentElement.dataset.theme = state.settings.theme || 'light';
    const banner = document.getElementById('storage-warning');
    if (banner) banner.hidden = storageAvailable;
  }
  ready(() => {
    refreshShell();
    document.addEventListener('keydown', event => {
      if (event.key === 'Escape') document.querySelector('.profile-menu')?.removeAttribute('open');
      if (event.key === '/' && !/input|textarea|select/i.test(document.activeElement?.tagName || '') && !document.getElementById('app-modal')?.open) { event.preventDefault(); document.getElementById('global-search')?.focus(); }
    });
    document.addEventListener('click', event => { if (!event.target.closest('.profile-menu')) document.querySelector('.profile-menu')?.removeAttribute('open'); });
    document.getElementById('global-search-form')?.addEventListener('submit', event => {
      event.preventDefault();
      const query = document.getElementById('global-search').value.trim();
      if (query) location.href = `products.html?q=${encodeURIComponent(query)}`;
    });
    document.getElementById('notification-button')?.addEventListener('click', () => {
      const alerts = getAlerts();
      openModal({ title: `Stock notifications (${alerts.length})`, body: alerts.length ? `<div class="notification-list">${alerts.map(alert => `<a href="${alert.href}" class="notification-item"><span class="item-icon tone-${alert.type === 'danger' ? 'rose' : 'amber'}">${icon('alert')}</span><span><strong>${escape(alert.title)}</strong><small>${escape(alert.detail)}</small></span>${icon('arrow-right')}</a>`).join('')}</div>` : '<div class="empty-state"><strong>All caught up</strong><p>No stock notifications right now.</p></div>' });
    });
    document.querySelectorAll('[data-close-modal]').forEach(button => button.addEventListener('click', closeModal));
    const dialog = document.getElementById('app-modal');
    dialog?.addEventListener('close', () => { modalSubmit = null; if (modalFocus?.isConnected) modalFocus.focus(); });
    dialog?.addEventListener('click', event => { if (event.target === dialog) { const bounds = dialog.getBoundingClientRect(); if (event.clientX < bounds.left || event.clientX > bounds.right || event.clientY < bounds.top || event.clientY > bounds.bottom) closeModal(); } });
    document.getElementById('modal-form')?.addEventListener('submit', event => {
      event.preventDefault();
      if (!modalSubmit) { closeModal(); return; }
      try { if (modalSubmit(new FormData(event.currentTarget), event.currentTarget) !== false) closeModal(); }
      catch (error) { const message = document.getElementById('modal-error'); message.textContent = error.message || 'Please check the form and try again.'; message.hidden = false; message.focus(); }
    });
    document.getElementById('help-button')?.addEventListener('click', () => openModal({ title: 'Your SmartStock workspace', body: '<div class="prose"><p>This is a desktop-focused HTML, CSS and vanilla JavaScript admin preview. Changes are saved only in this browser.</p><h3>A few useful shortcuts</h3><p>Press <kbd>/</kbd> to find a product. Use the sidebar to explore every module. Review stock notifications from the bell.</p><div class="notice"><strong>Demo, not a connected business system</strong><p>No authentication, supplier messages, real payments, live AI or server backups are connected. Do not enter real credentials or sensitive information.</p></div></div>' }));
  });
  onChange(refreshShell);
  window.addEventListener('storage', event => { if (event.key === KEY) { state = read(); window.dispatchEvent(new CustomEvent('smartstock:change')); } });
  window.SS = {
    get state() { return state; }, get page() { return document.body.dataset.page; },
    ready, onChange, update, read, uid, nextNumber, today, daysFromToday, escape, number, money, round, date, icon, badge, stockStatus, productCell, initials, emptyRow,
    openModal, closeModal, confirm, toast, downloadCSV, getAlerts,
    reset() { persist(seed()); state = read(); window.dispatchEvent(new CustomEvent('smartstock:change')); toast('The local sample workspace has been restored.'); },
    product(id, source = state) { return source.products.find(product => product.id === id); },
    supplier(id, source = state) { return source.suppliers.find(supplier => supplier.id === id); },
    customer(id, source = state) { return source.customers.find(customer => customer.id === id); },
    orderTotal(order) { return round(order.items.reduce((sum, item) => sum + item.quantity * item.unitCost, 0)); },
    invoiceCredit(invoice) { return round((invoice.returns || []).reduce((sum, entry) => sum + Number(entry.credit || 0), 0)); },
    invoiceDue(invoice) { return Math.max(0, round(invoice.total - invoice.paid - (invoice.returns || []).reduce((sum, entry) => sum + Number(entry.credit || 0), 0))); }
  };
})();
