using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface INotificationDeliveryRepository
    {
        Task<List<NotificationDelivery>> GetAllAsync();
        Task<NotificationDelivery?> GetByIdAsync(long deliveryId);
        Task AddAsync(NotificationDelivery notificationDelivery);
        Task UpdateAsync(NotificationDelivery notificationDelivery);
        Task DeleteAsync(long deliveryId);
    }
}
