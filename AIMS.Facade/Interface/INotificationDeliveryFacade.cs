using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed NotificationDelivery records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface INotificationDeliveryFacade
    {
        Task<List<NotificationDelivery>> GetNotificationDeliveriesAsync(CancellationToken cancellationToken = default);
        Task<NotificationDelivery?> GetNotificationDeliveryByIdAsync(long deliveryId, CancellationToken cancellationToken = default);
        Task<List<NotificationDelivery>> GetNotificationDeliveriesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
