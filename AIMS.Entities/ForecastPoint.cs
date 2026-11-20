using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("ForecastPoint", Schema = "ss")]
[PrimaryKey(nameof(ForecastRunId), nameof(HorizonDays))]
public class ForecastPoint
{
    public long ForecastRunId { get; set; }
    public short HorizonDays { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal PredictedQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? LowerQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? UpperQuantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? NominalCoverage { get; set; }
}
