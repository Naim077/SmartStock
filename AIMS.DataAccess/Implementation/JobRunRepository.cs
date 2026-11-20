using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class JobRunRepository : IJobRunRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public JobRunRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<JobRun>> GetAllAsync()
        {
            return await _dbContext.JobRuns.ToListAsync();
        }

        public async Task<JobRun?> GetByIdAsync(long jobRunId)
        {
            return await _dbContext.JobRuns.FindAsync(new object[] { jobRunId });
        }

        public async Task AddAsync(JobRun jobRun)
        {
            await _dbContext.JobRuns.AddAsync(jobRun);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobRun jobRun)
        {
            _dbContext.JobRuns.Update(jobRun);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long jobRunId)
        {
            var jobRun = await _dbContext.JobRuns.FindAsync(new object[] { jobRunId });
            if (jobRun == null)
                return;

            _dbContext.JobRuns.Remove(jobRun);
            await _dbContext.SaveChangesAsync();
        }
    }
}
