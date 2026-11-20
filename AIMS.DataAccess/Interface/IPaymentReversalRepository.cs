using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IPaymentReversalRepository
    {
        Task<List<PaymentReversal>> GetAllAsync();
        Task<PaymentReversal?> GetByIdAsync(long documentId);
        Task AddAsync(PaymentReversal paymentReversal);
        Task UpdateAsync(PaymentReversal paymentReversal);
        Task DeleteAsync(long documentId);
    }
}
