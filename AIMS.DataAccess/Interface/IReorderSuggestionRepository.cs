using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IReorderSuggestionRepository : IReadOnlyRepository<ReorderSuggestion>
    {
        Task<ReorderSuggestion?> GetByIdAsync(long suggestionId, CancellationToken cancellationToken = default);
    }
}
