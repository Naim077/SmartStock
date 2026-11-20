using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("SuggestionConversion", Schema = "ss")]
public class SuggestionConversion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ConversionId { get; set; }
    public long SuggestionId { get; set; }
    public Guid OpportunityKey { get; set; }
    public long PurchaseOrderLineId { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal ReviewedQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal ReviewedUnitCost { get; set; }
    public bool UncertainOrdersAcknowledged { get; set; }
    public Guid ReviewedByUserId { get; set; }
    public DateTime ConvertedUtc { get; set; }
    public long RequestId { get; set; }
    public DateTime? ReleasedUtc { get; set; }
    public Guid? ReleasedByUserId { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? ReleaseReason { get; set; }
}
