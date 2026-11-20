using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("AccountAllocation", Schema = "ss")]
public class AccountAllocation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long AllocationId { get; set; }
    public long CreditEntryId { get; set; }
    public long DebitEntryId { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string AllocationKind { get; set; } = null!;
    public long? ReversesAllocationId { get; set; }
    public long RequestId { get; set; }
    public Guid ActorUserId { get; set; }
    public DateTime AllocatedUtc { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? Reason { get; set; }
}
