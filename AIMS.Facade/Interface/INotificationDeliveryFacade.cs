using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface INotificationDeliveryFacade
    {
        Task<List<NotificationDelivery>> GetNotificationDeliveriesAsync();
        Task<NotificationDelivery?> GetNotificationDeliveryByIdAsync(long deliveryId);
        Task AddNotificationDeliveryAsync(NotificationDelivery notificationDelivery);
        Task<bool> UpdateNotificationDeliveryAsync(NotificationDelivery notificationDelivery);
        Task<bool> DeleteNotificationDeliveryAsync(long deliveryId);
    }
}
