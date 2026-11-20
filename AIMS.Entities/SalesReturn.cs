using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("SalesReturn", Schema = "ss")]
public class SalesReturn
{
    [Key]
    public long DocumentId { get; set; }
    public long OriginalSaleId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string ReturnMode { get; set; } = null!;
    public long? PostingId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Reason { get; set; } = null!;
}
