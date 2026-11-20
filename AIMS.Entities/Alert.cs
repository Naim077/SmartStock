using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("Alert", Schema = "ss")]
public class Alert
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long AlertId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string ConditionKey { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string AlertKind { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string Severity { get; set; } = null!;
    public long? ProductId { get; set; }
    public long? ProductBatchId { get; set; }
    public long? JobRunId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string SafeMessage { get; set; } = null!;
    public DateTime RaisedUtc { get; set; }
    public DateTime LastObservedUtc { get; set; }
    public Guid? AcknowledgedByUserId { get; set; }
    public DateTime? AcknowledgedUtc { get; set; }
    public DateTime? ResolvedUtc { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
