using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("SaleBatchAllocation", Schema = "ss")]
public class SaleBatchAllocation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long AllocationId { get; set; }
    public long SaleLineId { get; set; }
    public long SaleId { get; set; }
    public long ProductId { get; set; }
    public long ProductBatchId { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Quantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal InventoryValue { get; set; }
}
