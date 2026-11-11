
namespace AIMS.Entities;

// Maps to ss.SalesReturnLine. UTC timestamps and posting rules are service responsibilities.
public sealed class SalesReturnLine
{
    public long LineId { get; set; }
    public long DocumentId { get; set; }
    public long ProductId { get; set; }
    public long OriginalSaleId { get; set; }
    public long OriginalAllocationId { get; set; }
    public decimal Quantity { get; set; }
    public required string Disposition { get; set; }
    public decimal CreditAmount { get; set; }
    public decimal RestockValue { get; set; }

    public DocumentLine DocumentLine { get; set; } = null!;
    public SalesReturn SalesReturn { get; set; } = null!;
    public SaleBatchAllocation OriginalAllocation { get; set; } = null!;
}
