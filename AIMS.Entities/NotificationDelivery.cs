using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("NotificationDelivery", Schema = "ss")]
public class NotificationDelivery
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long DeliveryId { get; set; }
    public long AlertId { get; set; }
    public long? JobRunId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string NotificationKey { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string ApprovedRecipientReference { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string Status { get; set; } = null!;
    public byte AttemptCount { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime? LastAttemptUtc { get; set; }
    public DateTime? NextAttemptUtc { get; set; }
    public DateTime? SentUtc { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? SafeErrorCode { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
