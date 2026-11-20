using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("Posting", Schema = "ss")]
public class Posting
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long PostingId { get; set; }
    public long DocumentId { get; set; }
    public int VersionNo { get; set; }
    public long RequestId { get; set; }
    public Guid ActorUserId { get; set; }
    public DateTime PostedUtc { get; set; }
    public DateTime BusinessDate { get; set; }
    public Guid CorrelationId { get; set; }
}
