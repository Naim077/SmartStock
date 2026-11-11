using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IAccountAllocationRepository : IReadOnlyRepository<AccountAllocation>
    {
        Task<AccountAllocation?> GetByIdAsync(long allocationId, CancellationToken cancellationToken = default);
    }
}
