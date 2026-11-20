using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class SaleBatchAllocationFacade : ISaleBatchAllocationFacade
    {
        private readonly ISaleBatchAllocationRepository _saleBatchAllocationRepository;

        public SaleBatchAllocationFacade(ISaleBatchAllocationRepository saleBatchAllocationRepository)
        {
            _saleBatchAllocationRepository = saleBatchAllocationRepository;
        }

        public async Task<List<SaleBatchAllocation>> GetSaleBatchAllocationsAsync()
        {
            return await _saleBatchAllocationRepository.GetAllAsync();
        }

        public async Task<SaleBatchAllocation?> GetSaleBatchAllocationByIdAsync(long allocationId)
        {
            return await _saleBatchAllocationRepository.GetByIdAsync(allocationId);
        }

        public async Task AddSaleBatchAllocationAsync(SaleBatchAllocation saleBatchAllocation)
        {
            ArgumentNullException.ThrowIfNull(saleBatchAllocation);
            await _saleBatchAllocationRepository.AddAsync(saleBatchAllocation);
        }

        public async Task<bool> UpdateSaleBatchAllocationAsync(SaleBatchAllocation saleBatchAllocation)
        {
            ArgumentNullException.ThrowIfNull(saleBatchAllocation);
            var existing = await _saleBatchAllocationRepository.GetByIdAsync(saleBatchAllocation.AllocationId);
            if (existing == null)
                return false;

            await _saleBatchAllocationRepository.UpdateAsync(saleBatchAllocation);
            return true;
        }

        public async Task<bool> DeleteSaleBatchAllocationAsync(long allocationId)
        {
            var existing = await _saleBatchAllocationRepository.GetByIdAsync(allocationId);
            if (existing == null)
                return false;

            await _saleBatchAllocationRepository.DeleteAsync(allocationId);
            return true;
        }
    }
}
