
namespace AIMS.Entities;

// Maps to ss.AccountAllocation. UTC timestamps and posting rules are service responsibilities.
public sealed class AccountAllocation
{
    public long AllocationId { get; set; }
    public long CreditEntryId { get; set; }
    public long DebitEntryId { get; set; }
    public decimal Amount { get; set; }
    public required string AllocationKind { get; set; }
    public long? ReversesAllocationId { get; set; }
    public long RequestId { get; set; }
    public Guid ActorUserId { get; set; }
    public DateTime AllocatedUtc { get; set; }
    public string? Reason { get; set; }

    public AccountEntry CreditEntry { get; set; } = null!;
    public AccountEntry DebitEntry { get; set; } = null!;
    public AccountAllocation? ReversesAllocation { get; set; }
    public IdempotencyRequest IdempotencyRequest { get; set; } = null!;
    public UserProfile ActorUser { get; set; } = null!;
    public AccountAllocation? AccountAllocationAsReversesAllocation { get; set; }
}
