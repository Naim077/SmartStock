
namespace AIMS.Entities;

// Maps to ss.SaleBatchAllocation. UTC timestamps and posting rules are service responsibilities.
public sealed class SaleBatchAllocation
{
    public long AllocationId { get; set; }
    public long SaleLineId { get; set; }
    public long SaleId { get; set; }
    public long ProductId { get; set; }
    public long ProductBatchId { get; set; }
    public decimal Quantity { get; set; }
    public decimal InventoryValue { get; set; }

    public SaleLine SaleLine { get; set; } = null!;
    public ProductBatch ProductBatch { get; set; } = null!;
    public ICollection<SalesReturnLine> SalesReturnLines { get; set; } = new List<SalesReturnLine>();
}
