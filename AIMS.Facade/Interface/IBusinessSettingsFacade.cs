using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IBusinessSettingsFacade
    {
        Task<List<BusinessSettings>> GetBusinessSettingsesAsync();
        Task<BusinessSettings?> GetBusinessSettingsByIdAsync(byte settingsId);
        Task AddBusinessSettingsAsync(BusinessSettings businessSettings);
        Task<bool> UpdateBusinessSettingsAsync(BusinessSettings businessSettings);
        Task<bool> DeleteBusinessSettingsAsync(byte settingsId);
    }
}
