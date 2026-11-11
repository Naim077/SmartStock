using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed AccountAllocation records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IAccountAllocationFacade
    {
        Task<List<AccountAllocation>> GetAccountAllocationsAsync(CancellationToken cancellationToken = default);
        Task<AccountAllocation?> GetAccountAllocationByIdAsync(long allocationId, CancellationToken cancellationToken = default);
        Task<List<AccountAllocation>> GetAccountAllocationsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
