using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class NotificationDeliveryRepository : ReadOnlyRepository<NotificationDelivery>, INotificationDeliveryRepository
    {
        public NotificationDeliveryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<NotificationDelivery?> GetByIdAsync(long deliveryId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { deliveryId }, cancellationToken);
        }
    }
}
