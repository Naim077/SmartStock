using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed SaleBatchAllocation records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface ISaleBatchAllocationFacade
    {
        Task<List<SaleBatchAllocation>> GetSaleBatchAllocationsAsync(CancellationToken cancellationToken = default);
        Task<SaleBatchAllocation?> GetSaleBatchAllocationByIdAsync(long allocationId, CancellationToken cancellationToken = default);
        Task<List<SaleBatchAllocation>> GetSaleBatchAllocationsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
