using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class JobRunFacade : IJobRunFacade
    {
        private readonly IJobRunRepository _jobRunRepository;

        public JobRunFacade(IJobRunRepository jobRunRepository)
        {
            _jobRunRepository = jobRunRepository;
        }

        public async Task<List<JobRun>> GetJobRunsAsync()
        {
            return await _jobRunRepository.GetAllAsync();
        }

        public async Task<JobRun?> GetJobRunByIdAsync(long jobRunId)
        {
            return await _jobRunRepository.GetByIdAsync(jobRunId);
        }

        public async Task AddJobRunAsync(JobRun jobRun)
        {
            ArgumentNullException.ThrowIfNull(jobRun);
            await _jobRunRepository.AddAsync(jobRun);
        }

        public async Task<bool> UpdateJobRunAsync(JobRun jobRun)
        {
            ArgumentNullException.ThrowIfNull(jobRun);
            var existing = await _jobRunRepository.GetByIdAsync(jobRun.JobRunId);
            if (existing == null)
                return false;

            await _jobRunRepository.UpdateAsync(jobRun);
            return true;
        }

        public async Task<bool> DeleteJobRunAsync(long jobRunId)
        {
            var existing = await _jobRunRepository.GetByIdAsync(jobRunId);
            if (existing == null)
                return false;

            await _jobRunRepository.DeleteAsync(jobRunId);
            return true;
        }
    }
}
