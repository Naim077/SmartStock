using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("UserProfile", Schema = "ss")]
public class UserProfile
{
    [Key]
    public Guid UserId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string DisplayName { get; set; } = null!;
    public bool IsActive { get; set; }
    public bool MustChangePassword { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime? DeactivatedUtc { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
