using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("AuditLog", Schema = "ss")]
public class AuditLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long AuditLogId { get; set; }
    public Guid? ActorUserId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string ActorType { get; set; } = null!;
    public DateTime OccurredUtc { get; set; }
    public Guid CorrelationId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Action { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string Outcome { get; set; } = null!;
    public long? DocumentId { get; set; }
    public long? PostingId { get; set; }
    public long? ProductId { get; set; }
    public Guid? TargetUserId { get; set; }
    public long? ImportBatchId { get; set; }
    public long? JobRunId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string EntityType { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string? EntityReference { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? BeforeRedactedJson { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? AfterRedactedJson { get; set; }
}
