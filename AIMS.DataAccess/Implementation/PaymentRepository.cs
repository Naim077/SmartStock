using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _dbContext.Payments.ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(long documentId)
        {
            return await _dbContext.Payments.FindAsync(new object[] { documentId });
        }

        public async Task AddAsync(Payment payment)
        {
            await _dbContext.Payments.AddAsync(payment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            _dbContext.Payments.Update(payment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long documentId)
        {
            var payment = await _dbContext.Payments.FindAsync(new object[] { documentId });
            if (payment == null)
                return;

            _dbContext.Payments.Remove(payment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
