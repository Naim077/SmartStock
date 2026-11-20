using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("Category", Schema = "ss")]
public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long CategoryId { get; set; }
    public long? ParentCategoryId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Name { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string? NormalizedName { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
