using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class BusinessSettingsRepository : IBusinessSettingsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BusinessSettingsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BusinessSettings>> GetAllAsync()
        {
            return await _dbContext.BusinessSettingses.ToListAsync();
        }

        public async Task<BusinessSettings?> GetByIdAsync(byte settingsId)
        {
            return await _dbContext.BusinessSettingses.FindAsync(new object[] { settingsId });
        }

        public async Task AddAsync(BusinessSettings businessSettings)
        {
            await _dbContext.BusinessSettingses.AddAsync(businessSettings);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(BusinessSettings businessSettings)
        {
            _dbContext.BusinessSettingses.Update(businessSettings);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(byte settingsId)
        {
            var businessSettings = await _dbContext.BusinessSettingses.FindAsync(new object[] { settingsId });
            if (businessSettings == null)
                return;

            _dbContext.BusinessSettingses.Remove(businessSettings);
            await _dbContext.SaveChangesAsync();
        }
    }
}
