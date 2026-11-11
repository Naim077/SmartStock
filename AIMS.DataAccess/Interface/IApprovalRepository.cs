using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IApprovalRepository : IReadOnlyRepository<Approval>
    {
        Task<Approval?> GetByIdAsync(long approvalId, CancellationToken cancellationToken = default);
    }
}
