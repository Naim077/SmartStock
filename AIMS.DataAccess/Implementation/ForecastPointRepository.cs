using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class ForecastPointRepository : IForecastPointRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ForecastPointRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ForecastPoint>> GetAllAsync()
        {
            return await _dbContext.ForecastPoints.ToListAsync();
        }

        public async Task<ForecastPoint?> GetByIdAsync(long forecastRunId, short horizonDays)
        {
            return await _dbContext.ForecastPoints.FindAsync(new object[] { forecastRunId, horizonDays });
        }

        public async Task AddAsync(ForecastPoint forecastPoint)
        {
            await _dbContext.ForecastPoints.AddAsync(forecastPoint);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ForecastPoint forecastPoint)
        {
            _dbContext.ForecastPoints.Update(forecastPoint);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long forecastRunId, short horizonDays)
        {
            var forecastPoint = await _dbContext.ForecastPoints.FindAsync(new object[] { forecastRunId, horizonDays });
            if (forecastPoint == null)
                return;

            _dbContext.ForecastPoints.Remove(forecastPoint);
            await _dbContext.SaveChangesAsync();
        }
    }
}
