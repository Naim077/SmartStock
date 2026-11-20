using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class AccountAllocationFacade : IAccountAllocationFacade
    {
        private readonly IAccountAllocationRepository _accountAllocationRepository;

        public AccountAllocationFacade(IAccountAllocationRepository accountAllocationRepository)
        {
            _accountAllocationRepository = accountAllocationRepository;
        }

        public async Task<List<AccountAllocation>> GetAccountAllocationsAsync()
        {
            return await _accountAllocationRepository.GetAllAsync();
        }

        public async Task<AccountAllocation?> GetAccountAllocationByIdAsync(long allocationId)
        {
            return await _accountAllocationRepository.GetByIdAsync(allocationId);
        }

        public async Task AddAccountAllocationAsync(AccountAllocation accountAllocation)
        {
            ArgumentNullException.ThrowIfNull(accountAllocation);
            await _accountAllocationRepository.AddAsync(accountAllocation);
        }

        public async Task<bool> UpdateAccountAllocationAsync(AccountAllocation accountAllocation)
        {
            ArgumentNullException.ThrowIfNull(accountAllocation);
            var existing = await _accountAllocationRepository.GetByIdAsync(accountAllocation.AllocationId);
            if (existing == null)
                return false;

            await _accountAllocationRepository.UpdateAsync(accountAllocation);
            return true;
        }

        public async Task<bool> DeleteAccountAllocationAsync(long allocationId)
        {
            var existing = await _accountAllocationRepository.GetByIdAsync(allocationId);
            if (existing == null)
                return false;

            await _accountAllocationRepository.DeleteAsync(allocationId);
            return true;
        }
    }
}
