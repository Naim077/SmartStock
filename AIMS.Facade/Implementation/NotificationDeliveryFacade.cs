using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class NotificationDeliveryFacade : INotificationDeliveryFacade
    {
        private readonly INotificationDeliveryRepository _notificationDeliveryRepository;

        public NotificationDeliveryFacade(INotificationDeliveryRepository notificationDeliveryRepository)
        {
            _notificationDeliveryRepository = notificationDeliveryRepository;
        }

        public async Task<List<NotificationDelivery>> GetNotificationDeliveriesAsync()
        {
            return await _notificationDeliveryRepository.GetAllAsync();
        }

        public async Task<NotificationDelivery?> GetNotificationDeliveryByIdAsync(long deliveryId)
        {
            return await _notificationDeliveryRepository.GetByIdAsync(deliveryId);
        }

        public async Task AddNotificationDeliveryAsync(NotificationDelivery notificationDelivery)
        {
            ArgumentNullException.ThrowIfNull(notificationDelivery);
            await _notificationDeliveryRepository.AddAsync(notificationDelivery);
        }

        public async Task<bool> UpdateNotificationDeliveryAsync(NotificationDelivery notificationDelivery)
        {
            ArgumentNullException.ThrowIfNull(notificationDelivery);
            var existing = await _notificationDeliveryRepository.GetByIdAsync(notificationDelivery.DeliveryId);
            if (existing == null)
                return false;

            await _notificationDeliveryRepository.UpdateAsync(notificationDelivery);
            return true;
        }

        public async Task<bool> DeleteNotificationDeliveryAsync(long deliveryId)
        {
            var existing = await _notificationDeliveryRepository.GetByIdAsync(deliveryId);
            if (existing == null)
                return false;

            await _notificationDeliveryRepository.DeleteAsync(deliveryId);
            return true;
        }
    }
}
