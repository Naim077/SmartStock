using AIMS.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIMS.Web.Ui.Controllers;

[Authorize]
public class AuditLogController : Controller
{
    private readonly IAuditLogFacade _auditLogFacade;

    public AuditLogController(IAuditLogFacade auditLogFacade)
    {
        _auditLogFacade = auditLogFacade;
    }

    public async Task<IActionResult> Index(string? search)
    {
        var logs = await _auditLogFacade.GetAuditLogsAsync();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            logs = logs.Where(x => x.Action.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                   x.EntityType.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                   (x.EntityReference ?? "").Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                   x.Outcome.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        ViewBag.Search = search ?? "";
        ViewBag.TotalLogs = logs.Count;
        return View(logs.OrderByDescending(x => x.OccurredUtc).Take(250).ToList());
    }
}
