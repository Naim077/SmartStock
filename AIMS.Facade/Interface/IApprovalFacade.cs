using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IApprovalFacade
    {
        Task<List<Approval>> GetApprovalsAsync();
        Task<Approval?> GetApprovalByIdAsync(long approvalId);
        Task AddApprovalAsync(Approval approval);
        Task<bool> UpdateApprovalAsync(Approval approval);
        Task<bool> DeleteApprovalAsync(long approvalId);
    }
}
