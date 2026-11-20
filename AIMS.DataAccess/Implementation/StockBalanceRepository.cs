using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class StockBalanceRepository : IStockBalanceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StockBalanceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<StockBalance>> GetAllAsync()
        {
            return await _dbContext.StockBalances.ToListAsync();
        }

        public async Task<StockBalance?> GetByIdAsync(long productBatchId, string bucket)
        {
            return await _dbContext.StockBalances.FindAsync(new object[] { productBatchId, bucket });
        }

        public async Task AddAsync(StockBalance stockBalance)
        {
            await _dbContext.StockBalances.AddAsync(stockBalance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(StockBalance stockBalance)
        {
            _dbContext.StockBalances.Update(stockBalance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long productBatchId, string bucket)
        {
            var stockBalance = await _dbContext.StockBalances.FindAsync(new object[] { productBatchId, bucket });
            if (stockBalance == null)
                return;

            _dbContext.StockBalances.Remove(stockBalance);
            await _dbContext.SaveChangesAsync();
        }
    }
}
