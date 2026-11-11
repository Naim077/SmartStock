
namespace AIMS.Entities;

// Maps to ss.OpeningAccountBalance. UTC timestamps and posting rules are service responsibilities.
public sealed class OpeningAccountBalance
{
    public long DocumentId { get; set; }
    public long? CustomerId { get; set; }
    public long? SupplierId { get; set; }
    public decimal Amount { get; set; }
    public required string SourceReference { get; set; }
    public int SourceRowNumber { get; set; }
    public long? ImportBatchId { get; set; }

    public Document Document { get; set; } = null!;
    public Customer? Customer { get; set; }
    public Supplier? Supplier { get; set; }
    public ImportBatch? ImportBatch { get; set; }
}
