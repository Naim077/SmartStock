
namespace AIMS.Entities;

// Maps to ss.GoodsReceiptReversal. UTC timestamps and posting rules are service responsibilities.
public sealed class GoodsReceiptReversal
{
    public long DocumentId { get; set; }
    public long OriginalGoodsReceiptId { get; set; }
    public long PostingId { get; set; }
    public required string Reason { get; set; }

    public Document Document { get; set; } = null!;
    public GoodsReceipt OriginalGoodsReceipt { get; set; } = null!;
    public Posting Posting { get; set; } = null!;
}
