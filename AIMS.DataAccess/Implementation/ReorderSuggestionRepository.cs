using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class ReorderSuggestionRepository : ReadOnlyRepository<ReorderSuggestion>, IReorderSuggestionRepository
    {
        public ReorderSuggestionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<ReorderSuggestion?> GetByIdAsync(long suggestionId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { suggestionId }, cancellationToken);
        }
    }
}
