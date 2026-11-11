
namespace AIMS.Entities;

// Maps to ss.Sale. UTC timestamps and posting rules are service responsibilities.
public sealed class Sale
{
    public long DocumentId { get; set; }
    public long CustomerId { get; set; }
    public required string CustomerNameSnapshot { get; set; }
    public decimal InvoiceDiscountAmount { get; set; } = 0m;
    public decimal TotalAmount { get; set; }

    public Document Document { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
    public ICollection<SaleLine> SaleLines { get; set; } = new List<SaleLine>();
    public ICollection<SalesReturn> SalesReturns { get; set; } = new List<SalesReturn>();
}
