using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed AccountEntry records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IAccountEntryFacade
    {
        Task<List<AccountEntry>> GetAccountEntriesAsync(CancellationToken cancellationToken = default);
        Task<AccountEntry?> GetAccountEntryByIdAsync(long accountEntryId, CancellationToken cancellationToken = default);
        Task<List<AccountEntry>> GetAccountEntriesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
