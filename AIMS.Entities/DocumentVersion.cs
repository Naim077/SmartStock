using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("DocumentVersion", Schema = "ss")]
[PrimaryKey(nameof(DocumentId), nameof(VersionNo))]
public class DocumentVersion
{
    public long DocumentId { get; set; }
    public int VersionNo { get; set; }
    public byte[] PayloadHash { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string SnapshotJson { get; set; } = null!;
    public Guid CapturedByUserId { get; set; }
    public DateTime CapturedUtc { get; set; }
}
