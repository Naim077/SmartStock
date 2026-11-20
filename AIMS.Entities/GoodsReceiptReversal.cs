using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("GoodsReceiptReversal", Schema = "ss")]
public class GoodsReceiptReversal
{
    [Key]
    public long DocumentId { get; set; }
    public long OriginalGoodsReceiptId { get; set; }
    public long PostingId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Reason { get; set; } = null!;
}
