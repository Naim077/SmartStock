using AIMS.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIMS.Web.Ui.Controllers;

[Authorize]
public class SmartReorderController : Controller
{
    private readonly IReorderSuggestionFacade _suggestionFacade;
    private readonly IProductFacade _productFacade;

    public SmartReorderController(IReorderSuggestionFacade suggestionFacade, IProductFacade productFacade)
    {
        _suggestionFacade = suggestionFacade;
        _productFacade = productFacade;
    }

    public async Task<IActionResult> Index(string? status)
    {
        var suggestions = await _suggestionFacade.GetReorderSuggestionsAsync();
        var products = await _productFacade.GetProductsAsync();
        if (!string.IsNullOrWhiteSpace(status) && !status.Equals("All", StringComparison.OrdinalIgnoreCase))
            suggestions = suggestions.Where(x => x.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

        ViewBag.Products = products.ToDictionary(x => x.ProductId);
        ViewBag.SelectedStatus = status ?? "All";
        ViewBag.TotalSuggestions = suggestions.Count;
        ViewBag.ProposedUnits = suggestions.Where(x => x.SuggestedQuantity.HasValue).Sum(x => x.SuggestedQuantity!.Value);
        return View(suggestions.OrderByDescending(x => x.GeneratedUtc).ToList());
    }
}
