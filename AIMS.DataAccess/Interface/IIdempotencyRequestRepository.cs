using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IIdempotencyRequestRepository
    {
        Task<List<IdempotencyRequest>> GetAllAsync();
        Task<IdempotencyRequest?> GetByIdAsync(long requestId);
        Task AddAsync(IdempotencyRequest idempotencyRequest);
        Task UpdateAsync(IdempotencyRequest idempotencyRequest);
        Task DeleteAsync(long requestId);
    }
}
