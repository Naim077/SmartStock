using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class ForecastMetricRepository : IForecastMetricRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ForecastMetricRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ForecastMetric>> GetAllAsync()
        {
            return await _dbContext.ForecastMetrics.ToListAsync();
        }

        public async Task<ForecastMetric?> GetByIdAsync(long forecastRunId, string method)
        {
            return await _dbContext.ForecastMetrics.FindAsync(new object[] { forecastRunId, method });
        }

        public async Task AddAsync(ForecastMetric forecastMetric)
        {
            await _dbContext.ForecastMetrics.AddAsync(forecastMetric);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ForecastMetric forecastMetric)
        {
            _dbContext.ForecastMetrics.Update(forecastMetric);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long forecastRunId, string method)
        {
            var forecastMetric = await _dbContext.ForecastMetrics.FindAsync(new object[] { forecastRunId, method });
            if (forecastMetric == null)
                return;

            _dbContext.ForecastMetrics.Remove(forecastMetric);
            await _dbContext.SaveChangesAsync();
        }
    }
}
