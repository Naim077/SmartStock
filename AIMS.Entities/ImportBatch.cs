using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("ImportBatch", Schema = "ss")]
public class ImportBatch
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ImportBatchId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string ImportKind { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string SourceName { get; set; } = null!;
    public byte[] ContentHash { get; set; } = null!;
    public int SourceRowCount { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string ValidationStatus { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string ValidationReportJson { get; set; } = null!;
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedUtc { get; set; }
    public Guid? ConfirmedByUserId { get; set; }
    public DateTime? ConfirmedUtc { get; set; }
    public long? RequestId { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
