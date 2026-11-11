
namespace AIMS.Entities;

// Maps to ss.AccountEntry. UTC timestamps and posting rules are service responsibilities.
public sealed class AccountEntry
{
    public long AccountEntryId { get; set; }
    public long PostingId { get; set; }
    public long? CustomerId { get; set; }
    public long? SupplierId { get; set; }
    public required string EntryKind { get; set; }
    public required string Side { get; set; }
    public decimal Amount { get; set; }

    public Posting Posting { get; set; } = null!;
    public Customer? Customer { get; set; }
    public Supplier? Supplier { get; set; }
    public ICollection<AccountAllocation> AccountAllocations { get; set; } = new List<AccountAllocation>();
    public ICollection<AccountAllocation> AccountAllocationsAsDebitEntry { get; set; } = new List<AccountAllocation>();
}
