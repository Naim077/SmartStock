using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("ProductStockBalance", Schema = "ss")]
public class ProductStockBalance
{
    [Key]
    public long ProductId { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Quantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal InventoryValue { get; set; }
    public long MovementVersion { get; set; }
    public long? LastPostingId { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
