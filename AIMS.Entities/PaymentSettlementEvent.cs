using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("PaymentSettlementEvent", Schema = "ss")]
public class PaymentSettlementEvent
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long EventId { get; set; }
    public long PaymentId { get; set; }
    public int EventNo { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string SettlementStatus { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string? ExternalReference { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? EvidenceReference { get; set; }
    public Guid RecordedByUserId { get; set; }
    public DateTime RecordedUtc { get; set; }
}
