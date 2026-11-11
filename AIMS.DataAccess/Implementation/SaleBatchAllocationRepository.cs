using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class SaleBatchAllocationRepository : ReadOnlyRepository<SaleBatchAllocation>, ISaleBatchAllocationRepository
    {
        public SaleBatchAllocationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<SaleBatchAllocation?> GetByIdAsync(long allocationId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { allocationId }, cancellationToken);
        }
    }
}
