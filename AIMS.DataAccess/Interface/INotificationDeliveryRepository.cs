using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface INotificationDeliveryRepository : IReadOnlyRepository<NotificationDelivery>
    {
        Task<NotificationDelivery?> GetByIdAsync(long deliveryId, CancellationToken cancellationToken = default);
    }
}
