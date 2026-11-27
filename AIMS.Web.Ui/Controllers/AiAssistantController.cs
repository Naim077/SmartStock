using AIMS.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIMS.Web.Ui.Controllers;

[Authorize]
public class AiAssistantController : Controller
{
    private readonly IAiChatLogFacade _chatLogFacade;

    public AiAssistantController(IAiChatLogFacade chatLogFacade)
    {
        _chatLogFacade = chatLogFacade;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userId, out var id))
            return Forbid();

        var history = (await _chatLogFacade.GetAiChatLogsAsync())
            .Where(x => x.UserId == id)
            .OrderByDescending(x => x.StartedUtc)
            .Take(50)
            .ToList();

        ViewBag.IntegrationConfigured = false;
        return View(history);
    }
}
