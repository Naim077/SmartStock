
namespace AIMS.Entities;

// Maps to ss.AuditLog. UTC timestamps and posting rules are service responsibilities.
public sealed class AuditLog
{
    public long AuditLogId { get; set; }
    public Guid? ActorUserId { get; set; }
    public required string ActorType { get; set; }
    public DateTime OccurredUtc { get; set; }
    public Guid CorrelationId { get; set; }
    public required string Action { get; set; }
    public required string Outcome { get; set; }
    public long? DocumentId { get; set; }
    public long? PostingId { get; set; }
    public long? ProductId { get; set; }
    public Guid? TargetUserId { get; set; }
    public long? ImportBatchId { get; set; }
    public long? JobRunId { get; set; }
    public required string EntityType { get; set; }
    public string? EntityReference { get; set; }
    public string? BeforeRedactedJson { get; set; }
    public string? AfterRedactedJson { get; set; }

    public UserProfile? ActorUser { get; set; }
    public Document? Document { get; set; }
    public Posting? Posting { get; set; }
    public Product? Product { get; set; }
    public UserProfile? TargetUser { get; set; }
    public ImportBatch? ImportBatch { get; set; }
    public JobRun? JobRun { get; set; }
}
