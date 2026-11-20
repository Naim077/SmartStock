using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class PaymentSettlementEventFacade : IPaymentSettlementEventFacade
    {
        private readonly IPaymentSettlementEventRepository _paymentSettlementEventRepository;

        public PaymentSettlementEventFacade(IPaymentSettlementEventRepository paymentSettlementEventRepository)
        {
            _paymentSettlementEventRepository = paymentSettlementEventRepository;
        }

        public async Task<List<PaymentSettlementEvent>> GetPaymentSettlementEventsAsync()
        {
            return await _paymentSettlementEventRepository.GetAllAsync();
        }

        public async Task<PaymentSettlementEvent?> GetPaymentSettlementEventByIdAsync(long eventId)
        {
            return await _paymentSettlementEventRepository.GetByIdAsync(eventId);
        }

        public async Task AddPaymentSettlementEventAsync(PaymentSettlementEvent paymentSettlementEvent)
        {
            ArgumentNullException.ThrowIfNull(paymentSettlementEvent);
            await _paymentSettlementEventRepository.AddAsync(paymentSettlementEvent);
        }

        public async Task<bool> UpdatePaymentSettlementEventAsync(PaymentSettlementEvent paymentSettlementEvent)
        {
            ArgumentNullException.ThrowIfNull(paymentSettlementEvent);
            var existing = await _paymentSettlementEventRepository.GetByIdAsync(paymentSettlementEvent.EventId);
            if (existing == null)
                return false;

            await _paymentSettlementEventRepository.UpdateAsync(paymentSettlementEvent);
            return true;
        }

        public async Task<bool> DeletePaymentSettlementEventAsync(long eventId)
        {
            var existing = await _paymentSettlementEventRepository.GetByIdAsync(eventId);
            if (existing == null)
                return false;

            await _paymentSettlementEventRepository.DeleteAsync(eventId);
            return true;
        }
    }
}
