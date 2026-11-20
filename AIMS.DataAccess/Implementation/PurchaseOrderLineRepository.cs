using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class PurchaseOrderLineRepository : IPurchaseOrderLineRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PurchaseOrderLineRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PurchaseOrderLine>> GetAllAsync()
        {
            return await _dbContext.PurchaseOrderLines.ToListAsync();
        }

        public async Task<PurchaseOrderLine?> GetByIdAsync(long lineId)
        {
            return await _dbContext.PurchaseOrderLines.FindAsync(new object[] { lineId });
        }

        public async Task AddAsync(PurchaseOrderLine purchaseOrderLine)
        {
            await _dbContext.PurchaseOrderLines.AddAsync(purchaseOrderLine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PurchaseOrderLine purchaseOrderLine)
        {
            _dbContext.PurchaseOrderLines.Update(purchaseOrderLine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long lineId)
        {
            var purchaseOrderLine = await _dbContext.PurchaseOrderLines.FindAsync(new object[] { lineId });
            if (purchaseOrderLine == null)
                return;

            _dbContext.PurchaseOrderLines.Remove(purchaseOrderLine);
            await _dbContext.SaveChangesAsync();
        }
    }
}
