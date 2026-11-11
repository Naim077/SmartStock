
namespace AIMS.Entities;

// Maps to ss.ForecastPoint. UTC timestamps and posting rules are service responsibilities.
public sealed class ForecastPoint
{
    public long ForecastRunId { get; set; }
    public short HorizonDays { get; set; }
    public decimal PredictedQuantity { get; set; }
    public decimal? LowerQuantity { get; set; }
    public decimal? UpperQuantity { get; set; }
    public decimal? NominalCoverage { get; set; }

    public ForecastRun ForecastRun { get; set; } = null!;
}
