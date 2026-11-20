using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("ForecastRun", Schema = "ss")]
public class ForecastRun
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ForecastRunId { get; set; }
    public long ProductId { get; set; }
    public long JobRunId { get; set; }
    public DateTime GeneratedUtc { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Method { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string ModelVersion { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string Confidence { get; set; } = null!;
    public DateTime? HistoryStartDate { get; set; }
    public DateTime? HistoryEndDate { get; set; }
    public DateTime TrainingCutoffUtc { get; set; }
    public DateTime? ValidationStartDate { get; set; }
    public DateTime? ValidationEndDate { get; set; }
    public DateTime? ValidationCutoffUtc { get; set; }
    public DateTime RefitCutoffUtc { get; set; }
    public int CompleteDayCount { get; set; }
    public int NonzeroDayCount { get; set; }
    public int ExcludedDayCount { get; set; }
    public int CensoredDayCount { get; set; }
    public byte[] InputHash { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string ParametersAndPostprocessingJson { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string? WarningText { get; set; }
}
