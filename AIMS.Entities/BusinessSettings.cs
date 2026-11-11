
namespace AIMS.Entities;

// Maps to ss.BusinessSettings. UTC timestamps and posting rules are service responsibilities.
public sealed class BusinessSettings
{
    public byte SettingsId { get; set; } = 1;
    public required string BusinessName { get; set; }
    public required string CurrencyCode { get; set; }
    public required string TimeZoneId { get; set; }
    public required string InvoicePrefix { get; set; }
    public decimal DiscountApprovalPercent { get; set; } = 10m;
    public decimal? AdjustmentApprovalThreshold { get; set; }
    public short ExpiryWarningDays { get; set; } = (short)30;
    public short DeadStockDays { get; set; } = (short)90;
    public short ReviewPeriodDays { get; set; } = (short)7;
    public decimal SafetyFactor { get; set; } = 1.65m;
    public bool LlmEnabled { get; set; } = false;
    public string? LlmProvider { get; set; }
    public string? LlmModel { get; set; }
    public string? LlmSecretReference { get; set; }
    public short LlmRequestsPerMinute { get; set; } = (short)10;
    public long LlmMonthlyTokenBudget { get; set; } = 0L;
    public bool SmtpEnabled { get; set; } = false;
    public string? SmtpHost { get; set; }
    public int? SmtpPort { get; set; }
    public string? SmtpSecretReference { get; set; }
    public string? ApprovedRecipientsJson { get; set; }
    public short ChatRetentionMonths { get; set; } = (short)12;
    public string? RetentionPolicyReference { get; set; }
    public DateTime? RetentionApprovedUtc { get; set; }
    public bool LegalHold { get; set; } = false;
    public Guid UpdatedByUserId { get; set; }
    public DateTime UpdatedUtc { get; set; }
    public byte[] RowVersion { get; set; } = [];

    public UserProfile UpdatedByUser { get; set; } = null!;
}
