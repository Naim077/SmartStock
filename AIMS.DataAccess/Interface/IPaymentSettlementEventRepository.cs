using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IPaymentSettlementEventRepository : IReadOnlyRepository<PaymentSettlementEvent>
    {
        Task<PaymentSettlementEvent?> GetByIdAsync(long eventId, CancellationToken cancellationToken = default);
    }
}
