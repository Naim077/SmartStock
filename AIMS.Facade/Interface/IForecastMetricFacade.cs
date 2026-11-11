using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed ForecastMetric records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IForecastMetricFacade
    {
        Task<List<ForecastMetric>> GetForecastMetricsAsync(CancellationToken cancellationToken = default);
        Task<ForecastMetric?> GetForecastMetricByIdAsync(long forecastRunId, string method, CancellationToken cancellationToken = default);
        Task<List<ForecastMetric>> GetForecastMetricsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
