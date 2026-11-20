using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IForecastMetricRepository
    {
        Task<List<ForecastMetric>> GetAllAsync();
        Task<ForecastMetric?> GetByIdAsync(long forecastRunId, string method);
        Task AddAsync(ForecastMetric forecastMetric);
        Task UpdateAsync(ForecastMetric forecastMetric);
        Task DeleteAsync(long forecastRunId, string method);
    }
}
