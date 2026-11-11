
namespace AIMS.Entities;

// Maps to ss.GoodsReceiptLine. UTC timestamps and posting rules are service responsibilities.
public sealed class GoodsReceiptLine
{
    public long LineId { get; set; }
    public long DocumentId { get; set; }
    public long ProductId { get; set; }
    public long PurchaseOrderId { get; set; }
    public long PurchaseOrderLineId { get; set; }
    public long ProductBatchId { get; set; }
    public required string Bucket { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal InventoryValue { get; set; }
    public decimal PayableAmount { get; set; }

    public DocumentLine DocumentLine { get; set; } = null!;
    public GoodsReceipt GoodsReceipt { get; set; } = null!;
    public PurchaseOrderLine PurchaseOrderLine { get; set; } = null!;
    public ProductBatch ProductBatch { get; set; } = null!;
}
