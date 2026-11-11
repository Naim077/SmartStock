using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IPostingRepository : IReadOnlyRepository<Posting>
    {
        Task<Posting?> GetByIdAsync(long postingId, CancellationToken cancellationToken = default);
    }
}
