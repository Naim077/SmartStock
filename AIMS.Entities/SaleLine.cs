using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("SaleLine", Schema = "ss")]
public class SaleLine
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
    public decimal Quantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal UnitPrice { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal GrossAmount { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal ExplicitDiscountAmount { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal InvoiceDiscountAllocation { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal NetAmount { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal WacAtPosting { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal CogsValue { get; set; }
}
