using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class AccountEntryRepository : IAccountEntryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountEntryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AccountEntry>> GetAllAsync()
        {
            return await _dbContext.AccountEntries.ToListAsync();
        }

        public async Task<AccountEntry?> GetByIdAsync(long accountEntryId)
        {
            return await _dbContext.AccountEntries.FindAsync(new object[] { accountEntryId });
        }

        public async Task AddAsync(AccountEntry accountEntry)
        {
            await _dbContext.AccountEntries.AddAsync(accountEntry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(AccountEntry accountEntry)
        {
            _dbContext.AccountEntries.Update(accountEntry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long accountEntryId)
        {
            var accountEntry = await _dbContext.AccountEntries.FindAsync(new object[] { accountEntryId });
            if (accountEntry == null)
                return;

            _dbContext.AccountEntries.Remove(accountEntry);
            await _dbContext.SaveChangesAsync();
        }
    }
}
