
namespace AIMS.Entities;

// Maps to ss.StockMovement. UTC timestamps and posting rules are service responsibilities.
public sealed class StockMovement
{
    public long StockMovementId { get; set; }
    public long PostingId { get; set; }
    public long SourceLineId { get; set; }
    public long ProductId { get; set; }
    public long ProductBatchId { get; set; }
    public required string Bucket { get; set; }
    public required string MovementKind { get; set; }
    public decimal QuantityDelta { get; set; }
    public decimal ValueDelta { get; set; }
    public long? ReversesMovementId { get; set; }
    public string? Reason { get; set; }

    public Posting Posting { get; set; } = null!;
    public DocumentLine SourceLine { get; set; } = null!;
    public ProductBatch ProductBatch { get; set; } = null!;
    public StockMovement? ReversesMovement { get; set; }
    public StockMovement? StockMovementAsReversesMovement { get; set; }
}
