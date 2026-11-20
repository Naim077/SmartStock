using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IForecastObservationFacade
    {
        Task<List<ForecastObservation>> GetForecastObservationsAsync();
        Task<ForecastObservation?> GetForecastObservationByIdAsync(long forecastRunId, string snapshotPhase, DateTime observationDate);
        Task AddForecastObservationAsync(ForecastObservation forecastObservation);
        Task<bool> UpdateForecastObservationAsync(ForecastObservation forecastObservation);
        Task<bool> DeleteForecastObservationAsync(long forecastRunId, string snapshotPhase, DateTime observationDate);
    }
}
