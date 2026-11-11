using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class JobRunRepository : ReadOnlyRepository<JobRun>, IJobRunRepository
    {
        public JobRunRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<JobRun?> GetByIdAsync(long jobRunId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { jobRunId }, cancellationToken);
        }
    }
}
