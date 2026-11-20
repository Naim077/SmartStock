using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class ReorderSuggestionFacade : IReorderSuggestionFacade
    {
        private readonly IReorderSuggestionRepository _reorderSuggestionRepository;

        public ReorderSuggestionFacade(IReorderSuggestionRepository reorderSuggestionRepository)
        {
            _reorderSuggestionRepository = reorderSuggestionRepository;
        }

        public async Task<List<ReorderSuggestion>> GetReorderSuggestionsAsync()
        {
            return await _reorderSuggestionRepository.GetAllAsync();
        }

        public async Task<ReorderSuggestion?> GetReorderSuggestionByIdAsync(long suggestionId)
        {
            return await _reorderSuggestionRepository.GetByIdAsync(suggestionId);
        }

        public async Task AddReorderSuggestionAsync(ReorderSuggestion reorderSuggestion)
        {
            ArgumentNullException.ThrowIfNull(reorderSuggestion);
            await _reorderSuggestionRepository.AddAsync(reorderSuggestion);
        }

        public async Task<bool> UpdateReorderSuggestionAsync(ReorderSuggestion reorderSuggestion)
        {
            ArgumentNullException.ThrowIfNull(reorderSuggestion);
            var existing = await _reorderSuggestionRepository.GetByIdAsync(reorderSuggestion.SuggestionId);
            if (existing == null)
                return false;

            await _reorderSuggestionRepository.UpdateAsync(reorderSuggestion);
            return true;
        }

        public async Task<bool> DeleteReorderSuggestionAsync(long suggestionId)
        {
            var existing = await _reorderSuggestionRepository.GetByIdAsync(suggestionId);
            if (existing == null)
                return false;

            await _reorderSuggestionRepository.DeleteAsync(suggestionId);
            return true;
        }
    }
}
