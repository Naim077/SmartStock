using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("Product", Schema = "ss")]
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ProductId { get; set; }
    public long CategoryId { get; set; }
    public long? PreferredSupplierId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Sku { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string? NormalizedSku { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? Barcode { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? NormalizedBarcode { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Name { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string BaseUnit { get; set; } = null!;
    public byte QuantityScale { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? QuantityIncrement { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal SellingUnitPrice { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal DefaultPurchaseUnitCost { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string ReorderMode { get; set; } = null!;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal ManualReorderPoint { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal PackQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal MinimumOrderQuantity { get; set; }
    public bool ExpiryTracked { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? ImageAssetReference { get; set; }
    public bool IsActive { get; set; }
    public DateTime ActivatedUtc { get; set; }
    public DateTime? ArchivedUtc { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
