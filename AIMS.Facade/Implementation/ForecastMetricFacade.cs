using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class ForecastMetricFacade : IForecastMetricFacade
    {
        private readonly IForecastMetricRepository _forecastMetricRepository;

        public ForecastMetricFacade(IForecastMetricRepository forecastMetricRepository)
        {
            _forecastMetricRepository = forecastMetricRepository;
        }

        public async Task<List<ForecastMetric>> GetForecastMetricsAsync()
        {
            return await _forecastMetricRepository.GetAllAsync();
        }

        public async Task<ForecastMetric?> GetForecastMetricByIdAsync(long forecastRunId, string method)
        {
            return await _forecastMetricRepository.GetByIdAsync(forecastRunId, method);
        }

        public async Task AddForecastMetricAsync(ForecastMetric forecastMetric)
        {
            ArgumentNullException.ThrowIfNull(forecastMetric);
            await _forecastMetricRepository.AddAsync(forecastMetric);
        }

        public async Task<bool> UpdateForecastMetricAsync(ForecastMetric forecastMetric)
        {
            ArgumentNullException.ThrowIfNull(forecastMetric);
            var existing = await _forecastMetricRepository.GetByIdAsync(forecastMetric.ForecastRunId, forecastMetric.Method);
            if (existing == null)
                return false;

            await _forecastMetricRepository.UpdateAsync(forecastMetric);
            return true;
        }

        public async Task<bool> DeleteForecastMetricAsync(long forecastRunId, string method)
        {
            var existing = await _forecastMetricRepository.GetByIdAsync(forecastRunId, method);
            if (existing == null)
                return false;

            await _forecastMetricRepository.DeleteAsync(forecastRunId, method);
            return true;
        }
    }
}
