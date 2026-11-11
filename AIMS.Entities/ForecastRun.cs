
namespace AIMS.Entities;

// Maps to ss.ForecastRun. UTC timestamps and posting rules are service responsibilities.
public sealed class ForecastRun
{
    public long ForecastRunId { get; set; }
    public long ProductId { get; set; }
    public long JobRunId { get; set; }
    public DateTime GeneratedUtc { get; set; }
    public required string Method { get; set; }
    public required string ModelVersion { get; set; }
    public required string Confidence { get; set; }
    public DateOnly? HistoryStartDate { get; set; }
    public DateOnly? HistoryEndDate { get; set; }
    public DateTime TrainingCutoffUtc { get; set; }
    public DateOnly? ValidationStartDate { get; set; }
    public DateOnly? ValidationEndDate { get; set; }
    public DateTime? ValidationCutoffUtc { get; set; }
    public DateTime RefitCutoffUtc { get; set; }
    public int CompleteDayCount { get; set; }
    public int NonzeroDayCount { get; set; }
    public int ExcludedDayCount { get; set; }
    public int CensoredDayCount { get; set; }
    public required byte[] InputHash { get; set; }
    public required string ParametersAndPostprocessingJson { get; set; }
    public string? WarningText { get; set; }

    public Product Product { get; set; } = null!;
    public JobRun JobRun { get; set; } = null!;
    public ICollection<ForecastObservation> ForecastObservations { get; set; } = new List<ForecastObservation>();
    public ICollection<ForecastMetric> ForecastMetrics { get; set; } = new List<ForecastMetric>();
    public ICollection<ForecastPoint> ForecastPoints { get; set; } = new List<ForecastPoint>();
    public ICollection<ReorderSuggestion> ReorderSuggestions { get; set; } = new List<ReorderSuggestion>();
    public ICollection<AiToolCallLog> AiToolCallLogs { get; set; } = new List<AiToolCallLog>();
}
