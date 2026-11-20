using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("PaymentReversal", Schema = "ss")]
public class PaymentReversal
{
    [Key]
    public long DocumentId { get; set; }
    public long OriginalPaymentId { get; set; }
    public long PostingId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Reason { get; set; } = null!;
}
