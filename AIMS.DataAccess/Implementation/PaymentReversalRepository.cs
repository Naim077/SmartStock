using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class PaymentReversalRepository : IPaymentReversalRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentReversalRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PaymentReversal>> GetAllAsync()
        {
            return await _dbContext.PaymentReversals.ToListAsync();
        }

        public async Task<PaymentReversal?> GetByIdAsync(long documentId)
        {
            return await _dbContext.PaymentReversals.FindAsync(new object[] { documentId });
        }

        public async Task AddAsync(PaymentReversal paymentReversal)
        {
            await _dbContext.PaymentReversals.AddAsync(paymentReversal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PaymentReversal paymentReversal)
        {
            _dbContext.PaymentReversals.Update(paymentReversal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long documentId)
        {
            var paymentReversal = await _dbContext.PaymentReversals.FindAsync(new object[] { documentId });
            if (paymentReversal == null)
                return;

            _dbContext.PaymentReversals.Remove(paymentReversal);
            await _dbContext.SaveChangesAsync();
        }
    }
}
