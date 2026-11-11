
namespace AIMS.Entities;

// Maps to ss.ReorderSuggestion. UTC timestamps and posting rules are service responsibilities.
public sealed class ReorderSuggestion
{
    public long SuggestionId { get; set; }
    public Guid OpportunityKey { get; set; }
    public long ProductId { get; set; }
    public long? SupplierId { get; set; }
    public long? ForecastRunId { get; set; }
    public DateTime GeneratedUtc { get; set; }
    public DateTime InputAsOfUtc { get; set; }
    public required string CalculationVersion { get; set; }
    public required byte[] InputHash { get; set; }
    public required string InputSnapshotJson { get; set; }
    public required string Status { get; set; }
    public required string ReorderModeSnapshot { get; set; }
    public long ProductMovementVersion { get; set; }
    public decimal SaleableQuantity { get; set; }
    public decimal EligibleOnOrderQuantity { get; set; }
    public decimal UncertainOnOrderQuantity { get; set; }
    public decimal FutureOnOrderQuantity { get; set; }
    public decimal PendingDraftQuantity { get; set; }
    public decimal? DailyDemand { get; set; }
    public decimal? DemandSigma { get; set; }
    public decimal? LeadTimeDays { get; set; }
    public required string LeadTimeSource { get; set; }
    public short LeadTimeSampleCount { get; set; }
    public short ReviewPeriodDays { get; set; }
    public decimal SafetyFactor { get; set; }
    public decimal QuantityIncrement { get; set; }
    public decimal? PackQuantity { get; set; }
    public decimal? MinimumOrderQuantity { get; set; }
    public decimal? ProposedUnitCost { get; set; }
    public decimal SavedManualRop { get; set; }
    public decimal? CalculatedRop { get; set; }
    public decimal? EffectiveRop { get; set; }
    public decimal? SafetyStock { get; set; }
    public decimal? TargetQuantity { get; set; }
    public decimal? RawSuggestedQuantity { get; set; }
    public decimal? SuggestedQuantity { get; set; }
    public decimal? EstimatedCost { get; set; }
    public decimal? DaysOfStock { get; set; }
    public bool IsFallback { get; set; }
    public required string Explanation { get; set; }
    public required string MissingInputsJson { get; set; }
    public Guid? OutcomeByUserId { get; set; }
    public DateTime? OutcomeUtc { get; set; }
    public string? OutcomeReason { get; set; }
    public byte[] RowVersion { get; set; } = [];

    public Product Product { get; set; } = null!;
    public Supplier? Supplier { get; set; }
    public ForecastRun? ForecastRun { get; set; }
    public UserProfile? OutcomeByUser { get; set; }
    public ICollection<SuggestionConversion> SuggestionConversions { get; set; } = new List<SuggestionConversion>();
    public ICollection<AiToolCallLog> AiToolCallLogs { get; set; } = new List<AiToolCallLog>();
}
