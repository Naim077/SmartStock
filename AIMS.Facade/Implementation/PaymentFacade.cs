using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class PaymentFacade : IPaymentFacade
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentFacade(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<List<Payment>> GetPaymentsAsync()
        {
            return await _paymentRepository.GetAllAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(long documentId)
        {
            return await _paymentRepository.GetByIdAsync(documentId);
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            ArgumentNullException.ThrowIfNull(payment);
            await _paymentRepository.AddAsync(payment);
        }

        public async Task<bool> UpdatePaymentAsync(Payment payment)
        {
            ArgumentNullException.ThrowIfNull(payment);
            var existing = await _paymentRepository.GetByIdAsync(payment.DocumentId);
            if (existing == null)
                return false;

            await _paymentRepository.UpdateAsync(payment);
            return true;
        }

        public async Task<bool> DeletePaymentAsync(long documentId)
        {
            var existing = await _paymentRepository.GetByIdAsync(documentId);
            if (existing == null)
                return false;

            await _paymentRepository.DeleteAsync(documentId);
            return true;
        }
    }
}
