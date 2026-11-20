using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IBusinessSettingsRepository
    {
        Task<List<BusinessSettings>> GetAllAsync();
        Task<BusinessSettings?> GetByIdAsync(byte settingsId);
        Task AddAsync(BusinessSettings businessSettings);
        Task UpdateAsync(BusinessSettings businessSettings);
        Task DeleteAsync(byte settingsId);
    }
}
