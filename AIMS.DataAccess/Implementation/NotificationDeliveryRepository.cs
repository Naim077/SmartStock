using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class NotificationDeliveryRepository : INotificationDeliveryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public NotificationDeliveryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<NotificationDelivery>> GetAllAsync()
        {
            return await _dbContext.NotificationDeliveries.ToListAsync();
        }

        public async Task<NotificationDelivery?> GetByIdAsync(long deliveryId)
        {
            return await _dbContext.NotificationDeliveries.FindAsync(new object[] { deliveryId });
        }

        public async Task AddAsync(NotificationDelivery notificationDelivery)
        {
            await _dbContext.NotificationDeliveries.AddAsync(notificationDelivery);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(NotificationDelivery notificationDelivery)
        {
            _dbContext.NotificationDeliveries.Update(notificationDelivery);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long deliveryId)
        {
            var notificationDelivery = await _dbContext.NotificationDeliveries.FindAsync(new object[] { deliveryId });
            if (notificationDelivery == null)
                return;

            _dbContext.NotificationDeliveries.Remove(notificationDelivery);
            await _dbContext.SaveChangesAsync();
        }
    }
}
