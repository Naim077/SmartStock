using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class PaymentReversalFacade : IPaymentReversalFacade
    {
        private readonly IPaymentReversalRepository _paymentReversalRepository;

        public PaymentReversalFacade(IPaymentReversalRepository paymentReversalRepository)
        {
            _paymentReversalRepository = paymentReversalRepository;
        }

        public async Task<List<PaymentReversal>> GetPaymentReversalsAsync()
        {
            return await _paymentReversalRepository.GetAllAsync();
        }

        public async Task<PaymentReversal?> GetPaymentReversalByIdAsync(long documentId)
        {
            return await _paymentReversalRepository.GetByIdAsync(documentId);
        }

        public async Task AddPaymentReversalAsync(PaymentReversal paymentReversal)
        {
            ArgumentNullException.ThrowIfNull(paymentReversal);
            await _paymentReversalRepository.AddAsync(paymentReversal);
        }

        public async Task<bool> UpdatePaymentReversalAsync(PaymentReversal paymentReversal)
        {
            ArgumentNullException.ThrowIfNull(paymentReversal);
            var existing = await _paymentReversalRepository.GetByIdAsync(paymentReversal.DocumentId);
            if (existing == null)
                return false;

            await _paymentReversalRepository.UpdateAsync(paymentReversal);
            return true;
        }

        public async Task<bool> DeletePaymentReversalAsync(long documentId)
        {
            var existing = await _paymentReversalRepository.GetByIdAsync(documentId);
            if (existing == null)
                return false;

            await _paymentReversalRepository.DeleteAsync(documentId);
            return true;
        }
    }
}
