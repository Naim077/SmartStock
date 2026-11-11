
namespace AIMS.Entities;

// Maps to ss.ProductBatch. UTC timestamps and posting rules are service responsibilities.
public sealed class ProductBatch
{
    public long ProductBatchId { get; set; }
    public long ProductId { get; set; }
    public bool IsUnbatched { get; set; }
    public string? BatchNumber { get; set; }
    public string? NormalizedBatchNumber { get; private set; }
    public DateOnly? ExpiryDate { get; set; }
    public DateTime? FirstReceivedUtc { get; set; }
    public byte[] RowVersion { get; set; } = [];

    public Product Product { get; set; } = null!;
    public ICollection<GoodsReceiptLine> GoodsReceiptLines { get; set; } = new List<GoodsReceiptLine>();
    public ICollection<SaleBatchAllocation> SaleBatchAllocations { get; set; } = new List<SaleBatchAllocation>();
    public ICollection<StockChangeLine> StockChangeLines { get; set; } = new List<StockChangeLine>();
    public ICollection<StockBalance> StockBalances { get; set; } = new List<StockBalance>();
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
}
