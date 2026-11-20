using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class SuggestionConversionFacade : ISuggestionConversionFacade
    {
        private readonly ISuggestionConversionRepository _suggestionConversionRepository;

        public SuggestionConversionFacade(ISuggestionConversionRepository suggestionConversionRepository)
        {
            _suggestionConversionRepository = suggestionConversionRepository;
        }

        public async Task<List<SuggestionConversion>> GetSuggestionConversionsAsync()
        {
            return await _suggestionConversionRepository.GetAllAsync();
        }

        public async Task<SuggestionConversion?> GetSuggestionConversionByIdAsync(long conversionId)
        {
            return await _suggestionConversionRepository.GetByIdAsync(conversionId);
        }

        public async Task AddSuggestionConversionAsync(SuggestionConversion suggestionConversion)
        {
            ArgumentNullException.ThrowIfNull(suggestionConversion);
            await _suggestionConversionRepository.AddAsync(suggestionConversion);
        }

        public async Task<bool> UpdateSuggestionConversionAsync(SuggestionConversion suggestionConversion)
        {
            ArgumentNullException.ThrowIfNull(suggestionConversion);
            var existing = await _suggestionConversionRepository.GetByIdAsync(suggestionConversion.ConversionId);
            if (existing == null)
                return false;

            await _suggestionConversionRepository.UpdateAsync(suggestionConversion);
            return true;
        }

        public async Task<bool> DeleteSuggestionConversionAsync(long conversionId)
        {
            var existing = await _suggestionConversionRepository.GetByIdAsync(conversionId);
            if (existing == null)
                return false;

            await _suggestionConversionRepository.DeleteAsync(conversionId);
            return true;
        }
    }
}
