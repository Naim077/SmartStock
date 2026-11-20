using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class ForecastPointFacade : IForecastPointFacade
    {
        private readonly IForecastPointRepository _forecastPointRepository;

        public ForecastPointFacade(IForecastPointRepository forecastPointRepository)
        {
            _forecastPointRepository = forecastPointRepository;
        }

        public async Task<List<ForecastPoint>> GetForecastPointsAsync()
        {
            return await _forecastPointRepository.GetAllAsync();
        }

        public async Task<ForecastPoint?> GetForecastPointByIdAsync(long forecastRunId, short horizonDays)
        {
            return await _forecastPointRepository.GetByIdAsync(forecastRunId, horizonDays);
        }

        public async Task AddForecastPointAsync(ForecastPoint forecastPoint)
        {
            ArgumentNullException.ThrowIfNull(forecastPoint);
            await _forecastPointRepository.AddAsync(forecastPoint);
        }

        public async Task<bool> UpdateForecastPointAsync(ForecastPoint forecastPoint)
        {
            ArgumentNullException.ThrowIfNull(forecastPoint);
            var existing = await _forecastPointRepository.GetByIdAsync(forecastPoint.ForecastRunId, forecastPoint.HorizonDays);
            if (existing == null)
                return false;

            await _forecastPointRepository.UpdateAsync(forecastPoint);
            return true;
        }

        public async Task<bool> DeleteForecastPointAsync(long forecastRunId, short horizonDays)
        {
            var existing = await _forecastPointRepository.GetByIdAsync(forecastRunId, horizonDays);
            if (existing == null)
                return false;

            await _forecastPointRepository.DeleteAsync(forecastRunId, horizonDays);
            return true;
        }
    }
}
