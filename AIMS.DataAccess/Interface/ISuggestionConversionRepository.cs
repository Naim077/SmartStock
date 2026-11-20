using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface ISuggestionConversionRepository
    {
        Task<List<SuggestionConversion>> GetAllAsync();
        Task<SuggestionConversion?> GetByIdAsync(long conversionId);
        Task AddAsync(SuggestionConversion suggestionConversion);
        Task UpdateAsync(SuggestionConversion suggestionConversion);
        Task DeleteAsync(long conversionId);
    }
}
