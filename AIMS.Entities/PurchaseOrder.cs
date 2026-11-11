
namespace AIMS.Entities;

// Maps to ss.PurchaseOrder. UTC timestamps and posting rules are service responsibilities.
public sealed class PurchaseOrder
{
    public long DocumentId { get; set; }
    public long SupplierId { get; set; }
    public DateOnly? ExpectedDate { get; set; }
    public required string SupplierNameSnapshot { get; set; }
    public string? SupplierAddressSnapshot { get; set; }
    public short PaymentTermsDaysSnapshot { get; set; }
    public DateTime? ApprovedUtc { get; set; }
    public DateTime? FinalReceiptUtc { get; set; }
    public Guid? ClosedOrCancelledByUserId { get; set; }
    public DateTime? ClosedOrCancelledUtc { get; set; }
    public string? CloseReason { get; set; }

    public Document Document { get; set; } = null!;
    public Supplier Supplier { get; set; } = null!;
    public UserProfile? ClosedOrCancelledByUser { get; set; }
    public ICollection<PurchaseOrderLine> PurchaseOrderLines { get; set; } = new List<PurchaseOrderLine>();
    public ICollection<GoodsReceipt> GoodsReceipts { get; set; } = new List<GoodsReceipt>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
