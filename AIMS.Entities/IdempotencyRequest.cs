using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("IdempotencyRequest", Schema = "ss")]
public class IdempotencyRequest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long RequestId { get; set; }
    public Guid ActorUserId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Operation { get; set; } = null!;
    public Guid RequestKey { get; set; }
    public byte[] PayloadHash { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string RequestStatus { get; set; } = null!;
    public DateTime CreatedUtc { get; set; }
    public DateTime? CompletedUtc { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? ResultJson { get; set; }
}
