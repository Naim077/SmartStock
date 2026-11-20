using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class ForecastObservationRepository : IForecastObservationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ForecastObservationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ForecastObservation>> GetAllAsync()
        {
            return await _dbContext.ForecastObservations.ToListAsync();
        }

        public async Task<ForecastObservation?> GetByIdAsync(long forecastRunId, string snapshotPhase, DateTime observationDate)
        {
            return await _dbContext.ForecastObservations.FindAsync(new object[] { forecastRunId, snapshotPhase, observationDate });
        }

        public async Task AddAsync(ForecastObservation forecastObservation)
        {
            await _dbContext.ForecastObservations.AddAsync(forecastObservation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ForecastObservation forecastObservation)
        {
            _dbContext.ForecastObservations.Update(forecastObservation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long forecastRunId, string snapshotPhase, DateTime observationDate)
        {
            var forecastObservation = await _dbContext.ForecastObservations.FindAsync(new object[] { forecastRunId, snapshotPhase, observationDate });
            if (forecastObservation == null)
                return;

            _dbContext.ForecastObservations.Remove(forecastObservation);
            await _dbContext.SaveChangesAsync();
        }
    }
}
