using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("JobRun", Schema = "ss")]
public class JobRun
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long JobRunId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string JobType { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string JobKey { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string Status { get; set; } = null!;
    public Guid? RequestedByUserId { get; set; }
    public long? RequestId { get; set; }
    public DateTime QueuedUtc { get; set; }
    public DateTime? StartedUtc { get; set; }
    public DateTime? CompletedUtc { get; set; }
    public DateTime? LastSuccessUtc { get; set; }
    public int AttemptCount { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? CheckpointJson { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? SafeErrorCode { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? SafeErrorMessage { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
