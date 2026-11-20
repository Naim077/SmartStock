using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("Supplier", Schema = "ss")]
public class Supplier
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long SupplierId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Name { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string? ContactName { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? Email { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? Phone { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? Address { get; set; }
    public short PaymentTermsDays { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ConfiguredLeadTimeDays { get; set; }
    public bool IsActive { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
