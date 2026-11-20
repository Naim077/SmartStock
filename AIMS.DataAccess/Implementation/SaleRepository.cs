using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SaleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            return await _dbContext.Sales.ToListAsync();
        }

        public async Task<Sale?> GetByIdAsync(long documentId)
        {
            return await _dbContext.Sales.FindAsync(new object[] { documentId });
        }

        public async Task AddAsync(Sale sale)
        {
            await _dbContext.Sales.AddAsync(sale);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sale sale)
        {
            _dbContext.Sales.Update(sale);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long documentId)
        {
            var sale = await _dbContext.Sales.FindAsync(new object[] { documentId });
            if (sale == null)
                return;

            _dbContext.Sales.Remove(sale);
            await _dbContext.SaveChangesAsync();
        }
    }
}
