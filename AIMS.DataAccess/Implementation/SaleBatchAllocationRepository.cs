using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class SaleBatchAllocationRepository : ISaleBatchAllocationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SaleBatchAllocationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SaleBatchAllocation>> GetAllAsync()
        {
            return await _dbContext.SaleBatchAllocations.ToListAsync();
        }

        public async Task<SaleBatchAllocation?> GetByIdAsync(long allocationId)
        {
            return await _dbContext.SaleBatchAllocations.FindAsync(new object[] { allocationId });
        }

        public async Task AddAsync(SaleBatchAllocation saleBatchAllocation)
        {
            await _dbContext.SaleBatchAllocations.AddAsync(saleBatchAllocation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(SaleBatchAllocation saleBatchAllocation)
        {
            _dbContext.SaleBatchAllocations.Update(saleBatchAllocation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long allocationId)
        {
            var saleBatchAllocation = await _dbContext.SaleBatchAllocations.FindAsync(new object[] { allocationId });
            if (saleBatchAllocation == null)
                return;

            _dbContext.SaleBatchAllocations.Remove(saleBatchAllocation);
            await _dbContext.SaveChangesAsync();
        }
    }
}
