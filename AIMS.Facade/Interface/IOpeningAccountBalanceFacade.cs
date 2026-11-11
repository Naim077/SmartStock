using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed OpeningAccountBalance records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IOpeningAccountBalanceFacade
    {
        Task<List<OpeningAccountBalance>> GetOpeningAccountBalancesAsync(CancellationToken cancellationToken = default);
        Task<OpeningAccountBalance?> GetOpeningAccountBalanceByIdAsync(long documentId, CancellationToken cancellationToken = default);
        Task<List<OpeningAccountBalance>> GetOpeningAccountBalancesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
