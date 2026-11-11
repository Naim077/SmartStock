using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class PostingRepository : ReadOnlyRepository<Posting>, IPostingRepository
    {
        public PostingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Posting?> GetByIdAsync(long postingId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { postingId }, cancellationToken);
        }
    }
}
