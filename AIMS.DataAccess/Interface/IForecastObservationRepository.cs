using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IForecastObservationRepository : IReadOnlyRepository<ForecastObservation>
    {
        Task<ForecastObservation?> GetByIdAsync(long forecastRunId, string snapshotPhase, DateOnly observationDate, CancellationToken cancellationToken = default);
    }
}
