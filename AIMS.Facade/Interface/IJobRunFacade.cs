using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed JobRun records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IJobRunFacade
    {
        Task<List<JobRun>> GetJobRunsAsync(CancellationToken cancellationToken = default);
        Task<JobRun?> GetJobRunByIdAsync(long jobRunId, CancellationToken cancellationToken = default);
        Task<List<JobRun>> GetJobRunsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
