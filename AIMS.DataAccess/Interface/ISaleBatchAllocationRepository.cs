using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface ISaleBatchAllocationRepository : IReadOnlyRepository<SaleBatchAllocation>
    {
        Task<SaleBatchAllocation?> GetByIdAsync(long allocationId, CancellationToken cancellationToken = default);
    }
}
