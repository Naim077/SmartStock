
namespace AIMS.Entities;

// Maps to ss.StockBalance. UTC timestamps and posting rules are service responsibilities.
public sealed class StockBalance
{
    public long ProductBatchId { get; set; }
    public required string Bucket { get; set; }
    public long ProductId { get; set; }
    public decimal Quantity { get; set; } = 0m;
    public long MovementVersion { get; set; } = 0L;
    public long? LastPostingId { get; set; }
    public byte[] RowVersion { get; set; } = [];

    public ProductBatch ProductBatch { get; set; } = null!;
    public Posting? LastPosting { get; set; }
}
