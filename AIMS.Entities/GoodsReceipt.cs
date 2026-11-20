using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("GoodsReceipt", Schema = "ss")]
public class GoodsReceipt
{
    [Key]
    public long DocumentId { get; set; }
    public long PurchaseOrderId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? SupplierDeliveryReference { get; set; }
}
