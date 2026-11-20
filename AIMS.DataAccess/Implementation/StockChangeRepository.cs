using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class StockChangeRepository : IStockChangeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StockChangeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<StockChange>> GetAllAsync()
        {
            return await _dbContext.StockChanges.ToListAsync();
        }

        public async Task<StockChange?> GetByIdAsync(long documentId)
        {
            return await _dbContext.StockChanges.FindAsync(new object[] { documentId });
        }

        public async Task AddAsync(StockChange stockChange)
        {
            await _dbContext.StockChanges.AddAsync(stockChange);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(StockChange stockChange)
        {
            _dbContext.StockChanges.Update(stockChange);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long documentId)
        {
            var stockChange = await _dbContext.StockChanges.FindAsync(new object[] { documentId });
            if (stockChange == null)
                return;

            _dbContext.StockChanges.Remove(stockChange);
            await _dbContext.SaveChangesAsync();
        }
    }
}
