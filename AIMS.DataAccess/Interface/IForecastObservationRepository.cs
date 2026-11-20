using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IForecastObservationRepository
    {
        Task<List<ForecastObservation>> GetAllAsync();
        Task<ForecastObservation?> GetByIdAsync(long forecastRunId, string snapshotPhase, DateTime observationDate);
        Task AddAsync(ForecastObservation forecastObservation);
        Task UpdateAsync(ForecastObservation forecastObservation);
        Task DeleteAsync(long forecastRunId, string snapshotPhase, DateTime observationDate);
    }
}
