using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IOpeningAccountBalanceRepository : IReadOnlyRepository<OpeningAccountBalance>
    {
        Task<OpeningAccountBalance?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default);
    }
}
