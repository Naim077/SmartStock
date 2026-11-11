using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed ForecastObservation records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IForecastObservationFacade
    {
        Task<List<ForecastObservation>> GetForecastObservationsAsync(CancellationToken cancellationToken = default);
        Task<ForecastObservation?> GetForecastObservationByIdAsync(long forecastRunId, string snapshotPhase, DateOnly observationDate, CancellationToken cancellationToken = default);
        Task<List<ForecastObservation>> GetForecastObservationsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
