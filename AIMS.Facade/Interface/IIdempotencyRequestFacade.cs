using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed IdempotencyRequest records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IIdempotencyRequestFacade
    {
        Task<List<IdempotencyRequest>> GetIdempotencyRequestsAsync(CancellationToken cancellationToken = default);
        Task<IdempotencyRequest?> GetIdempotencyRequestByIdAsync(long requestId, CancellationToken cancellationToken = default);
        Task<List<IdempotencyRequest>> GetIdempotencyRequestsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
