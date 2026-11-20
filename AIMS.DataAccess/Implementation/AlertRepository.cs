using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class AlertRepository : IAlertRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AlertRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Alert>> GetAllAsync()
        {
            return await _dbContext.Alerts.ToListAsync();
        }

        public async Task<Alert?> GetByIdAsync(long alertId)
        {
            return await _dbContext.Alerts.FindAsync(new object[] { alertId });
        }

        public async Task AddAsync(Alert alert)
        {
            await _dbContext.Alerts.AddAsync(alert);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Alert alert)
        {
            _dbContext.Alerts.Update(alert);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long alertId)
        {
            var alert = await _dbContext.Alerts.FindAsync(new object[] { alertId });
            if (alert == null)
                return;

            _dbContext.Alerts.Remove(alert);
            await _dbContext.SaveChangesAsync();
        }
    }
}
