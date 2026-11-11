
namespace AIMS.Entities;

// Maps to ss.ForecastMetric. UTC timestamps and posting rules are service responsibilities.
public sealed class ForecastMetric
{
    public long ForecastRunId { get; set; }
    public required string Method { get; set; }
    public bool FitSucceeded { get; set; }
    public int SampleCount { get; set; }
    public decimal? Mae { get; set; }
    public decimal? Mape { get; set; }
    public decimal? Wape { get; set; }
    public int MapeIncludedCount { get; set; }
    public int MapeExcludedCount { get; set; }
    public decimal? ObservedIntervalCoverage { get; set; }
    public string? FailureCode { get; set; }

    public ForecastRun ForecastRun { get; set; } = null!;
}
