using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("ForecastMetric", Schema = "ss")]
[PrimaryKey(nameof(ForecastRunId), nameof(Method))]
public class ForecastMetric
{
    public long ForecastRunId { get; set; }
    [Column(TypeName = "nvarchar(450)")]
    public string Method { get; set; } = null!;
    public bool FitSucceeded { get; set; }
    public int SampleCount { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Mae { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Mape { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Wape { get; set; }
    public int MapeIncludedCount { get; set; }
    public int MapeExcludedCount { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ObservedIntervalCoverage { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? FailureCode { get; set; }
}
