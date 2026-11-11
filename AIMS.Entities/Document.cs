
namespace AIMS.Entities;

// Maps to ss.Document. UTC timestamps and posting rules are service responsibilities.
public sealed class Document
{
    public long DocumentId { get; set; }
    public required string DocumentKind { get; set; }
    public string? DocumentNo { get; set; }
    public required string Status { get; set; }
    public int CurrentVersionNo { get; set; } = 1;
    public required string CurrencyCode { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedUtc { get; set; }
    public long? CreationRequestId { get; set; }
    public byte[] RowVersion { get; set; } = [];

    public UserProfile CreatedByUser { get; set; } = null!;
    public IdempotencyRequest? CreationRequest { get; set; }
    public ICollection<DocumentVersion> DocumentVersions { get; set; } = new List<DocumentVersion>();
    public ICollection<DocumentLine> DocumentLines { get; set; } = new List<DocumentLine>();
    public PurchaseOrder? PurchaseOrder { get; set; }
    public GoodsReceipt? GoodsReceipt { get; set; }
    public GoodsReceiptReversal? GoodsReceiptReversal { get; set; }
    public Sale? Sale { get; set; }
    public SalesReturn? SalesReturn { get; set; }
    public StockChange? StockChange { get; set; }
    public OpeningAccountBalance? OpeningAccountBalance { get; set; }
    public Payment? Payment { get; set; }
    public PaymentReversal? PaymentReversal { get; set; }
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}
