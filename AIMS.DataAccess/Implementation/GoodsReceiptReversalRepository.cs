using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class GoodsReceiptReversalRepository : IGoodsReceiptReversalRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GoodsReceiptReversalRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GoodsReceiptReversal>> GetAllAsync()
        {
            return await _dbContext.GoodsReceiptReversals.ToListAsync();
        }

        public async Task<GoodsReceiptReversal?> GetByIdAsync(long documentId)
        {
            return await _dbContext.GoodsReceiptReversals.FindAsync(new object[] { documentId });
        }

        public async Task AddAsync(GoodsReceiptReversal goodsReceiptReversal)
        {
            await _dbContext.GoodsReceiptReversals.AddAsync(goodsReceiptReversal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(GoodsReceiptReversal goodsReceiptReversal)
        {
            _dbContext.GoodsReceiptReversals.Update(goodsReceiptReversal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long documentId)
        {
            var goodsReceiptReversal = await _dbContext.GoodsReceiptReversals.FindAsync(new object[] { documentId });
            if (goodsReceiptReversal == null)
                return;

            _dbContext.GoodsReceiptReversals.Remove(goodsReceiptReversal);
            await _dbContext.SaveChangesAsync();
        }
    }
}
