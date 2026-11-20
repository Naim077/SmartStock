using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class ForecastObservationFacade : IForecastObservationFacade
    {
        private readonly IForecastObservationRepository _forecastObservationRepository;

        public ForecastObservationFacade(IForecastObservationRepository forecastObservationRepository)
        {
            _forecastObservationRepository = forecastObservationRepository;
        }

        public async Task<List<ForecastObservation>> GetForecastObservationsAsync()
        {
            return await _forecastObservationRepository.GetAllAsync();
        }

        public async Task<ForecastObservation?> GetForecastObservationByIdAsync(long forecastRunId, string snapshotPhase, DateTime observationDate)
        {
            return await _forecastObservationRepository.GetByIdAsync(forecastRunId, snapshotPhase, observationDate);
        }

        public async Task AddForecastObservationAsync(ForecastObservation forecastObservation)
        {
            ArgumentNullException.ThrowIfNull(forecastObservation);
            await _forecastObservationRepository.AddAsync(forecastObservation);
        }

        public async Task<bool> UpdateForecastObservationAsync(ForecastObservation forecastObservation)
        {
            ArgumentNullException.ThrowIfNull(forecastObservation);
            var existing = await _forecastObservationRepository.GetByIdAsync(forecastObservation.ForecastRunId, forecastObservation.SnapshotPhase, forecastObservation.ObservationDate);
            if (existing == null)
                return false;

            await _forecastObservationRepository.UpdateAsync(forecastObservation);
            return true;
        }

        public async Task<bool> DeleteForecastObservationAsync(long forecastRunId, string snapshotPhase, DateTime observationDate)
        {
            var existing = await _forecastObservationRepository.GetByIdAsync(forecastRunId, snapshotPhase, observationDate);
            if (existing == null)
                return false;

            await _forecastObservationRepository.DeleteAsync(forecastRunId, snapshotPhase, observationDate);
            return true;
        }
    }
}
