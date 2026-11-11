using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IAccountEntryRepository : IReadOnlyRepository<AccountEntry>
    {
        Task<AccountEntry?> GetByIdAsync(long accountEntryId, CancellationToken cancellationToken = default);
    }
}
