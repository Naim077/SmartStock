using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IForecastPointFacade
    {
        Task<List<ForecastPoint>> GetForecastPointsAsync();
        Task<ForecastPoint?> GetForecastPointByIdAsync(long forecastRunId, short horizonDays);
        Task AddForecastPointAsync(ForecastPoint forecastPoint);
        Task<bool> UpdateForecastPointAsync(ForecastPoint forecastPoint);
        Task<bool> DeleteForecastPointAsync(long forecastRunId, short horizonDays);
    }
}
