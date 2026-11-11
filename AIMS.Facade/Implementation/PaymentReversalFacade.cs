using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class PaymentReversalFacade : IPaymentReversalFacade
    {
        private readonly IPaymentReversalRepository _paymentReversalRepository;

        public PaymentReversalFacade(IPaymentReversalRepository paymentReversalRepository)
        {
            ArgumentNullException.ThrowIfNull(paymentReversalRepository);
            _paymentReversalRepository = paymentReversalRepository;
        }

        public Task<List<PaymentReversal>> GetPaymentReversalsAsync(CancellationToken cancellationToken = default)
        {
            return _paymentReversalRepository.GetAllAsync(cancellationToken);
        }

        public Task<PaymentReversal?> GetPaymentReversalByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return _paymentReversalRepository.GetByIdAsync(documentId, cancellationToken);
        }

        public Task<List<PaymentReversal>> GetPaymentReversalsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _paymentReversalRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
