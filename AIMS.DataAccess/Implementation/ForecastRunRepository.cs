using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class ForecastRunRepository : IForecastRunRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ForecastRunRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ForecastRun>> GetAllAsync()
        {
            return await _dbContext.ForecastRuns.ToListAsync();
        }

        public async Task<ForecastRun?> GetByIdAsync(long forecastRunId)
        {
            return await _dbContext.ForecastRuns.FindAsync(new object[] { forecastRunId });
        }

        public async Task AddAsync(ForecastRun forecastRun)
        {
            await _dbContext.ForecastRuns.AddAsync(forecastRun);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ForecastRun forecastRun)
        {
            _dbContext.ForecastRuns.Update(forecastRun);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long forecastRunId)
        {
            var forecastRun = await _dbContext.ForecastRuns.FindAsync(new object[] { forecastRunId });
            if (forecastRun == null)
                return;

            _dbContext.ForecastRuns.Remove(forecastRun);
            await _dbContext.SaveChangesAsync();
        }
    }
}
