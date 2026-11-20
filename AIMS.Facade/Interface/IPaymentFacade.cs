using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IPaymentFacade
    {
        Task<List<Payment>> GetPaymentsAsync();
        Task<Payment?> GetPaymentByIdAsync(long documentId);
        Task AddPaymentAsync(Payment payment);
        Task<bool> UpdatePaymentAsync(Payment payment);
        Task<bool> DeletePaymentAsync(long documentId);
    }
}
