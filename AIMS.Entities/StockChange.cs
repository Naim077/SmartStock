using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("StockChange", Schema = "ss")]
public class StockChange
{
    [Key]
    public long DocumentId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string ChangeKind { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string Reason { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string? EvidenceReference { get; set; }
}
