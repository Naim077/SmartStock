using AIMS.Entities;
using AIMS.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIMS.Web.Ui.Controllers;

[Authorize]
public class SettingsController : Controller
{
    private readonly IBusinessSettingsFacade _settingsFacade;

    public SettingsController(IBusinessSettingsFacade settingsFacade)
    {
        _settingsFacade = settingsFacade;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var settings = (await _settingsFacade.GetBusinessSettingsesAsync()).FirstOrDefault();
        if (settings == null)
            return View(new BusinessSettings
            {
                BusinessName = "",
                CurrencyCode = "BDT",
                TimeZoneId = "Asia/Dhaka",
                InvoicePrefix = "INV",
                UpdatedUtc = DateTime.UtcNow
            });

        return View(settings);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(BusinessSettings model)
    {
        ModelState.Remove(nameof(BusinessSettings.RowVersion));
        ModelState.Remove(nameof(BusinessSettings.UpdatedByUserId));
        ModelState.Remove(nameof(BusinessSettings.UpdatedUtc));

        if (!ModelState.IsValid)
            return View(model);

        var existing = await _settingsFacade.GetBusinessSettingsByIdAsync(model.SettingsId);
        if (existing == null)
        {
            TempData["ErrorMessage"] = "Settings record was not found. Contact an administrator to initialize business settings.";
            return RedirectToAction(nameof(Index));
        }

        existing.BusinessName = model.BusinessName.Trim();
        existing.CurrencyCode = model.CurrencyCode.Trim().ToUpperInvariant();
        existing.TimeZoneId = model.TimeZoneId.Trim();
        existing.InvoicePrefix = model.InvoicePrefix.Trim();
        existing.DiscountApprovalPercent = model.DiscountApprovalPercent;
        existing.AdjustmentApprovalThreshold = model.AdjustmentApprovalThreshold;
        existing.ExpiryWarningDays = model.ExpiryWarningDays;
        existing.DeadStockDays = model.DeadStockDays;
        existing.ReviewPeriodDays = model.ReviewPeriodDays;
        existing.SafetyFactor = model.SafetyFactor;
        existing.LlmEnabled = model.LlmEnabled;
        existing.LlmProvider = model.LlmProvider;
        existing.LlmModel = model.LlmModel;
        existing.LlmRequestsPerMinute = model.LlmRequestsPerMinute;
        existing.LlmMonthlyTokenBudget = model.LlmMonthlyTokenBudget;
        existing.SmtpEnabled = model.SmtpEnabled;
        existing.SmtpHost = model.SmtpHost;
        existing.SmtpPort = model.SmtpPort;
        existing.ChatRetentionMonths = model.ChatRetentionMonths;
        existing.LegalHold = model.LegalHold;

        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Forbid();

        existing.UpdatedByUserId = userId;
        existing.UpdatedUtc = DateTime.UtcNow;
        await _settingsFacade.UpdateBusinessSettingsAsync(existing);
        TempData["SuccessMessage"] = "Business settings updated successfully.";
        return RedirectToAction(nameof(Index));
    }
}
