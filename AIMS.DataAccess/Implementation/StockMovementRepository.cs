using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class StockMovementRepository : IStockMovementRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StockMovementRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<StockMovement>> GetAllAsync()
        {
            return await _dbContext.StockMovements.ToListAsync();
        }

        public async Task<StockMovement?> GetByIdAsync(long stockMovementId)
        {
            return await _dbContext.StockMovements.FindAsync(new object[] { stockMovementId });
        }

        public async Task AddAsync(StockMovement stockMovement)
        {
            await _dbContext.StockMovements.AddAsync(stockMovement);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(StockMovement stockMovement)
        {
            _dbContext.StockMovements.Update(stockMovement);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long stockMovementId)
        {
            var stockMovement = await _dbContext.StockMovements.FindAsync(new object[] { stockMovementId });
            if (stockMovement == null)
                return;

            _dbContext.StockMovements.Remove(stockMovement);
            await _dbContext.SaveChangesAsync();
        }
    }
}
