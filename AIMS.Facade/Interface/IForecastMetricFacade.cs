using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IForecastMetricFacade
    {
        Task<List<ForecastMetric>> GetForecastMetricsAsync();
        Task<ForecastMetric?> GetForecastMetricByIdAsync(long forecastRunId, string method);
        Task AddForecastMetricAsync(ForecastMetric forecastMetric);
        Task<bool> UpdateForecastMetricAsync(ForecastMetric forecastMetric);
        Task<bool> DeleteForecastMetricAsync(long forecastRunId, string method);
    }
}
