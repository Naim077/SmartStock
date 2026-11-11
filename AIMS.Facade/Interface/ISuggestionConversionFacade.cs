using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed SuggestionConversion records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface ISuggestionConversionFacade
    {
        Task<List<SuggestionConversion>> GetSuggestionConversionsAsync(CancellationToken cancellationToken = default);
        Task<SuggestionConversion?> GetSuggestionConversionByIdAsync(long conversionId, CancellationToken cancellationToken = default);
        Task<List<SuggestionConversion>> GetSuggestionConversionsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
