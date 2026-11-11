
namespace AIMS.Entities;

// Maps to ss.ImportBatch. UTC timestamps and posting rules are service responsibilities.
public sealed class ImportBatch
{
    public long ImportBatchId { get; set; }
    public required string ImportKind { get; set; }
    public required string SourceName { get; set; }
    public required byte[] ContentHash { get; set; }
    public int SourceRowCount { get; set; }
    public required string ValidationStatus { get; set; }
    public required string ValidationReportJson { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedUtc { get; set; }
    public Guid? ConfirmedByUserId { get; set; }
    public DateTime? ConfirmedUtc { get; set; }
    public long? RequestId { get; set; }
    public byte[] RowVersion { get; set; } = [];

    public UserProfile CreatedByUser { get; set; } = null!;
    public UserProfile? ConfirmedByUser { get; set; }
    public IdempotencyRequest? IdempotencyRequest { get; set; }
    public ICollection<DocumentLine> DocumentLines { get; set; } = new List<DocumentLine>();
    public ICollection<OpeningAccountBalance> OpeningAccountBalances { get; set; } = new List<OpeningAccountBalance>();
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}
