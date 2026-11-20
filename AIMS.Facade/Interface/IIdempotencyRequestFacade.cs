using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IIdempotencyRequestFacade
    {
        Task<List<IdempotencyRequest>> GetIdempotencyRequestsAsync();
        Task<IdempotencyRequest?> GetIdempotencyRequestByIdAsync(long requestId);
        Task AddIdempotencyRequestAsync(IdempotencyRequest idempotencyRequest);
        Task<bool> UpdateIdempotencyRequestAsync(IdempotencyRequest idempotencyRequest);
        Task<bool> DeleteIdempotencyRequestAsync(long requestId);
    }
}
