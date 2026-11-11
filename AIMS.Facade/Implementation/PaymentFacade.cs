using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class PaymentFacade : IPaymentFacade
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentFacade(IPaymentRepository paymentRepository)
        {
            ArgumentNullException.ThrowIfNull(paymentRepository);
            _paymentRepository = paymentRepository;
        }

        public Task<List<Payment>> GetPaymentsAsync(CancellationToken cancellationToken = default)
        {
            return _paymentRepository.GetAllAsync(cancellationToken);
        }

        public Task<Payment?> GetPaymentByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return _paymentRepository.GetByIdAsync(documentId, cancellationToken);
        }

        public Task<List<Payment>> GetPaymentsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _paymentRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
