using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class ForecastRunFacade : IForecastRunFacade
    {
        private readonly IForecastRunRepository _forecastRunRepository;

        public ForecastRunFacade(IForecastRunRepository forecastRunRepository)
        {
            _forecastRunRepository = forecastRunRepository;
        }

        public async Task<List<ForecastRun>> GetForecastRunsAsync()
        {
            return await _forecastRunRepository.GetAllAsync();
        }

        public async Task<ForecastRun?> GetForecastRunByIdAsync(long forecastRunId)
        {
            return await _forecastRunRepository.GetByIdAsync(forecastRunId);
        }

        public async Task AddForecastRunAsync(ForecastRun forecastRun)
        {
            ArgumentNullException.ThrowIfNull(forecastRun);
            await _forecastRunRepository.AddAsync(forecastRun);
        }

        public async Task<bool> UpdateForecastRunAsync(ForecastRun forecastRun)
        {
            ArgumentNullException.ThrowIfNull(forecastRun);
            var existing = await _forecastRunRepository.GetByIdAsync(forecastRun.ForecastRunId);
            if (existing == null)
                return false;

            await _forecastRunRepository.UpdateAsync(forecastRun);
            return true;
        }

        public async Task<bool> DeleteForecastRunAsync(long forecastRunId)
        {
            var existing = await _forecastRunRepository.GetByIdAsync(forecastRunId);
            if (existing == null)
                return false;

            await _forecastRunRepository.DeleteAsync(forecastRunId);
            return true;
        }
    }
}
