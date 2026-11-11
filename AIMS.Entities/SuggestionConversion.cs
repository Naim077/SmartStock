
namespace AIMS.Entities;

// Maps to ss.SuggestionConversion. UTC timestamps and posting rules are service responsibilities.
public sealed class SuggestionConversion
{
    public long ConversionId { get; set; }
    public long SuggestionId { get; set; }
    public Guid OpportunityKey { get; set; }
    public long PurchaseOrderLineId { get; set; }
    public decimal ReviewedQuantity { get; set; }
    public decimal ReviewedUnitCost { get; set; }
    public bool UncertainOrdersAcknowledged { get; set; }
    public Guid ReviewedByUserId { get; set; }
    public DateTime ConvertedUtc { get; set; }
    public long RequestId { get; set; }
    public DateTime? ReleasedUtc { get; set; }
    public Guid? ReleasedByUserId { get; set; }
    public string? ReleaseReason { get; set; }

    public ReorderSuggestion ReorderSuggestion { get; set; } = null!;
    public PurchaseOrderLine PurchaseOrderLine { get; set; } = null!;
    public UserProfile ReviewedByUser { get; set; } = null!;
    public IdempotencyRequest IdempotencyRequest { get; set; } = null!;
    public UserProfile? ReleasedByUser { get; set; }
}
