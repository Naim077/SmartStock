using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IJobRunRepository
    {
        Task<List<JobRun>> GetAllAsync();
        Task<JobRun?> GetByIdAsync(long jobRunId);
        Task AddAsync(JobRun jobRun);
        Task UpdateAsync(JobRun jobRun);
        Task DeleteAsync(long jobRunId);
    }
}
