
namespace AIMS.Entities;

// Maps to ss.Posting. UTC timestamps and posting rules are service responsibilities.
public sealed class Posting
{
    public long PostingId { get; set; }
    public long DocumentId { get; set; }
    public int VersionNo { get; set; }
    public long RequestId { get; set; }
    public Guid ActorUserId { get; set; }
    public DateTime PostedUtc { get; set; }
    public DateOnly BusinessDate { get; set; }
    public Guid CorrelationId { get; set; }

    public DocumentVersion DocumentVersion { get; set; } = null!;
    public IdempotencyRequest IdempotencyRequest { get; set; } = null!;
    public UserProfile ActorUser { get; set; } = null!;
    public GoodsReceiptReversal? GoodsReceiptReversal { get; set; }
    public SalesReturn? SalesReturn { get; set; }
    public ICollection<ProductStockBalance> ProductStockBalances { get; set; } = new List<ProductStockBalance>();
    public ICollection<StockBalance> StockBalances { get; set; } = new List<StockBalance>();
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    public ICollection<AccountEntry> AccountEntries { get; set; } = new List<AccountEntry>();
    public PaymentReversal? PaymentReversal { get; set; }
    public ICollection<AiToolCallLog> AiToolCallLogs { get; set; } = new List<AiToolCallLog>();
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}
