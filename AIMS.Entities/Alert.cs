
namespace AIMS.Entities;

// Maps to ss.Alert. UTC timestamps and posting rules are service responsibilities.
public sealed class Alert
{
    public long AlertId { get; set; }
    public required string ConditionKey { get; set; }
    public required string AlertKind { get; set; }
    public required string Severity { get; set; }
    public long? ProductId { get; set; }
    public long? ProductBatchId { get; set; }
    public long? JobRunId { get; set; }
    public required string SafeMessage { get; set; }
    public DateTime RaisedUtc { get; set; }
    public DateTime LastObservedUtc { get; set; }
    public Guid? AcknowledgedByUserId { get; set; }
    public DateTime? AcknowledgedUtc { get; set; }
    public DateTime? ResolvedUtc { get; set; }
    public byte[] RowVersion { get; set; } = [];

    public Product? Product { get; set; }
    public ProductBatch? ProductBatch { get; set; }
    public JobRun? JobRun { get; set; }
    public UserProfile? AcknowledgedByUser { get; set; }
    public ICollection<NotificationDelivery> NotificationDeliveries { get; set; } = new List<NotificationDelivery>();
}
