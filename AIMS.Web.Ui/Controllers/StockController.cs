using AIMS.DataAccess;
using AIMS.Entities;
using AIMS.Facade.Interface;
using AIMS.Web.Ui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIMS.Web.Ui.Controllers;

[Authorize]
public class StockController : Controller
{
    private readonly IProductFacade _productFacade;
    private readonly IProductStockBalanceFacade _stockFacade;
    private readonly IStockChangeFacade _changeFacade;
    private readonly IStockChangeLineFacade _changeLineFacade;
    private readonly IDocumentFacade _documentFacade;
    private readonly IDocumentLineFacade _documentLineFacade;
    private readonly IProductBatchFacade _batchFacade;
    private readonly IBusinessSettingsFacade _settingsFacade;
    private readonly ApplicationDbContext _db;

    public StockController(
        IProductFacade productFacade,
        IProductStockBalanceFacade stockFacade,
        IStockChangeFacade changeFacade,
        IStockChangeLineFacade changeLineFacade,
        IDocumentFacade documentFacade,
        IDocumentLineFacade documentLineFacade,
        IProductBatchFacade batchFacade,
        IBusinessSettingsFacade settingsFacade,
        ApplicationDbContext db)
    {
        _productFacade = productFacade;
        _stockFacade = stockFacade;
        _changeFacade = changeFacade;
        _changeLineFacade = changeLineFacade;
        _documentFacade = documentFacade;
        _documentLineFacade = documentLineFacade;
        _batchFacade = batchFacade;
        _settingsFacade = settingsFacade;
        _db = db;
    }

    [HttpGet]
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

