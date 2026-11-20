using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface ISaleBatchAllocationRepository
    {
        Task<List<SaleBatchAllocation>> GetAllAsync();
        Task<SaleBatchAllocation?> GetByIdAsync(long allocationId);
        Task AddAsync(SaleBatchAllocation saleBatchAllocation);
        Task UpdateAsync(SaleBatchAllocation saleBatchAllocation);
        Task DeleteAsync(long allocationId);
    }
}
