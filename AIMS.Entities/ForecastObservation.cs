
namespace AIMS.Entities;

// Maps to ss.ForecastObservation. UTC timestamps and posting rules are service responsibilities.
public sealed class ForecastObservation
{
    public long ForecastRunId { get; set; }
    public required string SnapshotPhase { get; set; }
    public DateOnly ObservationDate { get; set; }
    public decimal? NetDemand { get; set; }
    public bool IsComplete { get; set; }
    public bool IsCensored { get; set; }
    public string? GapReason { get; set; }
    public DateTime KnownAsOfUtc { get; set; }

    public ForecastRun ForecastRun { get; set; } = null!;
}
