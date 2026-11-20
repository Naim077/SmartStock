using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IAccountAllocationFacade
    {
        Task<List<AccountAllocation>> GetAccountAllocationsAsync();
        Task<AccountAllocation?> GetAccountAllocationByIdAsync(long allocationId);
        Task AddAccountAllocationAsync(AccountAllocation accountAllocation);
        Task<bool> UpdateAccountAllocationAsync(AccountAllocation accountAllocation);
        Task<bool> DeleteAccountAllocationAsync(long allocationId);
    }
}
