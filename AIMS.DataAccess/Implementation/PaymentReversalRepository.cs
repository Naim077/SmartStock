using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class PaymentReversalRepository : ReadOnlyRepository<PaymentReversal>, IPaymentReversalRepository
    {
        public PaymentReversalRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<PaymentReversal?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { documentId }, cancellationToken);
        }
    }
}
