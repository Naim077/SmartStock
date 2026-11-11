using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed ForecastPoint records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IForecastPointFacade
    {
        Task<List<ForecastPoint>> GetForecastPointsAsync(CancellationToken cancellationToken = default);
        Task<ForecastPoint?> GetForecastPointByIdAsync(long forecastRunId, short horizonDays, CancellationToken cancellationToken = default);
        Task<List<ForecastPoint>> GetForecastPointsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
