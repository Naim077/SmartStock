using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class IdempotencyRequestRepository : IIdempotencyRequestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public IdempotencyRequestRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<IdempotencyRequest>> GetAllAsync()
        {
            return await _dbContext.IdempotencyRequests.ToListAsync();
        }

        public async Task<IdempotencyRequest?> GetByIdAsync(long requestId)
        {
            return await _dbContext.IdempotencyRequests.FindAsync(new object[] { requestId });
        }

        public async Task AddAsync(IdempotencyRequest idempotencyRequest)
        {
            await _dbContext.IdempotencyRequests.AddAsync(idempotencyRequest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(IdempotencyRequest idempotencyRequest)
        {
            _dbContext.IdempotencyRequests.Update(idempotencyRequest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long requestId)
        {
            var idempotencyRequest = await _dbContext.IdempotencyRequests.FindAsync(new object[] { requestId });
            if (idempotencyRequest == null)
                return;

            _dbContext.IdempotencyRequests.Remove(idempotencyRequest);
            await _dbContext.SaveChangesAsync();
        }
    }
}
