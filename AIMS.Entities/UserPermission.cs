using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("UserPermission", Schema = "dbo")]
public class UserPermission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int? PermissionId { get; set; }
    public int? PermissionGroupId { get; set; }
}
