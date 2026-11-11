
namespace AIMS.Entities;

// Maps to ss.Payment. UTC timestamps and posting rules are service responsibilities.
public sealed class Payment
{
    public long DocumentId { get; set; }
    public required string PaymentKind { get; set; }
    public long? CustomerId { get; set; }
    public long? SupplierId { get; set; }
    public long? PurchaseOrderId { get; set; }
    public decimal Amount { get; set; }
    public required string Method { get; set; }
    public string? ExternalReference { get; set; }
    public DateOnly? ExternalEffectiveDate { get; set; }
    public decimal? CashTenderedAmount { get; set; }
    public decimal? CashChangeAmount { get; set; }

    public Document Document { get; set; } = null!;
    public Customer? Customer { get; set; }
    public Supplier? Supplier { get; set; }
    public PurchaseOrder? PurchaseOrder { get; set; }
    public PaymentReversal? PaymentReversal { get; set; }
    public ICollection<PaymentSettlementEvent> PaymentSettlementEvents { get; set; } = new List<PaymentSettlementEvent>();
}
