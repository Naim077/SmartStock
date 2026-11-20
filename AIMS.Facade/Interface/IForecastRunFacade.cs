using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IForecastRunFacade
    {
        Task<List<ForecastRun>> GetForecastRunsAsync();
        Task<ForecastRun?> GetForecastRunByIdAsync(long forecastRunId);
        Task AddForecastRunAsync(ForecastRun forecastRun);
        Task<bool> UpdateForecastRunAsync(ForecastRun forecastRun);
        Task<bool> DeleteForecastRunAsync(long forecastRunId);
    }
}
