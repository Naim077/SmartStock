using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class PaymentSettlementEventRepository : IPaymentSettlementEventRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentSettlementEventRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PaymentSettlementEvent>> GetAllAsync()
        {
            return await _dbContext.PaymentSettlementEvents.ToListAsync();
        }

        public async Task<PaymentSettlementEvent?> GetByIdAsync(long eventId)
        {
            return await _dbContext.PaymentSettlementEvents.FindAsync(new object[] { eventId });
        }

        public async Task AddAsync(PaymentSettlementEvent paymentSettlementEvent)
        {
            await _dbContext.PaymentSettlementEvents.AddAsync(paymentSettlementEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PaymentSettlementEvent paymentSettlementEvent)
        {
            _dbContext.PaymentSettlementEvents.Update(paymentSettlementEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long eventId)
        {
            var paymentSettlementEvent = await _dbContext.PaymentSettlementEvents.FindAsync(new object[] { eventId });
            if (paymentSettlementEvent == null)
                return;

            _dbContext.PaymentSettlementEvents.Remove(paymentSettlementEvent);
            await _dbContext.SaveChangesAsync();
        }
    }
}
