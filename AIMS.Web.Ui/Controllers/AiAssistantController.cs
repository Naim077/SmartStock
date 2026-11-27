using AIMS.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIMS.Web.Ui.Controllers;

[Authorize]
public class AiAssistantController : Controller
{
    private readonly IAiChatLogFacade _chatLogFacade;
    private readonly IAiToolCallLogFacade _toolCallLogFacade;

    public AiAssistantController(IAiChatLogFacade chatLogFacade, IAiToolCallLogFacade toolCallLogFacade)
    {
        _chatLogFacade = chatLogFacade;
        _toolCallLogFacade = toolCallLogFacade;
    }

    [HttpGet]
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(long id)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Forbid();

        var chat = await _chatLogFacade.GetAiChatLogByIdAsync(id);
        if (chat == null || chat.UserId != userId)
            return NotFound();

        if (chat.LegalHold)
        {
            TempData["ErrorMessage"] = "This conversation is under legal hold and cannot be deleted.";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            // Tool-call rows reference the parent chat row and must be deleted first.
            var toolCalls = (await _toolCallLogFacade.GetAiToolCallLogsAsync())
                .Where(x => x.AiChatLogId == id).ToList();

            foreach (var toolCall in toolCalls)
                await _toolCallLogFacade.DeleteAiToolCallLogAsync(toolCall.ToolCallId);

            await _chatLogFacade.DeleteAiChatLogAsync(id);
            TempData["SuccessMessage"] = "Conversation history deleted.";
        }
        catch
        {
            TempData["ErrorMessage"] = "The conversation could not be deleted. Please try again.";
        }

        return RedirectToAction(nameof(Index));
    }
}
