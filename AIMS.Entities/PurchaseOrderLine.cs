using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("PurchaseOrderLine", Schema = "ss")]
public class PurchaseOrderLine
{
    [Key]
    public long LineId { get; set; }
    public long DocumentId { get; set; }
    public long ProductId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string ProductNameSnapshot { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string UnitSnapshot { get; set; } = null!;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal OrderedQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal UnitCost { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal NetReceivedQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal ClosedQuantity { get; set; }
}
