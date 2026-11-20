using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("Payment", Schema = "ss")]
public class Payment
{
    [Key]
    public long DocumentId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string PaymentKind { get; set; } = null!;
    public long? CustomerId { get; set; }
    public long? SupplierId { get; set; }
    public long? PurchaseOrderId { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Method { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string? ExternalReference { get; set; }
    public DateTime? ExternalEffectiveDate { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CashTenderedAmount { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CashChangeAmount { get; set; }
}
