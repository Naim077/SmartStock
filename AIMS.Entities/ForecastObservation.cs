using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("ForecastObservation", Schema = "ss")]
[PrimaryKey(nameof(ForecastRunId), nameof(SnapshotPhase), nameof(ObservationDate))]
public class ForecastObservation
{
    public long ForecastRunId { get; set; }
    [Column(TypeName = "nvarchar(450)")]
    public string SnapshotPhase { get; set; } = null!;
    public DateTime ObservationDate { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? NetDemand { get; set; }
    public bool IsComplete { get; set; }
    public bool IsCensored { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? GapReason { get; set; }
    public DateTime KnownAsOfUtc { get; set; }
}
