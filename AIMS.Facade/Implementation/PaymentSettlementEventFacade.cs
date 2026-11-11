using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class PaymentSettlementEventFacade : IPaymentSettlementEventFacade
    {
        private readonly IPaymentSettlementEventRepository _paymentSettlementEventRepository;

        public PaymentSettlementEventFacade(IPaymentSettlementEventRepository paymentSettlementEventRepository)
        {
            ArgumentNullException.ThrowIfNull(paymentSettlementEventRepository);
            _paymentSettlementEventRepository = paymentSettlementEventRepository;
        }

        public Task<List<PaymentSettlementEvent>> GetPaymentSettlementEventsAsync(CancellationToken cancellationToken = default)
        {
            return _paymentSettlementEventRepository.GetAllAsync(cancellationToken);
        }

        public Task<PaymentSettlementEvent?> GetPaymentSettlementEventByIdAsync(long eventId, CancellationToken cancellationToken = default)
        {
            return _paymentSettlementEventRepository.GetByIdAsync(eventId, cancellationToken);
        }

        public Task<List<PaymentSettlementEvent>> GetPaymentSettlementEventsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _paymentSettlementEventRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
