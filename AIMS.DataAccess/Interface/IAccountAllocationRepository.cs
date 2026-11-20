using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IAccountAllocationRepository
    {
        Task<List<AccountAllocation>> GetAllAsync();
        Task<AccountAllocation?> GetByIdAsync(long allocationId);
        Task AddAsync(AccountAllocation accountAllocation);
        Task UpdateAsync(AccountAllocation accountAllocation);
        Task DeleteAsync(long allocationId);
    }
}
