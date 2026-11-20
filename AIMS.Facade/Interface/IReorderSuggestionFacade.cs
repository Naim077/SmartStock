using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IReorderSuggestionFacade
    {
        Task<List<ReorderSuggestion>> GetReorderSuggestionsAsync();
        Task<ReorderSuggestion?> GetReorderSuggestionByIdAsync(long suggestionId);
        Task AddReorderSuggestionAsync(ReorderSuggestion reorderSuggestion);
        Task<bool> UpdateReorderSuggestionAsync(ReorderSuggestion reorderSuggestion);
        Task<bool> DeleteReorderSuggestionAsync(long suggestionId);
    }
}
