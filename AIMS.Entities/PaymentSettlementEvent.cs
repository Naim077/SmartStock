
namespace AIMS.Entities;

// Maps to ss.PaymentSettlementEvent. UTC timestamps and posting rules are service responsibilities.
public sealed class PaymentSettlementEvent
{
    public long EventId { get; set; }
    public long PaymentId { get; set; }
    public int EventNo { get; set; }
    public required string SettlementStatus { get; set; }
    public string? ExternalReference { get; set; }
    public string? EvidenceReference { get; set; }
    public Guid RecordedByUserId { get; set; }
    public DateTime RecordedUtc { get; set; }

    public Payment Payment { get; set; } = null!;
    public UserProfile RecordedByUser { get; set; } = null!;
}
