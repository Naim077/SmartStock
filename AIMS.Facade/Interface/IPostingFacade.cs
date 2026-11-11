using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed Posting records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IPostingFacade
    {
        Task<List<Posting>> GetPostingsAsync(CancellationToken cancellationToken = default);
        Task<Posting?> GetPostingByIdAsync(long postingId, CancellationToken cancellationToken = default);
        Task<List<Posting>> GetPostingsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
