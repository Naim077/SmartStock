
namespace AIMS.Entities;

// Maps to ss.SaleLine. UTC timestamps and posting rules are service responsibilities.
public sealed class SaleLine
{
    public long LineId { get; set; }
    public long DocumentId { get; set; }
    public long ProductId { get; set; }
    public required string ProductNameSnapshot { get; set; }
    public required string UnitSnapshot { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal ExplicitDiscountAmount { get; set; }
    public decimal InvoiceDiscountAllocation { get; set; }
    public decimal NetAmount { get; set; }
    public decimal WacAtPosting { get; set; }
    public decimal CogsValue { get; set; }

    public DocumentLine DocumentLine { get; set; } = null!;
    public Sale Sale { get; set; } = null!;
    public ICollection<SaleBatchAllocation> SaleBatchAllocations { get; set; } = new List<SaleBatchAllocation>();
}
