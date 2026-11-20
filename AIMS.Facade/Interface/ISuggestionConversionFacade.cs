using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface ISuggestionConversionFacade
    {
        Task<List<SuggestionConversion>> GetSuggestionConversionsAsync();
        Task<SuggestionConversion?> GetSuggestionConversionByIdAsync(long conversionId);
        Task AddSuggestionConversionAsync(SuggestionConversion suggestionConversion);
        Task<bool> UpdateSuggestionConversionAsync(SuggestionConversion suggestionConversion);
        Task<bool> DeleteSuggestionConversionAsync(long conversionId);
    }
}
