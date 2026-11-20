using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IForecastPointRepository
    {
        Task<List<ForecastPoint>> GetAllAsync();
        Task<ForecastPoint?> GetByIdAsync(long forecastRunId, short horizonDays);
        Task AddAsync(ForecastPoint forecastPoint);
        Task UpdateAsync(ForecastPoint forecastPoint);
        Task DeleteAsync(long forecastRunId, short horizonDays);
    }
}
