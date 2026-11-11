using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IPaymentReversalRepository : IReadOnlyRepository<PaymentReversal>
    {
        Task<PaymentReversal?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default);
    }
}
