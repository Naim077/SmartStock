using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("Sale", Schema = "ss")]
public class Sale
{
    [Key]
    public long DocumentId { get; set; }
    public long CustomerId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string CustomerNameSnapshot { get; set; } = null!;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal InvoiceDiscountAmount { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalAmount { get; set; }
}
