using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class AlertFacade : IAlertFacade
    {
        private readonly IAlertRepository _alertRepository;

        public AlertFacade(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        public async Task<List<Alert>> GetAlertsAsync()
        {
            return await _alertRepository.GetAllAsync();
        }

        public async Task<Alert?> GetAlertByIdAsync(long alertId)
        {
            return await _alertRepository.GetByIdAsync(alertId);
        }

        public async Task AddAlertAsync(Alert alert)
        {
            ArgumentNullException.ThrowIfNull(alert);
            await _alertRepository.AddAsync(alert);
        }

        public async Task<bool> UpdateAlertAsync(Alert alert)
        {
            ArgumentNullException.ThrowIfNull(alert);
            var existing = await _alertRepository.GetByIdAsync(alert.AlertId);
            if (existing == null)
                return false;

            await _alertRepository.UpdateAsync(alert);
            return true;
        }

        public async Task<bool> DeleteAlertAsync(long alertId)
        {
            var existing = await _alertRepository.GetByIdAsync(alertId);
            if (existing == null)
                return false;

            await _alertRepository.DeleteAsync(alertId);
            return true;
        }
    }
}
