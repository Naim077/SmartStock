using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class NotificationDeliveryFacade : INotificationDeliveryFacade
    {
        private readonly INotificationDeliveryRepository _notificationDeliveryRepository;

        public NotificationDeliveryFacade(INotificationDeliveryRepository notificationDeliveryRepository)
        {
            ArgumentNullException.ThrowIfNull(notificationDeliveryRepository);
            _notificationDeliveryRepository = notificationDeliveryRepository;
        }

        public Task<List<NotificationDelivery>> GetNotificationDeliveriesAsync(CancellationToken cancellationToken = default)
        {
            return _notificationDeliveryRepository.GetAllAsync(cancellationToken);
        }

        public Task<NotificationDelivery?> GetNotificationDeliveryByIdAsync(long deliveryId, CancellationToken cancellationToken = default)
        {
            return _notificationDeliveryRepository.GetByIdAsync(deliveryId, cancellationToken);
        }

        public Task<List<NotificationDelivery>> GetNotificationDeliveriesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _notificationDeliveryRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
