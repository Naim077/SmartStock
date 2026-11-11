
namespace AIMS.Entities;

// Maps to ss.GoodsReceipt. UTC timestamps and posting rules are service responsibilities.
public sealed class GoodsReceipt
{
    public long DocumentId { get; set; }
    public long PurchaseOrderId { get; set; }
    public string? SupplierDeliveryReference { get; set; }

    public Document Document { get; set; } = null!;
    public PurchaseOrder PurchaseOrder { get; set; } = null!;
    public ICollection<GoodsReceiptLine> GoodsReceiptLines { get; set; } = new List<GoodsReceiptLine>();
    public GoodsReceiptReversal? GoodsReceiptReversal { get; set; }
}
