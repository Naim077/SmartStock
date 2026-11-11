
namespace AIMS.Entities;

// Maps to ss.SalesReturn. UTC timestamps and posting rules are service responsibilities.
public sealed class SalesReturn
{
    public long DocumentId { get; set; }
    public long OriginalSaleId { get; set; }
    public required string ReturnMode { get; set; }
    public long? PostingId { get; set; }
    public required string Reason { get; set; }

    public Document Document { get; set; } = null!;
    public Sale OriginalSale { get; set; } = null!;
    public Posting? Posting { get; set; }
    public ICollection<SalesReturnLine> SalesReturnLines { get; set; } = new List<SalesReturnLine>();
}
