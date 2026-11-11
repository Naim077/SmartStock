
namespace AIMS.Entities;

// Maps to ss.PurchaseOrderLine. UTC timestamps and posting rules are service responsibilities.
public sealed class PurchaseOrderLine
{
    public long LineId { get; set; }
    public long DocumentId { get; set; }
    public long ProductId { get; set; }
    public required string ProductNameSnapshot { get; set; }
    public required string UnitSnapshot { get; set; }
    public decimal OrderedQuantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal NetReceivedQuantity { get; set; } = 0m;
    public decimal ClosedQuantity { get; set; } = 0m;

    public DocumentLine DocumentLine { get; set; } = null!;
    public PurchaseOrder PurchaseOrder { get; set; } = null!;
    public ICollection<GoodsReceiptLine> GoodsReceiptLines { get; set; } = new List<GoodsReceiptLine>();
    public ICollection<SuggestionConversion> SuggestionConversions { get; set; } = new List<SuggestionConversion>();
}
