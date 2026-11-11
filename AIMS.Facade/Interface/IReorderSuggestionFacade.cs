using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed ReorderSuggestion records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IReorderSuggestionFacade
    {
        Task<List<ReorderSuggestion>> GetReorderSuggestionsAsync(CancellationToken cancellationToken = default);
        Task<ReorderSuggestion?> GetReorderSuggestionByIdAsync(long suggestionId, CancellationToken cancellationToken = default);
        Task<List<ReorderSuggestion>> GetReorderSuggestionsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
