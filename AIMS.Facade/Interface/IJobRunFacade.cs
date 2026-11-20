using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IJobRunFacade
    {
        Task<List<JobRun>> GetJobRunsAsync();
        Task<JobRun?> GetJobRunByIdAsync(long jobRunId);
        Task AddJobRunAsync(JobRun jobRun);
        Task<bool> UpdateJobRunAsync(JobRun jobRun);
        Task<bool> DeleteJobRunAsync(long jobRunId);
    }
}
