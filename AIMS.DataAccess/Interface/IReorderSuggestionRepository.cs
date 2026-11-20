using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IReorderSuggestionRepository
    {
        Task<List<ReorderSuggestion>> GetAllAsync();
        Task<ReorderSuggestion?> GetByIdAsync(long suggestionId);
        Task AddAsync(ReorderSuggestion reorderSuggestion);
        Task UpdateAsync(ReorderSuggestion reorderSuggestion);
        Task DeleteAsync(long suggestionId);
    }
}
