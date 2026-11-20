using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("Document", Schema = "ss")]
public class Document
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long DocumentId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string DocumentKind { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string? DocumentNo { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Status { get; set; } = null!;
    public int CurrentVersionNo { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string CurrencyCode { get; set; } = null!;
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedUtc { get; set; }
    public long? CreationRequestId { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
