
namespace AIMS.Entities;

// Maps to ss.StockChange. UTC timestamps and posting rules are service responsibilities.
public sealed class StockChange
{
    public long DocumentId { get; set; }
    public required string ChangeKind { get; set; }
    public required string Reason { get; set; }
    public string? EvidenceReference { get; set; }

    public Document Document { get; set; } = null!;
    public ICollection<StockChangeLine> StockChangeLines { get; set; } = new List<StockChangeLine>();
}
