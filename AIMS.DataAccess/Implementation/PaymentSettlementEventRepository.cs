using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class PaymentSettlementEventRepository : ReadOnlyRepository<PaymentSettlementEvent>, IPaymentSettlementEventRepository
    {
        public PaymentSettlementEventRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<PaymentSettlementEvent?> GetByIdAsync(long eventId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { eventId }, cancellationToken);
        }
    }
}
