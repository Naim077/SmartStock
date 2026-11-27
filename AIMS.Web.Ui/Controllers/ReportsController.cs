using AIMS.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIMS.Web.Ui.Controllers;

[Authorize]
public class ReportsController : Controller
{
    private readonly IProductFacade _productFacade;
    private readonly IProductStockBalanceFacade _stockFacade;
    private readonly ISaleFacade _saleFacade;
    private readonly IDocumentFacade _documentFacade;
    private readonly IPurchaseOrderFacade _purchaseFacade;

    public ReportsController(IProductFacade productFacade, IProductStockBalanceFacade stockFacade,
        ISaleFacade saleFacade, IDocumentFacade documentFacade, IPurchaseOrderFacade purchaseFacade)
    {
        _productFacade = productFacade;
        _stockFacade = stockFacade;
        _saleFacade = saleFacade;
        _documentFacade = documentFacade;
        _purchaseFacade = purchaseFacade;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productFacade.GetProductsAsync();
        var balances = await _stockFacade.GetProductStockBalancesAsync();
        var sales = await _saleFacade.GetSalesAsync();
        var purchases = await _purchaseFacade.GetPurchaseOrdersAsync();
        var documents = await _documentFacade.GetDocumentsAsync();
        var saleDocuments = documents.Where(d => d.DocumentKind.Equals("Sale", StringComparison.OrdinalIgnoreCase) ||
                                                  d.DocumentKind.Equals("SalesInvoice", StringComparison.OrdinalIgnoreCase))
            .ToDictionary(d => d.DocumentId);
        var postedSales = sales.Where(s => saleDocuments.ContainsKey(s.DocumentId) &&
            !new[] { "Draft", "Cancelled", "Void" }.Contains(saleDocuments[s.DocumentId].Status, StringComparer.OrdinalIgnoreCase))
            .ToList();

        ViewBag.ProductCount = products.Count(p => p.IsActive);
        ViewBag.StockUnits = balances.Sum(x => x.Quantity);
        ViewBag.InventoryValue = balances.Sum(x => x.InventoryValue);
        ViewBag.SalesCount = postedSales.Count;
        ViewBag.SalesRevenue = postedSales.Sum(s => s.TotalAmount);
        ViewBag.PurchaseOrders = purchases.Count;
        ViewBag.GeneratedUtc = DateTime.UtcNow;
        ViewBag.LowStockCount = products.Count(p =>
        {
            var quantity = balances.FirstOrDefault(b => b.ProductId == p.ProductId)?.Quantity ?? 0;
            return p.IsActive && quantity <= p.ManualReorderPoint;
        });
        return View();
    }
}
