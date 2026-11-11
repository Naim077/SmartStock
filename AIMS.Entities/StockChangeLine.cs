
namespace AIMS.Entities;

// Maps to ss.StockChangeLine. UTC timestamps and posting rules are service responsibilities.
public sealed class StockChangeLine
{
    public long LineId { get; set; }
    public long DocumentId { get; set; }
    public long ProductId { get; set; }
    public long ProductBatchId { get; set; }
    public required string Bucket { get; set; }
    public string? ToBucket { get; set; }
    public decimal QuantityDelta { get; set; }
    public decimal? ApprovedUnitCost { get; set; }
    public decimal? ExplicitValueDelta { get; set; }
    public decimal? ExpectedQuantity { get; set; }
    public decimal? CountedQuantity { get; set; }
    public long? CapturedProductMovementVersion { get; set; }
    public long? CapturedBucketMovementVersion { get; set; }

    public DocumentLine DocumentLine { get; set; } = null!;
    public StockChange StockChange { get; set; } = null!;
    public ProductBatch ProductBatch { get; set; } = null!;
}
