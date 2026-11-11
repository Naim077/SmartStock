using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface ISaleRepository : IReadOnlyRepository<Sale>
    {
        Task<Sale?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default);
    }
}
