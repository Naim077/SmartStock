using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("GoodsReceiptLine", Schema = "ss")]
public class GoodsReceiptLine
{
    [Key]
    public long LineId { get; set; }
    public long DocumentId { get; set; }
    public long ProductId { get; set; }
    public long PurchaseOrderId { get; set; }
    public long PurchaseOrderLineId { get; set; }
    public long ProductBatchId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Bucket { get; set; } = null!;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Quantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal UnitCost { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal InventoryValue { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal PayableAmount { get; set; }
}
