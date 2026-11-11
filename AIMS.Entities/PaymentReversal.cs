
namespace AIMS.Entities;

// Maps to ss.PaymentReversal. UTC timestamps and posting rules are service responsibilities.
public sealed class PaymentReversal
{
    public long DocumentId { get; set; }
    public long OriginalPaymentId { get; set; }
    public long PostingId { get; set; }
    public required string Reason { get; set; }

    public Document Document { get; set; } = null!;
    public Payment OriginalPayment { get; set; } = null!;
    public Posting Posting { get; set; } = null!;
}
