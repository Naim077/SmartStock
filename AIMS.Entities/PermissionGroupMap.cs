using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("PermissionGroupMap", Schema = "dbo")]
public class PermissionGroupMap
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int PermissionGroupId { get; set; }
    public int PermissionId { get; set; }
    public bool IsActive { get; set; }
}
