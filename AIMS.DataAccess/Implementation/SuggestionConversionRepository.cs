using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class SuggestionConversionRepository : ReadOnlyRepository<SuggestionConversion>, ISuggestionConversionRepository
    {
        public SuggestionConversionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<SuggestionConversion?> GetByIdAsync(long conversionId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { conversionId }, cancellationToken);
        }
    }
}
