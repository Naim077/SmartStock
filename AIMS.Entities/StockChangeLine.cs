using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("StockChangeLine", Schema = "ss")]
public class StockChangeLine
{
    [Key]
    public long LineId { get; set; }
    public long DocumentId { get; set; }
    public long ProductId { get; set; }
    public long ProductBatchId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Bucket { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string? ToBucket { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal QuantityDelta { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ApprovedUnitCost { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExplicitValueDelta { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExpectedQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CountedQuantity { get; set; }
    public long? CapturedProductMovementVersion { get; set; }
    public long? CapturedBucketMovementVersion { get; set; }
}
