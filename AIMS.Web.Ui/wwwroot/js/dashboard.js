(() => {
  'use strict';
  SS.ready(() => {
    if (SS.page !== 'index') return;
    const byId = id => document.getElementById(id);
    let period = 7;
    const moneyMarkup = amount => `<span class="currency">BDT</span>${SS.number(amount, 2)}`;
    function series() {
      const values = Array.from({ length: period }, (_, index) => ({ date: SS.daysFromToday(index - period + 1), value: 0 }));
      SS.state.invoices.forEach(invoice => {
        const day = values.find(row => row.date === invoice.date);
        if (day) day.value += invoice.total;
        (invoice.returns || []).forEach(entry => { const returnDay = values.find(row => row.date === entry.date); if (returnDay) returnDay.value -= entry.credit; });
      });
      return values;
    }
    function renderChart() {
      const points = series();
      const values = points.map(point => point.value);
      const minimum = Math.min(0, ...values);
      const maximum = Math.max(1000, Math.ceil(Math.max(...values) / 1000) * 1000);
      const span = maximum - minimum || 1;
      const chartWidth = Math.max(280, byId('sales-chart').clientWidth - 30);
      const left = 43, right = chartWidth - 17, top = 18, bottom = 170;
      const x = index => left + index * (right - left) / Math.max(1, points.length - 1);
      const y = value => bottom - (value - minimum) / span * (bottom - top);
      let line = `M${x(0)},${y(points[0].value)}`;
      points.slice(1).forEach((point, index) => {
        const middle = (x(index) + x(index + 1)) / 2;
        line += `C${middle},${y(points[index].value)} ${middle},${y(point.value)} ${x(index + 1)},${y(point.value)}`;
      });
      const area = `${line}L${right},${y(0)}L${left},${y(0)}Z`;
      const grid = Array.from({ length: 5 }, (_, index) => {
        const value = minimum + span * index / 4;
        return `<line x1="${left}" x2="${right}" y1="${y(value)}" y2="${y(value)}" stroke="var(--border)" stroke-dasharray="3 4"/><text x="32" y="${y(value) + 3}" text-anchor="end" font-size="9" fill="var(--muted)">${Math.abs(value) >= 1000 ? `${SS.number(value / 1000, 1)}k` : SS.number(value)}</text>`;
      }).join('');
      const labelCount = Math.min(points.length, chartWidth < 420 ? 4 : 7);
      const labelIndexes = new Set(Array.from({ length: labelCount }, (_, index) => Math.round(index * (points.length - 1) / Math.max(1, labelCount - 1))));
      const labels = points.map((point, index) => {
        if (!labelIndexes.has(index)) return '';
        const label = new Date(`${point.date}T12:00:00`).toLocaleDateString('en-GB', period === 7 ? { weekday: 'short' } : { day: 'numeric', month: 'short' });
        return `<text x="${x(index)}" y="195" text-anchor="middle" font-size="9" fill="var(--muted)">${SS.escape(label)}</text>`;
      }).join('');
      byId('sales-chart').innerHTML = `<svg viewBox="0 0 ${chartWidth} 205" role="img" aria-label="Net sales over the last ${period} days. Total ${SS.escape(SS.money(points.reduce((sum, row) => sum + row.value, 0)))}."><defs><linearGradient id="sales-fill" x1="0" x2="0" y1="0" y2="1"><stop offset="0%" stop-color="#90af83" stop-opacity=".21"/><stop offset="100%" stop-color="#90af83" stop-opacity=".02"/></linearGradient></defs>${grid}<path d="${area}" fill="url(#sales-fill)"/><path d="${line}" fill="none" stroke="var(--primary)" stroke-width="2.4" stroke-linecap="round"/><circle cx="${right}" cy="${y(points.at(-1).value)}" r="4" fill="var(--primary)" stroke="var(--surface)" stroke-width="2"/>${labels}</svg>`;
      byId('chart-revenue-total').textContent = SS.money(points.reduce((sum, row) => sum + row.value, 0));
    }
    function render() {
      const state = SS.state;
      const products = state.products.filter(product => product.status === 'active');
      const attention = products.filter(product => SS.stockStatus(product) !== 'healthy').sort((first, second) => first.stock - second.stock);
      const out = products.filter(product => !product.stock).length;
      const low = attention.length - out;
      const healthy = products.length - attention.length;
      const orders = state.purchaseOrders.filter(order => ['draft', 'approved', 'partial'].includes(order.status));
      const approved = orders.filter(order => order.status !== 'draft').length;
      const todayInvoices = state.invoices.filter(invoice => invoice.date === SS.today());
      const todayCredits = state.invoices.reduce((sum, invoice) => sum + (invoice.returns || []).filter(entry => entry.date === SS.today()).reduce((total, entry) => total + entry.credit, 0), 0);
      byId('kpi-inventory').innerHTML = moneyMarkup(state.products.reduce((sum, product) => sum + product.stock * product.cost, 0));
      byId('kpi-product-count').textContent = `${products.length} active products`;
      byId('kpi-sales').innerHTML = moneyMarkup(todayInvoices.reduce((sum, invoice) => sum + invoice.total, 0) - todayCredits);
      byId('kpi-invoice-count').textContent = `${todayInvoices.length} invoices`;
      byId('kpi-attention').innerHTML = `${attention.length}<span class="currency"> products</span>`;
      byId('kpi-out-count').textContent = `${out} out of stock`;
      byId('kpi-orders').textContent = orders.length;
      byId('kpi-approved-count').textContent = `${approved} approved`;
      byId('kpi-draft-count').textContent = `${orders.length - approved} drafts to review`;
      byId('health-total').textContent = products.length;
      byId('health-healthy').textContent = healthy;
      byId('health-low').textContent = low;
      byId('health-out').textContent = out;
      const divisor = products.length || 1;
      byId('stock-donut').style.background = `conic-gradient(#65846b 0% ${healthy / divisor * 100}%, #d8b878 ${healthy / divisor * 100}% ${(healthy + low) / divisor * 100}%, #c68070 ${(healthy + low) / divisor * 100}% 100%)`;
      byId('stock-donut').setAttribute('aria-label', `${healthy} in stock, ${low} low stock, ${out} out of stock`);
      byId('health-unit-count').textContent = `${SS.number(products.reduce((sum, product) => sum + product.stock, 0), 3)} units on hand`;
      byId('reorder-banner-copy').textContent = attention.length ? `Review ${attention.length} products that could use a restock. You stay in control of every order.` : 'Your active products are above their reorder levels. Review future demand before ordering.';
      byId('attention-count').textContent = `${attention.length} products`;
      byId('dashboard-low-stock').innerHTML = attention.slice(0, 5).map(product => `<tr><td><a href="products.html?q=${encodeURIComponent(product.sku)}">${SS.productCell(product)}</a></td><td><div class="stock-quantity"><strong>${SS.number(product.stock, 3)}</strong><span class="stock-meter"><span style="width:${Math.min(100, product.stock / Math.max(1, product.reorder) * 100)}%"></span></span></div></td><td>${SS.badge(SS.stockStatus(product))}</td><td><a href="smart-reorder.html" class="icon-button" aria-label="Review restock for ${SS.escape(product.name)}">${SS.icon('arrow-up-right')}</a></td></tr>`).join('') || SS.emptyRow(4, 'All active products are above their reorder levels.');
      byId('dashboard-activity').innerHTML = state.audit.slice(0, 5).map(entry => {
        const elapsed = Math.floor((Date.now() - new Date(entry.date).getTime()) / 86400000);
        const relative = elapsed < 1 ? 'Today' : elapsed === 1 ? 'Yesterday' : `${elapsed}d ago`;
        return `<div class="activity-item"><span class="activity-symbol">${SS.icon(entry.module === 'Stock' ? 'stock' : entry.module === 'Purchases' ? 'truck' : entry.module === 'Sales' ? 'receipt' : 'check')}</span><div><strong>${SS.escape(entry.action)}</strong><p>${SS.escape(entry.actor)} &middot; ${SS.escape(entry.module)}</p></div><time datetime="${SS.escape(entry.date)}">${relative}</time></div>`;
      }).join('');
      renderChart();
    }
    byId('dashboard-period').addEventListener('change', event => { period = Number(event.target.value); renderChart(); });
    let resizeFrame;
    window.addEventListener('resize', () => { cancelAnimationFrame(resizeFrame); resizeFrame = requestAnimationFrame(renderChart); });
    byId('dashboard-export').addEventListener('click', () => {
      const state = SS.state;
      SS.downloadCSV('smartstock-overview.csv', [['Metric', 'Value'], ['Active products', state.products.filter(product => product.status === 'active').length], ['Inventory value (BDT)', SS.round(state.products.reduce((sum, product) => sum + product.stock * product.cost, 0))], ['Low or out-of-stock products', state.products.filter(product => product.status === 'active' && SS.stockStatus(product) !== 'healthy').length], ['Report date', SS.today()], [], ['Date', 'Net sales (BDT)'], ...series().map(point => [point.date, SS.round(point.value)])]);
    });
    SS.onChange(render);
    render();
  });
})();
