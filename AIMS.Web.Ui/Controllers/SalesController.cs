using AIMS.DataAccess;
using AIMS.Entities;
using AIMS.Facade.Interface;
using AIMS.Web.Ui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIMS.Web.Ui.Controllers;

[Authorize]
public class SalesController : Controller
{
    private readonly ISaleFacade _saleFacade;
    private readonly IDocumentFacade _documentFacade;
    private readonly ISaleLineFacade _saleLineFacade;
    private readonly IDocumentLineFacade _documentLineFacade;
    private readonly ICustomerFacade _customerFacade;
    private readonly IProductFacade _productFacade;
    private readonly IBusinessSettingsFacade _settingsFacade;
    private readonly ApplicationDbContext _db;

    public SalesController(
        ISaleFacade saleFacade,
        IDocumentFacade documentFacade,
        ISaleLineFacade saleLineFacade,
        IDocumentLineFacade documentLineFacade,
        ICustomerFacade customerFacade,
        IProductFacade productFacade,
        IBusinessSettingsFacade settingsFacade,
        ApplicationDbContext db)
    {
        _saleFacade = saleFacade;
        _documentFacade = documentFacade;
        _saleLineFacade = saleLineFacade;
        _documentLineFacade = documentLineFacade;
        _customerFacade = customerFacade;
        _productFacade = productFacade;
        _settingsFacade = settingsFacade;
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? search, string? status)
    {
        var sales = await _saleFacade.GetSalesAsync();
        var allDocuments = await _documentFacade.GetDocumentsAsync();
        var documents = allDocuments
            .Where(x => x.DocumentKind.Equals("Sale", StringComparison.OrdinalIgnoreCase) ||
                        x.DocumentKind.Equals("SalesInvoice", StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.DocumentId);
        var lines = await _saleLineFacade.GetSaleLinesAsync();

        sales = sales.Where(x => documents.ContainsKey(x.DocumentId)).ToList();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            sales = sales.Where(x =>
                (x.CustomerNameSnapshot ?? "").Contains(term, StringComparison.OrdinalIgnoreCase) ||
                (documents[x.DocumentId].DocumentNo ?? "").Contains(term, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(status) && !status.Equals("All", StringComparison.OrdinalIgnoreCase))
            sales = sales.Where(x => documents[x.DocumentId].Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

        ViewBag.Documents = documents;
        ViewBag.LineCounts = lines.Where(x => sales.Any(s => s.DocumentId == x.DocumentId))
            .GroupBy(x => x.DocumentId).ToDictionary(x => x.Key, x => x.Count());
        ViewBag.Search = search ?? "";
        ViewBag.SelectedStatus = status ?? "All";
        ViewBag.TotalSales = sales.Count;
        ViewBag.TotalRevenue = sales.Where(x => !documents[x.DocumentId].Status.Equals("Draft", StringComparison.OrdinalIgnoreCase))
            .Sum(x => x.TotalAmount);
        return View(sales.OrderByDescending(x => documents[x.DocumentId].CreatedUtc).ToList());
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadSaleFormData();
        return View(new SaleEntryViewModel { Lines = new() { new SaleEntryLineViewModel() } });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SaleEntryViewModel model)
    {
        ValidateSaleLines(model);
        var customer = await _customerFacade.GetCustomerByIdAsync(model.CustomerId);
        if (customer == null || !customer.IsActive)
            ModelState.AddModelError(nameof(model.CustomerId), "Select an active customer.");

        var products = await _productFacade.GetProductsAsync();
        var productLookup = products.Where(x => x.IsActive).ToDictionary(x => x.ProductId);
        ValidateSelectedProducts(model, productLookup);

        if (!ModelState.IsValid || customer == null)
        {
            await LoadSaleFormData();
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
                DocumentKind = "Sale",
                Status = "Draft",
                CurrentVersionNo = 1,
                CurrencyCode = currency,
                CreatedByUserId = userId,
                CreatedUtc = DateTime.UtcNow
            };
            await _documentFacade.AddDocumentAsync(document);

            var sale = new Sale
            {
                DocumentId = document.DocumentId,
                CustomerId = customer.CustomerId,
                CustomerNameSnapshot = customer.Name,
                InvoiceDiscountAmount = 0,
                TotalAmount = CalculateTotal(model)
            };
            await _saleFacade.AddSaleAsync(sale);

            await ReplaceSaleLines(document.DocumentId, model, productLookup);
            await transaction.CommitAsync();

            TempData["SuccessMessage"] = "Sale saved as Draft. Stock has not been changed.";
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            await transaction.RollbackAsync();
            ModelState.AddModelError(string.Empty, "The sale could not be saved. No stock was posted. Please try again.");
            await LoadSaleFormData();
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var document = await _documentFacade.GetDocumentByIdAsync(id);
        var sale = await _saleFacade.GetSaleByIdAsync(id);
        if (document == null || sale == null || !IsSaleDocument(document) || !IsDraft(document))
            return NotFound();

        var saleLines = (await _saleLineFacade.GetSaleLinesAsync())
            .Where(x => x.DocumentId == id).OrderBy(x => x.LineId).ToList();

        await LoadSaleFormData();
        return View(new SaleEntryViewModel
        {
            DocumentId = id,
            CustomerId = sale.CustomerId,
            Lines = saleLines.Select(x => new SaleEntryLineViewModel
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                DiscountAmount = x.ExplicitDiscountAmount
            }).ToList()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, SaleEntryViewModel model)
    {
        if (id != model.DocumentId)
            return BadRequest();

        var document = await _documentFacade.GetDocumentByIdAsync(id);
        var sale = await _saleFacade.GetSaleByIdAsync(id);
        if (document == null || sale == null || !IsSaleDocument(document) || !IsDraft(document))
            return NotFound();

        ValidateSaleLines(model);
        var customer = await _customerFacade.GetCustomerByIdAsync(model.CustomerId);
        if (customer == null || !customer.IsActive)
            ModelState.AddModelError(nameof(model.CustomerId), "Select an active customer.");

        var products = await _productFacade.GetProductsAsync();
        var productLookup = products.Where(x => x.IsActive).ToDictionary(x => x.ProductId);
        ValidateSelectedProducts(model, productLookup);

        if (!ModelState.IsValid || customer == null)
        {
            await LoadSaleFormData();
            return View(model);
        }

        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            sale.CustomerId = customer.CustomerId;
            sale.CustomerNameSnapshot = customer.Name;
            sale.InvoiceDiscountAmount = 0;
            sale.TotalAmount = CalculateTotal(model);
            await _saleFacade.UpdateSaleAsync(sale);

            await DeleteSaleLines(id);
            await ReplaceSaleLines(id, model, productLookup);
            await transaction.CommitAsync();

            TempData["SuccessMessage"] = "Draft sale updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            await transaction.RollbackAsync();
            ModelState.AddModelError(string.Empty, "The draft sale could not be updated. Please try again.");
            await LoadSaleFormData();
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(long id)
    {
        var document = await _documentFacade.GetDocumentByIdAsync(id);
        var sale = await _saleFacade.GetSaleByIdAsync(id);
        if (document == null || sale == null || !IsSaleDocument(document) || !IsDraft(document))
        {
            TempData["ErrorMessage"] = "Only draft sales can be deleted. Posted sales must be reversed through the return/reversal workflow.";
            return RedirectToAction(nameof(Index));
        }

        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            await DeleteSaleLines(id);
            await _saleFacade.DeleteSaleAsync(id);
            await _documentFacade.DeleteDocumentAsync(id);
            await transaction.CommitAsync();
            TempData["SuccessMessage"] = "Draft sale deleted successfully.";
        }
        catch
        {
            await transaction.RollbackAsync();
            TempData["ErrorMessage"] = "The draft sale could not be deleted.";
        }

        return RedirectToAction(nameof(Index));
    }

    private static bool IsSaleDocument(Document document) =>
        document.DocumentKind.Equals("Sale", StringComparison.OrdinalIgnoreCase) ||
        document.DocumentKind.Equals("SalesInvoice", StringComparison.OrdinalIgnoreCase);

    private static bool IsDraft(Document document) =>
        document.Status.Equals("Draft", StringComparison.OrdinalIgnoreCase);

    private void ValidateSaleLines(SaleEntryViewModel model)
    {
        if (model.Lines == null || model.Lines.Count == 0)
        {
            ModelState.AddModelError(nameof(model.Lines), "Add at least one sale item.");
            return;
        }

        if (model.Lines.Any(x => x.Quantity <= 0 || x.UnitPrice < 0 || x.DiscountAmount < 0 ||
                                 x.DiscountAmount > x.Quantity * x.UnitPrice))
            ModelState.AddModelError(nameof(model.Lines), "Quantities, prices and line discounts must be valid. A discount cannot exceed its line amount.");
    }

    private void ValidateSelectedProducts(SaleEntryViewModel model, Dictionary<long, Product> products)
    {
        foreach (var line in model.Lines ?? new())
            if (!products.ContainsKey(line.ProductId))
                ModelState.AddModelError(nameof(model.Lines), $"Product {line.ProductId} is not active or does not exist.");
    }

    private static decimal CalculateTotal(SaleEntryViewModel model) =>
        model.Lines.Sum(x => (x.Quantity * x.UnitPrice) - x.DiscountAmount);

    private async Task LoadSaleFormData()
    {
        ViewBag.Customers = (await _customerFacade.GetCustomersAsync())
            .Where(x => x.IsActive).OrderBy(x => x.Name).ToList();
        ViewBag.Products = (await _productFacade.GetProductsAsync())
            .Where(x => x.IsActive).OrderBy(x => x.Name).ToList();
    }

    private async Task ReplaceSaleLines(long documentId, SaleEntryViewModel model, Dictionary<long, Product> products)
    {
        short number = 1;
        foreach (var line in model.Lines)
        {
            var product = products[line.ProductId];
            var documentLine = new DocumentLine
            {
                DocumentId = documentId,
                LineNumber = number++,
                ProductId = product.ProductId
            };
            await _documentLineFacade.AddDocumentLineAsync(documentLine);

            var gross = line.Quantity * line.UnitPrice;
            var net = gross - line.DiscountAmount;
            var saleLine = new SaleLine
            {
                LineId = documentLine.LineId,
                DocumentId = documentId,
                ProductId = product.ProductId,
                ProductNameSnapshot = product.Name,
                UnitSnapshot = product.BaseUnit,
                Quantity = line.Quantity,
                UnitPrice = line.UnitPrice,
                GrossAmount = gross,
                ExplicitDiscountAmount = line.DiscountAmount,
                InvoiceDiscountAllocation = 0,
                NetAmount = net,
                WacAtPosting = 0,
                CogsValue = 0
            };
            await _saleLineFacade.AddSaleLineAsync(saleLine);
        }
    }

    private async Task DeleteSaleLines(long documentId)
    {
        var lines = (await _saleLineFacade.GetSaleLinesAsync())
            .Where(x => x.DocumentId == documentId).ToList();
        foreach (var line in lines)
            await _saleLineFacade.DeleteSaleLineAsync(line.LineId);

        var documentLines = (await _documentLineFacade.GetDocumentLinesAsync())
            .Where(x => x.DocumentId == documentId).ToList();
        foreach (var line in documentLines)
            await _documentLineFacade.DeleteDocumentLineAsync(line.LineId);
    }
}
