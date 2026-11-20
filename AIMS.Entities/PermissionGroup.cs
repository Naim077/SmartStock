using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("PermissionGroup", Schema = "dbo")]
public class PermissionGroup
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; } = null!;
    [Column(TypeName = "nvarchar(500)")]
    public string? Tag { get; set; }
}
