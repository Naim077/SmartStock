using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class AccountAllocationRepository : IAccountAllocationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountAllocationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AccountAllocation>> GetAllAsync()
        {
            return await _dbContext.AccountAllocations.ToListAsync();
        }

        public async Task<AccountAllocation?> GetByIdAsync(long allocationId)
        {
            return await _dbContext.AccountAllocations.FindAsync(new object[] { allocationId });
        }

        public async Task AddAsync(AccountAllocation accountAllocation)
        {
            await _dbContext.AccountAllocations.AddAsync(accountAllocation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(AccountAllocation accountAllocation)
        {
            _dbContext.AccountAllocations.Update(accountAllocation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long allocationId)
        {
            var accountAllocation = await _dbContext.AccountAllocations.FindAsync(new object[] { allocationId });
            if (accountAllocation == null)
                return;

            _dbContext.AccountAllocations.Remove(accountAllocation);
            await _dbContext.SaveChangesAsync();
        }
    }
}
