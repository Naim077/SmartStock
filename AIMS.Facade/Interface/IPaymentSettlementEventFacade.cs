using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed PaymentSettlementEvent records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IPaymentSettlementEventFacade
    {
        Task<List<PaymentSettlementEvent>> GetPaymentSettlementEventsAsync(CancellationToken cancellationToken = default);
        Task<PaymentSettlementEvent?> GetPaymentSettlementEventByIdAsync(long eventId, CancellationToken cancellationToken = default);
        Task<List<PaymentSettlementEvent>> GetPaymentSettlementEventsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
