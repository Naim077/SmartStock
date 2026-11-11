
namespace AIMS.Entities;

// Maps to ss.ProductStockBalance. UTC timestamps and posting rules are service responsibilities.
public sealed class ProductStockBalance
{
    public long ProductId { get; set; }
    public decimal Quantity { get; set; } = 0m;
    public decimal InventoryValue { get; set; } = 0m;
    public long MovementVersion { get; set; } = 0L;
    public long? LastPostingId { get; set; }
    public byte[] RowVersion { get; set; } = [];

    public Product Product { get; set; } = null!;
    public Posting? LastPosting { get; set; }
}
