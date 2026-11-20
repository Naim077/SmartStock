using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PurchaseOrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PurchaseOrder>> GetAllAsync()
        {
            return await _dbContext.PurchaseOrders.ToListAsync();
        }

        public async Task<PurchaseOrder?> GetByIdAsync(long documentId)
        {
            return await _dbContext.PurchaseOrders.FindAsync(new object[] { documentId });
        }

        public async Task AddAsync(PurchaseOrder purchaseOrder)
        {
            await _dbContext.PurchaseOrders.AddAsync(purchaseOrder);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PurchaseOrder purchaseOrder)
        {
            _dbContext.PurchaseOrders.Update(purchaseOrder);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long documentId)
        {
            var purchaseOrder = await _dbContext.PurchaseOrders.FindAsync(new object[] { documentId });
            if (purchaseOrder == null)
                return;

            _dbContext.PurchaseOrders.Remove(purchaseOrder);
            await _dbContext.SaveChangesAsync();
        }
    }
}
