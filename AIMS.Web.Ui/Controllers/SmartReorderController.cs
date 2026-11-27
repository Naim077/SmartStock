using AIMS.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    [HttpGet]
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Review(long id, string outcomeReason)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Forbid();

        if (string.IsNullOrWhiteSpace(outcomeReason) || outcomeReason.Trim().Length > 1000)
        {
            TempData["ErrorMessage"] = "Enter a review note of up to 1,000 characters.";
            return RedirectToAction(nameof(Index));
        }

        var suggestion = await _suggestionFacade.GetReorderSuggestionByIdAsync(id);
        if (suggestion == null)
            return NotFound();

        // The generated forecast values remain unchanged. This records only the human review outcome.
        suggestion.OutcomeByUserId = userId;
        suggestion.OutcomeUtc = DateTime.UtcNow;
        suggestion.OutcomeReason = outcomeReason.Trim();

        var updated = await _suggestionFacade.UpdateReorderSuggestionAsync(suggestion);
        TempData[updated ? "SuccessMessage" : "ErrorMessage"] = updated
            ? "Review note saved."
            : "The review note could not be saved.";

        return RedirectToAction(nameof(Index));
    }
}
