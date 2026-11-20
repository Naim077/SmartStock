using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("Approval", Schema = "ss")]
public class Approval
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ApprovalId { get; set; }
    public long DocumentId { get; set; }
    public int VersionNo { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Purpose { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string Decision { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string Reason { get; set; } = null!;
    public Guid ReviewedByUserId { get; set; }
    public DateTime ReviewedUtc { get; set; }
}
