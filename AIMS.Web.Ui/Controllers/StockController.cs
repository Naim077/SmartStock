using AIMS.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIMS.Web.Ui.Controllers;

[Authorize]
public class StockController : Controller
{
    private readonly IProductFacade _productFacade;
    private readonly IProductStockBalanceFacade _stockFacade;

    public StockController(IProductFacade productFacade, IProductStockBalanceFacade stockFacade)
    {
        _productFacade = productFacade;
        _stockFacade = stockFacade;
    }

    public async Task<IActionResult> Index(string? search, string? stockStatus)
    {
        var products = await _productFacade.GetProductsAsync();
        var balances = (await _stockFacade.GetProductStockBalancesAsync()).ToDictionary(x => x.ProductId);
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            products = products.Where(p => p.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                           p.Sku.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (stockStatus == "Out")
            products = products.Where(p => !balances.ContainsKey(p.ProductId) || balances[p.ProductId].Quantity <= 0).ToList();
        else if (stockStatus == "Available")
            products = products.Where(p => balances.ContainsKey(p.ProductId) && balances[p.ProductId].Quantity > 0).ToList();

        ViewBag.Balances = balances;
        ViewBag.Search = search ?? "";
        ViewBag.StockStatus = stockStatus ?? "All";
        ViewBag.TotalProducts = products.Count;
        ViewBag.InventoryValue = products.Where(p => balances.ContainsKey(p.ProductId))
            .Sum(p => balances[p.ProductId].InventoryValue);
        ViewBag.OutOfStock = products.Count(p => !balances.ContainsKey(p.ProductId) || balances[p.ProductId].Quantity <= 0);
        return View(products.OrderBy(p => p.Name).ToList());
    }
}
