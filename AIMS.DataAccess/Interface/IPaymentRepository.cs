using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IPaymentRepository : IReadOnlyRepository<Payment>
    {
        Task<Payment?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default);
    }
}
