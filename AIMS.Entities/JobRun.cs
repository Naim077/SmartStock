
namespace AIMS.Entities;

// Maps to ss.JobRun. UTC timestamps and posting rules are service responsibilities.
public sealed class JobRun
{
    public long JobRunId { get; set; }
    public required string JobType { get; set; }
    public required string JobKey { get; set; }
    public required string Status { get; set; }
    public Guid? RequestedByUserId { get; set; }
    public long? RequestId { get; set; }
    public DateTime QueuedUtc { get; set; }
    public DateTime? StartedUtc { get; set; }
    public DateTime? CompletedUtc { get; set; }
    public DateTime? LastSuccessUtc { get; set; }
    public int AttemptCount { get; set; } = 0;
    public string? CheckpointJson { get; set; }
    public string? SafeErrorCode { get; set; }
    public string? SafeErrorMessage { get; set; }
    public byte[] RowVersion { get; set; } = [];

    public UserProfile? RequestedByUser { get; set; }
    public IdempotencyRequest? IdempotencyRequest { get; set; }
    public ICollection<ForecastRun> ForecastRuns { get; set; } = new List<ForecastRun>();
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
    public ICollection<NotificationDelivery> NotificationDeliveries { get; set; } = new List<NotificationDelivery>();
}
