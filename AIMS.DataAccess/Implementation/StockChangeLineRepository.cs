using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class StockChangeLineRepository : IStockChangeLineRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StockChangeLineRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<StockChangeLine>> GetAllAsync()
        {
            return await _dbContext.StockChangeLines.ToListAsync();
        }

        public async Task<StockChangeLine?> GetByIdAsync(long lineId)
        {
            return await _dbContext.StockChangeLines.FindAsync(new object[] { lineId });
        }

        public async Task AddAsync(StockChangeLine stockChangeLine)
        {
            await _dbContext.StockChangeLines.AddAsync(stockChangeLine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(StockChangeLine stockChangeLine)
        {
            _dbContext.StockChangeLines.Update(stockChangeLine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long lineId)
        {
            var stockChangeLine = await _dbContext.StockChangeLines.FindAsync(new object[] { lineId });
            if (stockChangeLine == null)
                return;

            _dbContext.StockChangeLines.Remove(stockChangeLine);
            await _dbContext.SaveChangesAsync();
        }
    }
}
