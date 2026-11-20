using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class IdempotencyRequestFacade : IIdempotencyRequestFacade
    {
        private readonly IIdempotencyRequestRepository _idempotencyRequestRepository;

        public IdempotencyRequestFacade(IIdempotencyRequestRepository idempotencyRequestRepository)
        {
            _idempotencyRequestRepository = idempotencyRequestRepository;
        }

        public async Task<List<IdempotencyRequest>> GetIdempotencyRequestsAsync()
        {
            return await _idempotencyRequestRepository.GetAllAsync();
        }

        public async Task<IdempotencyRequest?> GetIdempotencyRequestByIdAsync(long requestId)
        {
            return await _idempotencyRequestRepository.GetByIdAsync(requestId);
        }

        public async Task AddIdempotencyRequestAsync(IdempotencyRequest idempotencyRequest)
        {
            ArgumentNullException.ThrowIfNull(idempotencyRequest);
            await _idempotencyRequestRepository.AddAsync(idempotencyRequest);
        }

        public async Task<bool> UpdateIdempotencyRequestAsync(IdempotencyRequest idempotencyRequest)
        {
            ArgumentNullException.ThrowIfNull(idempotencyRequest);
            var existing = await _idempotencyRequestRepository.GetByIdAsync(idempotencyRequest.RequestId);
            if (existing == null)
                return false;

            await _idempotencyRequestRepository.UpdateAsync(idempotencyRequest);
            return true;
        }

        public async Task<bool> DeleteIdempotencyRequestAsync(long requestId)
        {
            var existing = await _idempotencyRequestRepository.GetByIdAsync(requestId);
            if (existing == null)
                return false;

            await _idempotencyRequestRepository.DeleteAsync(requestId);
            return true;
        }
    }
}
