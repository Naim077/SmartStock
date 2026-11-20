using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("ReorderSuggestion", Schema = "ss")]
public class ReorderSuggestion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long SuggestionId { get; set; }
    public Guid OpportunityKey { get; set; }
    public long ProductId { get; set; }
    public long? SupplierId { get; set; }
    public long? ForecastRunId { get; set; }
    public DateTime GeneratedUtc { get; set; }
    public DateTime InputAsOfUtc { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string CalculationVersion { get; set; } = null!;
    public byte[] InputHash { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string InputSnapshotJson { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string Status { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string ReorderModeSnapshot { get; set; } = null!;
    public long ProductMovementVersion { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal SaleableQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal EligibleOnOrderQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal UncertainOnOrderQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal FutureOnOrderQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal PendingDraftQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DailyDemand { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DemandSigma { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? LeadTimeDays { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string LeadTimeSource { get; set; } = null!;
    public short LeadTimeSampleCount { get; set; }
    public short ReviewPeriodDays { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal SafetyFactor { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal QuantityIncrement { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PackQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? MinimumOrderQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ProposedUnitCost { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal SavedManualRop { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CalculatedRop { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? EffectiveRop { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SafetyStock { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TargetQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? RawSuggestedQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SuggestedQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? EstimatedCost { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DaysOfStock { get; set; }
    public bool IsFallback { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Explanation { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string MissingInputsJson { get; set; } = null!;
    public Guid? OutcomeByUserId { get; set; }
    public DateTime? OutcomeUtc { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? OutcomeReason { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
