using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class SaleBatchAllocationFacade : ISaleBatchAllocationFacade
    {
        private readonly ISaleBatchAllocationRepository _saleBatchAllocationRepository;

        public SaleBatchAllocationFacade(ISaleBatchAllocationRepository saleBatchAllocationRepository)
        {
            ArgumentNullException.ThrowIfNull(saleBatchAllocationRepository);
            _saleBatchAllocationRepository = saleBatchAllocationRepository;
        }

        public Task<List<SaleBatchAllocation>> GetSaleBatchAllocationsAsync(CancellationToken cancellationToken = default)
        {
            return _saleBatchAllocationRepository.GetAllAsync(cancellationToken);
        }

        public Task<SaleBatchAllocation?> GetSaleBatchAllocationByIdAsync(long allocationId, CancellationToken cancellationToken = default)
        {
            return _saleBatchAllocationRepository.GetByIdAsync(allocationId, cancellationToken);
        }

        public Task<List<SaleBatchAllocation>> GetSaleBatchAllocationsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _saleBatchAllocationRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
