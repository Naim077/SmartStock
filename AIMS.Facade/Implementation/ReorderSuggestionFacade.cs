using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class ReorderSuggestionFacade : IReorderSuggestionFacade
    {
        private readonly IReorderSuggestionRepository _reorderSuggestionRepository;

        public ReorderSuggestionFacade(IReorderSuggestionRepository reorderSuggestionRepository)
        {
            ArgumentNullException.ThrowIfNull(reorderSuggestionRepository);
            _reorderSuggestionRepository = reorderSuggestionRepository;
        }

        public Task<List<ReorderSuggestion>> GetReorderSuggestionsAsync(CancellationToken cancellationToken = default)
        {
            return _reorderSuggestionRepository.GetAllAsync(cancellationToken);
        }

        public Task<ReorderSuggestion?> GetReorderSuggestionByIdAsync(long suggestionId, CancellationToken cancellationToken = default)
        {
            return _reorderSuggestionRepository.GetByIdAsync(suggestionId, cancellationToken);
        }

        public Task<List<ReorderSuggestion>> GetReorderSuggestionsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _reorderSuggestionRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
