using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface ISaleBatchAllocationFacade
    {
        Task<List<SaleBatchAllocation>> GetSaleBatchAllocationsAsync();
        Task<SaleBatchAllocation?> GetSaleBatchAllocationByIdAsync(long allocationId);
        Task AddSaleBatchAllocationAsync(SaleBatchAllocation saleBatchAllocation);
        Task<bool> UpdateSaleBatchAllocationAsync(SaleBatchAllocation saleBatchAllocation);
        Task<bool> DeleteSaleBatchAllocationAsync(long allocationId);
    }
}
