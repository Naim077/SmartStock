using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class OpeningAccountBalanceRepository : IOpeningAccountBalanceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OpeningAccountBalanceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OpeningAccountBalance>> GetAllAsync()
        {
            return await _dbContext.OpeningAccountBalances.ToListAsync();
        }

        public async Task<OpeningAccountBalance?> GetByIdAsync(long documentId)
        {
            return await _dbContext.OpeningAccountBalances.FindAsync(new object[] { documentId });
        }

        public async Task AddAsync(OpeningAccountBalance openingAccountBalance)
        {
            await _dbContext.OpeningAccountBalances.AddAsync(openingAccountBalance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(OpeningAccountBalance openingAccountBalance)
        {
            _dbContext.OpeningAccountBalances.Update(openingAccountBalance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long documentId)
        {
            var openingAccountBalance = await _dbContext.OpeningAccountBalances.FindAsync(new object[] { documentId });
            if (openingAccountBalance == null)
                return;

            _dbContext.OpeningAccountBalances.Remove(openingAccountBalance);
            await _dbContext.SaveChangesAsync();
        }
    }
}
