
namespace AIMS.Entities;

// Maps to ss.NotificationDelivery. UTC timestamps and posting rules are service responsibilities.
public sealed class NotificationDelivery
{
    public long DeliveryId { get; set; }
    public long AlertId { get; set; }
    public long? JobRunId { get; set; }
    public required string NotificationKey { get; set; }
    public required string ApprovedRecipientReference { get; set; }
    public required string Status { get; set; }
    public byte AttemptCount { get; set; } = (byte)0;
    public DateTime CreatedUtc { get; set; }
    public DateTime? LastAttemptUtc { get; set; }
    public DateTime? NextAttemptUtc { get; set; }
    public DateTime? SentUtc { get; set; }
    public string? SafeErrorCode { get; set; }
    public byte[] RowVersion { get; set; } = [];

    public Alert Alert { get; set; } = null!;
    public JobRun? JobRun { get; set; }
}
