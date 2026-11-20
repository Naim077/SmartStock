using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("ProductBatch", Schema = "ss")]
public class ProductBatch
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ProductBatchId { get; set; }
    public long ProductId { get; set; }
    public bool IsUnbatched { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? BatchNumber { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? NormalizedBatchNumber { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public DateTime? FirstReceivedUtc { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
