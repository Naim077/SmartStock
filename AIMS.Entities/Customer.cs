using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("Customer", Schema = "ss")]
public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long CustomerId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Name { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string? Email { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? Phone { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? Address { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal CreditLimit { get; set; }
    public bool IsWalkIn { get; set; }
    public bool IsActive { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
