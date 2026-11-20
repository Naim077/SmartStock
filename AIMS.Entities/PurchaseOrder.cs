using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("PurchaseOrder", Schema = "ss")]
public class PurchaseOrder
{
    [Key]
    public long DocumentId { get; set; }
    public long SupplierId { get; set; }
    public DateTime? ExpectedDate { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string SupplierNameSnapshot { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string? SupplierAddressSnapshot { get; set; }
    public short PaymentTermsDaysSnapshot { get; set; }
    public DateTime? ApprovedUtc { get; set; }
    public DateTime? FinalReceiptUtc { get; set; }
    public Guid? ClosedOrCancelledByUserId { get; set; }
    public DateTime? ClosedOrCancelledUtc { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? CloseReason { get; set; }
}
