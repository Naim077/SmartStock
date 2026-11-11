
namespace AIMS.Entities;

// Maps to ss.DocumentLine. UTC timestamps and posting rules are service responsibilities.
public sealed class DocumentLine
{
    public long LineId { get; set; }
    public long DocumentId { get; set; }
    public short LineNumber { get; set; }
    public long ProductId { get; set; }
    public string? SourceReference { get; set; }
    public int? SourceRowNumber { get; set; }
    public long? ImportBatchId { get; set; }

    public Document Document { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public ImportBatch? ImportBatch { get; set; }
    public PurchaseOrderLine? PurchaseOrderLine { get; set; }
    public GoodsReceiptLine? GoodsReceiptLine { get; set; }
    public SaleLine? SaleLine { get; set; }
    public SalesReturnLine? SalesReturnLine { get; set; }
    public StockChangeLine? StockChangeLine { get; set; }
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}
