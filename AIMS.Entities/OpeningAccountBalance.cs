using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("OpeningAccountBalance", Schema = "ss")]
public class OpeningAccountBalance
{
    [Key]
    public long DocumentId { get; set; }
    public long? CustomerId { get; set; }
    public long? SupplierId { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string SourceReference { get; set; } = null!;
    public int SourceRowNumber { get; set; }
    public long? ImportBatchId { get; set; }
}
