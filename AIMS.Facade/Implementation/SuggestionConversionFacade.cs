using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class SuggestionConversionFacade : ISuggestionConversionFacade
    {
        private readonly ISuggestionConversionRepository _suggestionConversionRepository;

        public SuggestionConversionFacade(ISuggestionConversionRepository suggestionConversionRepository)
        {
            ArgumentNullException.ThrowIfNull(suggestionConversionRepository);
            _suggestionConversionRepository = suggestionConversionRepository;
        }

        public Task<List<SuggestionConversion>> GetSuggestionConversionsAsync(CancellationToken cancellationToken = default)
        {
            return _suggestionConversionRepository.GetAllAsync(cancellationToken);
        }

        public Task<SuggestionConversion?> GetSuggestionConversionByIdAsync(long conversionId, CancellationToken cancellationToken = default)
        {
            return _suggestionConversionRepository.GetByIdAsync(conversionId, cancellationToken);
        }

        public Task<List<SuggestionConversion>> GetSuggestionConversionsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _suggestionConversionRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
