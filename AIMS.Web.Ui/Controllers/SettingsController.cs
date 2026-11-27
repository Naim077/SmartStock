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
        {
            ViewBag.IsNewSettings = true;
            return View(new BusinessSettings
            {
                SettingsId = 1,
                BusinessName = "",
                CurrencyCode = "BDT",
                TimeZoneId = "Asia/Dhaka",
                InvoicePrefix = "INV",
                DiscountApprovalPercent = 0,
                ExpiryWarningDays = 30,
                DeadStockDays = 90,
                ReviewPeriodDays = 7,
                SafetyFactor = 1,
                LlmRequestsPerMinute = 0,
                LlmMonthlyTokenBudget = 0,
                ChatRetentionMonths = 12
            });
        }

        ViewBag.IsNewSettings = false;
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
        {
            ViewBag.IsNewSettings = model.SettingsId == 0 ||
                await _settingsFacade.GetBusinessSettingsByIdAsync(model.SettingsId) == null;
            return View(model);
        }

        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Forbid();

        var existing = await _settingsFacade.GetBusinessSettingsByIdAsync(model.SettingsId);
        if (existing == null)
        {
            // This application uses one business-settings row. Initialize it once with key 1.
            if (model.SettingsId == 0)
                model.SettingsId = 1;

            var anotherSettingsRow = (await _settingsFacade.GetBusinessSettingsesAsync()).FirstOrDefault();
            if (anotherSettingsRow != null && anotherSettingsRow.SettingsId != model.SettingsId)
            {
                TempData["ErrorMessage"] = "A business-settings record already exists. Reload the page and edit that record.";
                return RedirectToAction(nameof(Index));
            }

            model.BusinessName = model.BusinessName.Trim();
            model.CurrencyCode = model.CurrencyCode.Trim().ToUpperInvariant();
            model.TimeZoneId = model.TimeZoneId.Trim();
            model.InvoicePrefix = model.InvoicePrefix.Trim();
            model.UpdatedByUserId = userId;
            model.UpdatedUtc = DateTime.UtcNow;
            await _settingsFacade.AddBusinessSettingsAsync(model);
            TempData["SuccessMessage"] = "Business settings initialized successfully.";
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
        existing.UpdatedByUserId = userId;
        existing.UpdatedUtc = DateTime.UtcNow;

        await _settingsFacade.UpdateBusinessSettingsAsync(existing);
        TempData["SuccessMessage"] = "Business settings updated successfully.";
        return RedirectToAction(nameof(Index));
    }
}
