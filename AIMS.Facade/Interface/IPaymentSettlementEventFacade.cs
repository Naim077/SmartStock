using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IPaymentSettlementEventFacade
    {
        Task<List<PaymentSettlementEvent>> GetPaymentSettlementEventsAsync();
        Task<PaymentSettlementEvent?> GetPaymentSettlementEventByIdAsync(long eventId);
        Task AddPaymentSettlementEventAsync(PaymentSettlementEvent paymentSettlementEvent);
        Task<bool> UpdatePaymentSettlementEventAsync(PaymentSettlementEvent paymentSettlementEvent);
        Task<bool> DeletePaymentSettlementEventAsync(long eventId);
    }
}
