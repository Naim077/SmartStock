using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed ForecastRun records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IForecastRunFacade
    {
        Task<List<ForecastRun>> GetForecastRunsAsync(CancellationToken cancellationToken = default);
        Task<ForecastRun?> GetForecastRunByIdAsync(long forecastRunId, CancellationToken cancellationToken = default);
        Task<List<ForecastRun>> GetForecastRunsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
