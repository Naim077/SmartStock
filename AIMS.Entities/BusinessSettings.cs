using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("BusinessSettings", Schema = "ss")]
public class BusinessSettings
{
    [Key]
    public byte SettingsId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string BusinessName { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string CurrencyCode { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string TimeZoneId { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string InvoicePrefix { get; set; } = null!;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal DiscountApprovalPercent { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? AdjustmentApprovalThreshold { get; set; }
    public short ExpiryWarningDays { get; set; }
    public short DeadStockDays { get; set; }
    public short ReviewPeriodDays { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal SafetyFactor { get; set; }
    public bool LlmEnabled { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? LlmProvider { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? LlmModel { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? LlmSecretReference { get; set; }
    public short LlmRequestsPerMinute { get; set; }
    public long LlmMonthlyTokenBudget { get; set; }
    public bool SmtpEnabled { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? SmtpHost { get; set; }
    public int? SmtpPort { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? SmtpSecretReference { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? ApprovedRecipientsJson { get; set; }
    public short ChatRetentionMonths { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? RetentionPolicyReference { get; set; }
    public DateTime? RetentionApprovedUtc { get; set; }
    public bool LegalHold { get; set; }
    public Guid UpdatedByUserId { get; set; }
    public DateTime UpdatedUtc { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
