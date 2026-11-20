using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("StockBalance", Schema = "ss")]
[PrimaryKey(nameof(ProductBatchId), nameof(Bucket))]
public class StockBalance
{
    public long ProductBatchId { get; set; }
    [Column(TypeName = "nvarchar(450)")]
    public string Bucket { get; set; } = null!;
    public long ProductId { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Quantity { get; set; }
    public long MovementVersion { get; set; }
    public long? LastPostingId { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
