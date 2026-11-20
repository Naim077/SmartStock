using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SupplierRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Supplier>> GetAllAsync()
        {
            return await _dbContext.Suppliers.ToListAsync();
        }

        public async Task<Supplier?> GetByIdAsync(long supplierId)
        {
            return await _dbContext.Suppliers.FindAsync(new object[] { supplierId });
        }

        public async Task AddAsync(Supplier supplier)
        {
            await _dbContext.Suppliers.AddAsync(supplier);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Supplier supplier)
        {
            _dbContext.Suppliers.Update(supplier);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long supplierId)
        {
            var supplier = await _dbContext.Suppliers.FindAsync(new object[] { supplierId });
            if (supplier == null)
                return;

            _dbContext.Suppliers.Remove(supplier);
            await _dbContext.SaveChangesAsync();
        }
    }
}
