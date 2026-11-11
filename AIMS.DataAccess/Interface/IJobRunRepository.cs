using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IJobRunRepository : IReadOnlyRepository<JobRun>
    {
        Task<JobRun?> GetByIdAsync(long jobRunId, CancellationToken cancellationToken = default);
    }
}
