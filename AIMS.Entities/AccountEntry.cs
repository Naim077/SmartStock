using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("AccountEntry", Schema = "ss")]
public class AccountEntry
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long AccountEntryId { get; set; }
    public long PostingId { get; set; }
    public long? CustomerId { get; set; }
    public long? SupplierId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string EntryKind { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string Side { get; set; } = null!;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
}
