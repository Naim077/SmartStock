using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IPaymentSettlementEventRepository
    {
        Task<List<PaymentSettlementEvent>> GetAllAsync();
        Task<PaymentSettlementEvent?> GetByIdAsync(long eventId);
        Task AddAsync(PaymentSettlementEvent paymentSettlementEvent);
        Task UpdateAsync(PaymentSettlementEvent paymentSettlementEvent);
        Task DeleteAsync(long eventId);
    }
}
