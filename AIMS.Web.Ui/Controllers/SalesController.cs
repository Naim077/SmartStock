using AIMS.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIMS.Web.Ui.Controllers;

[Authorize]
public class SalesController : Controller
{
    private readonly ISaleFacade _saleFacade;
    private readonly IDocumentFacade _documentFacade;
    private readonly ISaleLineFacade _saleLineFacade;

    public SalesController(ISaleFacade saleFacade, IDocumentFacade documentFacade, ISaleLineFacade saleLineFacade)
    {
        _saleFacade = saleFacade;
        _documentFacade = documentFacade;
        _saleLineFacade = saleLineFacade;
    }

    public async Task<IActionResult> Index(string? search, string? status)
    {
        var sales = await _saleFacade.GetSalesAsync();
        var documents = (await _documentFacade.GetDocumentsAsync())
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
        ViewBag.LineCounts = lines.Where(x => sales.Select(s => s.DocumentId).Contains(x.DocumentId))
            .GroupBy(x => x.DocumentId).ToDictionary(x => x.Key, x => x.Count());
        ViewBag.Search = search ?? "";
        ViewBag.SelectedStatus = status ?? "All";
        ViewBag.TotalSales = sales.Count;
        ViewBag.TotalRevenue = sales.Sum(x => x.TotalAmount);
        return View(sales.OrderByDescending(x => documents[x.DocumentId].CreatedUtc).ToList());
    }
}
