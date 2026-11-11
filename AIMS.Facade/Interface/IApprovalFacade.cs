using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed Approval records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IApprovalFacade
    {
        Task<List<Approval>> GetApprovalsAsync(CancellationToken cancellationToken = default);
        Task<Approval?> GetApprovalByIdAsync(long approvalId, CancellationToken cancellationToken = default);
        Task<List<Approval>> GetApprovalsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
