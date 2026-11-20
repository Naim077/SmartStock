using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IForecastRunRepository
    {
        Task<List<ForecastRun>> GetAllAsync();
        Task<ForecastRun?> GetByIdAsync(long forecastRunId);
        Task AddAsync(ForecastRun forecastRun);
        Task UpdateAsync(ForecastRun forecastRun);
        Task DeleteAsync(long forecastRunId);
    }
}
