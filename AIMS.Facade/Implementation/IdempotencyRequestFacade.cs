using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class IdempotencyRequestFacade : IIdempotencyRequestFacade
    {
        private readonly IIdempotencyRequestRepository _idempotencyRequestRepository;

        public IdempotencyRequestFacade(IIdempotencyRequestRepository idempotencyRequestRepository)
        {
            ArgumentNullException.ThrowIfNull(idempotencyRequestRepository);
            _idempotencyRequestRepository = idempotencyRequestRepository;
        }

        public Task<List<IdempotencyRequest>> GetIdempotencyRequestsAsync(CancellationToken cancellationToken = default)
        {
            return _idempotencyRequestRepository.GetAllAsync(cancellationToken);
        }

        public Task<IdempotencyRequest?> GetIdempotencyRequestByIdAsync(long requestId, CancellationToken cancellationToken = default)
        {
            return _idempotencyRequestRepository.GetByIdAsync(requestId, cancellationToken);
        }

        public Task<List<IdempotencyRequest>> GetIdempotencyRequestsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _idempotencyRequestRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
