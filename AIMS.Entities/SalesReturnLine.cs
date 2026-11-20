using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("SalesReturnLine", Schema = "ss")]
public class SalesReturnLine
{
    [Key]
    public long LineId { get; set; }
    public long DocumentId { get; set; }
    public long ProductId { get; set; }
    public long OriginalSaleId { get; set; }
    public long OriginalAllocationId { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Quantity { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Disposition { get; set; } = null!;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal CreditAmount { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal RestockValue { get; set; }
}
