using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("DocumentLine", Schema = "ss")]
public class DocumentLine
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long LineId { get; set; }
    public long DocumentId { get; set; }
    public short LineNumber { get; set; }
    public long ProductId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? SourceReference { get; set; }
    public int? SourceRowNumber { get; set; }
    public long? ImportBatchId { get; set; }
}
