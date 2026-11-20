using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IPaymentReversalFacade
    {
        Task<List<PaymentReversal>> GetPaymentReversalsAsync();
        Task<PaymentReversal?> GetPaymentReversalByIdAsync(long documentId);
        Task AddPaymentReversalAsync(PaymentReversal paymentReversal);
        Task<bool> UpdatePaymentReversalAsync(PaymentReversal paymentReversal);
        Task<bool> DeletePaymentReversalAsync(long documentId);
    }
}