    [HttpGet]
    public async Task<IActionResult> Adjustments(string? search)
    {
        var changes = await _changeFacade.GetStockChangesAsync();
        var documents = (await _documentFacade.GetDocumentsAsync())
            .Where(x => x.DocumentKind.Equals("StockChange", StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.DocumentId);

        changes = changes.Where(x => documents.ContainsKey(x.DocumentId)).ToList();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            changes = changes.Where(x => x.Reason.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                         x.ChangeKind.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                         (x.EvidenceReference ?? "").Contains(term, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        var lines = await _changeLineFacade.GetStockChangeLinesAsync();
        ViewBag.Documents = documents;
        ViewBag.LineCounts = lines.Where(x => changes.Any(c => c.DocumentId == x.DocumentId))
            .GroupBy(x => x.DocumentId).ToDictionary(x => x.Key, x => x.Count());
        ViewBag.Search = search ?? "";
        return View(changes.OrderByDescending(x => documents[x.DocumentId].CreatedUtc).ToList());
    }

    [HttpGet]
    public async Task<IActionResult> CreateAdjustment()
    {
        await LoadAdjustmentFormData();
        return View(new StockAdjustmentViewModel { Lines = new() { new StockAdjustmentLineViewModel() } });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAdjustment(StockAdjustmentViewModel model)
    {
        var products = await _productFacade.GetProductsAsync();
        var activeProducts = products.Where(x => x.IsActive).ToDictionary(x => x.ProductId);
        var batches = await _batchFacade.GetProductBatchesAsync();
        ValidateAdjustment(model, activeProducts, batches);

        if (!ModelState.IsValid)
        {
            await LoadAdjustmentFormData();
            return View(model);
        }

        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Forbid();

        var settings = (await _settingsFacade.GetBusinessSettingsesAsync()).FirstOrDefault();
        var currency = string.IsNullOrWhiteSpace(settings?.CurrencyCode) ? "BDT" : settings.CurrencyCode;

        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            var document = new Document
            {
                DocumentKind = "StockChange",
                Status = "Draft",
                CurrentVersionNo = 1,
                CurrencyCode = currency,
                CreatedByUserId = userId,
                CreatedUtc = DateTime.UtcNow
            };
            await _documentFacade.AddDocumentAsync(document);

            var change = new StockChange
            {
                DocumentId = document.DocumentId,
                ChangeKind = model.ChangeKind.Trim(),
                Reason = model.Reason.Trim(),
                EvidenceReference = string.IsNullOrWhiteSpace(model.EvidenceReference)
                    ? null : model.EvidenceReference.Trim()
            };
            await _changeFacade.AddStockChangeAsync(change);

            await ReplaceAdjustmentLines(document.DocumentId, model);
            await transaction.CommitAsync();

            TempData["SuccessMessage"] = "Stock adjustment saved as Draft. On-hand balances have not changed.";
            return RedirectToAction(nameof(Adjustments));
        }
        catch
        {
            await transaction.RollbackAsync();
            ModelState.AddModelError(string.Empty, "The adjustment draft could not be saved.");
            await LoadAdjustmentFormData();
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditAdjustment(long id)
    {
        var document = await _documentFacade.GetDocumentByIdAsync(id);
        var change = await _changeFacade.GetStockChangeByIdAsync(id);
        if (document == null || change == null || !IsAdjustmentDraft(document))
            return NotFound();

        var lines = (await _changeLineFacade.GetStockChangeLinesAsync())
            .Where(x => x.DocumentId == id).OrderBy(x => x.LineId).ToList();

        await LoadAdjustmentFormData();
        return View(new StockAdjustmentViewModel
        {
            DocumentId = id,
            ChangeKind = change.ChangeKind,
            Reason = change.Reason,
            EvidenceReference = change.EvidenceReference,
            Lines = lines.Select(x => new StockAdjustmentLineViewModel
            {
                ProductId = x.ProductId,
                ProductBatchId = x.ProductBatchId,
                Bucket = x.Bucket,
                QuantityDelta = x.QuantityDelta
            }).ToList()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAdjustment(long id, StockAdjustmentViewModel model)
    {
        if (id != model.DocumentId)
            return BadRequest();

        var document = await _documentFacade.GetDocumentByIdAsync(id);
        var change = await _changeFacade.GetStockChangeByIdAsync(id);
        if (document == null || change == null || !IsAdjustmentDraft(document))
            return NotFound();

        var products = await _productFacade.GetProductsAsync();
        var activeProducts = products.Where(x => x.IsActive).ToDictionary(x => x.ProductId);
        var batches = await _batchFacade.GetProductBatchesAsync();
        ValidateAdjustment(model, activeProducts, batches);

        if (!ModelState.IsValid)
        {
            await LoadAdjustmentFormData();
            return View(model);
        }

        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            change.ChangeKind = model.ChangeKind.Trim();
            change.Reason = model.Reason.Trim();
            change.EvidenceReference = string.IsNullOrWhiteSpace(model.EvidenceReference)
                ? null : model.EvidenceReference.Trim();
            await _changeFacade.UpdateStockChangeAsync(change);

            await DeleteAdjustmentLines(id);
            await ReplaceAdjustmentLines(id, model);
            await transaction.CommitAsync();

            TempData["SuccessMessage"] = "Draft adjustment updated successfully.";
            return RedirectToAction(nameof(Adjustments));
        }
        catch
        {
            await transaction.RollbackAsync();
            ModelState.AddModelError(string.Empty, "The adjustment draft could not be updated.");
            await LoadAdjustmentFormData();
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAdjustment(long id)
    {
        var document = await _documentFacade.GetDocumentByIdAsync(id);
        var change = await _changeFacade.GetStockChangeByIdAsync(id);
        if (document == null || change == null || !IsAdjustmentDraft(document))
        {
            TempData["ErrorMessage"] = "Only unposted adjustment drafts can be deleted.";
            return RedirectToAction(nameof(Adjustments));
        }

        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            await DeleteAdjustmentLines(id);
            await _changeFacade.DeleteStockChangeAsync(id);
            await _documentFacade.DeleteDocumentAsync(id);
            await transaction.CommitAsync();
            TempData["SuccessMessage"] = "Draft adjustment deleted.";
        }
        catch
        {
            await transaction.RollbackAsync();
            TempData["ErrorMessage"] = "The adjustment draft could not be deleted.";
        }

        return RedirectToAction(nameof(Adjustments));
    }

    private static bool IsAdjustmentDraft(Document document) =>
        document.DocumentKind.Equals("StockChange", StringComparison.OrdinalIgnoreCase) &&
        document.Status.Equals("Draft", StringComparison.OrdinalIgnoreCase);

    private void ValidateAdjustment(
        StockAdjustmentViewModel model,
        Dictionary<long, Product> products,
        List<ProductBatch> batches)
    {
        if (model.Lines == null || model.Lines.Count == 0)
        {
            ModelState.AddModelError(nameof(model.Lines), "Add at least one adjustment line.");
            return;
        }

        foreach (var line in model.Lines)
        {
            if (line.QuantityDelta == 0)
                ModelState.AddModelError(nameof(model.Lines), "Each adjustment quantity must be non-zero.");

            if (!products.ContainsKey(line.ProductId))
                ModelState.AddModelError(nameof(model.Lines), $"Product {line.ProductId} is unavailable.");

            var batch = batches.FirstOrDefault(x => x.ProductBatchId == line.ProductBatchId);
            if (batch == null || batch.ProductId != line.ProductId)
                ModelState.AddModelError(nameof(model.Lines), "Each selected batch must belong to its selected product.");

            if (string.IsNullOrWhiteSpace(line.Bucket))
                ModelState.AddModelError(nameof(model.Lines), "Enter the stock bucket for every adjustment line.");
        }
    }

    private async Task LoadAdjustmentFormData()
    {
        ViewBag.Products = (await _productFacade.GetProductsAsync())
            .Where(x => x.IsActive).OrderBy(x => x.Name).ToList();
        ViewBag.Batches = (await _batchFacade.GetProductBatchesAsync()).ToList();
    }

    private async Task ReplaceAdjustmentLines(long documentId, StockAdjustmentViewModel model)
    {
        short lineNo = 1;
        foreach (var line in model.Lines)
        {
            var documentLine = new DocumentLine
            {
                DocumentId = documentId,
                LineNumber = lineNo++,
                ProductId = line.ProductId
            };
            await _documentLineFacade.AddDocumentLineAsync(documentLine);

            var changeLine = new StockChangeLine
            {
                LineId = documentLine.LineId,
                DocumentId = documentId,
                ProductId = line.ProductId,
                ProductBatchId = line.ProductBatchId,
                Bucket = line.Bucket.Trim(),
                QuantityDelta = line.QuantityDelta
            };
            await _changeLineFacade.AddStockChangeLineAsync(changeLine);
        }
    }

    private async Task DeleteAdjustmentLines(long documentId)
    {
        var lines = (await _changeLineFacade.GetStockChangeLinesAsync())
            .Where(x => x.DocumentId == documentId).ToList();
        foreach (var line in lines)
            await _changeLineFacade.DeleteStockChangeLineAsync(line.LineId);

        var documentLines = (await _documentLineFacade.GetDocumentLinesAsync())
            .Where(x => x.DocumentId == documentId).ToList();
        foreach (var line in documentLines)
            await _documentLineFacade.DeleteDocumentLineAsync(line.LineId);
    }
}
