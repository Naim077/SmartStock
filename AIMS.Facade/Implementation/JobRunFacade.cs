using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class JobRunFacade : IJobRunFacade
    {
        private readonly IJobRunRepository _jobRunRepository;

        public JobRunFacade(IJobRunRepository jobRunRepository)
        {
            ArgumentNullException.ThrowIfNull(jobRunRepository);
            _jobRunRepository = jobRunRepository;
        }

        public Task<List<JobRun>> GetJobRunsAsync(CancellationToken cancellationToken = default)
        {
            return _jobRunRepository.GetAllAsync(cancellationToken);
        }

        public Task<JobRun?> GetJobRunByIdAsync(long jobRunId, CancellationToken cancellationToken = default)
        {
            return _jobRunRepository.GetByIdAsync(jobRunId, cancellationToken);
        }

        public Task<List<JobRun>> GetJobRunsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _jobRunRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
