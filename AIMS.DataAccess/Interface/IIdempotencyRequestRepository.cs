using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IIdempotencyRequestRepository : IReadOnlyRepository<IdempotencyRequest>
    {
        Task<IdempotencyRequest?> GetByIdAsync(long requestId, CancellationToken cancellationToken = default);
    }
}
