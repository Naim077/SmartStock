using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("StockMovement", Schema = "ss")]
public class StockMovement
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long StockMovementId { get; set; }
    public long PostingId { get; set; }
    public long SourceLineId { get; set; }
    public long ProductId { get; set; }
    public long ProductBatchId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Bucket { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string MovementKind { get; set; } = null!;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal QuantityDelta { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal ValueDelta { get; set; }
    public long? ReversesMovementId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? Reason { get; set; }
}
