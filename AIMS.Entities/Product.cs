
namespace AIMS.Entities;

// Maps to ss.Product. UTC timestamps and posting rules are service responsibilities.
public sealed class Product
{
    public long ProductId { get; set; }
    public long CategoryId { get; set; }
    public long? PreferredSupplierId { get; set; }
    public required string Sku { get; set; }
    public string? NormalizedSku { get; private set; }
    public string? Barcode { get; set; }
    public string? NormalizedBarcode { get; private set; }
    public required string Name { get; set; }
    public required string BaseUnit { get; set; }
    public byte QuantityScale { get; set; }
    public decimal? QuantityIncrement { get; private set; }
    public decimal SellingUnitPrice { get; set; }
    public decimal DefaultPurchaseUnitCost { get; set; }
    public required string ReorderMode { get; set; }
    public decimal ManualReorderPoint { get; set; }
    public decimal PackQuantity { get; set; }
    public decimal MinimumOrderQuantity { get; set; }
    public bool ExpiryTracked { get; set; } = false;
    public string? ImageAssetReference { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime ActivatedUtc { get; set; }
    public DateTime? ArchivedUtc { get; set; }
    public byte[] RowVersion { get; set; } = [];

    public Category Category { get; set; } = null!;
    public Supplier? PreferredSupplier { get; set; }
    public ICollection<ProductBatch> ProductBatches { get; set; } = new List<ProductBatch>();
    public ICollection<DocumentLine> DocumentLines { get; set; } = new List<DocumentLine>();
    public ProductStockBalance? ProductStockBalance { get; set; }
    public ICollection<ForecastRun> ForecastRuns { get; set; } = new List<ForecastRun>();
    public ICollection<ReorderSuggestion> ReorderSuggestions { get; set; } = new List<ReorderSuggestion>();
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
}
