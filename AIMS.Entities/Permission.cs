using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("Permission", Schema = "dbo")]
public class Permission
{
    [Key]
    public int Id { get; set; }
    public int? ParentId { get; set; }
    [Column(TypeName = "nvarchar(500)")]
    public string? Name { get; set; }
    [Column(TypeName = "nvarchar(500)")]
    public string? DisplayText { get; set; }
    public bool? IsActive { get; set; }
}
