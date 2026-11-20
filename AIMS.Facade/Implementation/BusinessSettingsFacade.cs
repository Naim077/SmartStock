using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class BusinessSettingsFacade : IBusinessSettingsFacade
    {
        private readonly IBusinessSettingsRepository _businessSettingsRepository;

        public BusinessSettingsFacade(IBusinessSettingsRepository businessSettingsRepository)
        {
            _businessSettingsRepository = businessSettingsRepository;
        }

        public async Task<List<BusinessSettings>> GetBusinessSettingsesAsync()
        {
            return await _businessSettingsRepository.GetAllAsync();
        }

        public async Task<BusinessSettings?> GetBusinessSettingsByIdAsync(byte settingsId)
        {
            return await _businessSettingsRepository.GetByIdAsync(settingsId);
        }

        public async Task AddBusinessSettingsAsync(BusinessSettings businessSettings)
        {
            ArgumentNullException.ThrowIfNull(businessSettings);
            await _businessSettingsRepository.AddAsync(businessSettings);
        }

        public async Task<bool> UpdateBusinessSettingsAsync(BusinessSettings businessSettings)
        {
            ArgumentNullException.ThrowIfNull(businessSettings);
            var existing = await _businessSettingsRepository.GetByIdAsync(businessSettings.SettingsId);
            if (existing == null)
                return false;

            await _businessSettingsRepository.UpdateAsync(businessSettings);
            return true;
        }

        public async Task<bool> DeleteBusinessSettingsAsync(byte settingsId)
        {
            var existing = await _businessSettingsRepository.GetByIdAsync(settingsId);
            if (existing == null)
                return false;

            await _businessSettingsRepository.DeleteAsync(settingsId);
            return true;
        }
    }
}
