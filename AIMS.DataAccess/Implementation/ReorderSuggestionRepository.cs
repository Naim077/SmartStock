using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class ReorderSuggestionRepository : IReorderSuggestionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ReorderSuggestionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ReorderSuggestion>> GetAllAsync()
        {
            return await _dbContext.ReorderSuggestions.ToListAsync();
        }

        public async Task<ReorderSuggestion?> GetByIdAsync(long suggestionId)
        {
            return await _dbContext.ReorderSuggestions.FindAsync(new object[] { suggestionId });
        }

        public async Task AddAsync(ReorderSuggestion reorderSuggestion)
        {
            await _dbContext.ReorderSuggestions.AddAsync(reorderSuggestion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReorderSuggestion reorderSuggestion)
        {
            _dbContext.ReorderSuggestions.Update(reorderSuggestion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long suggestionId)
        {
            var reorderSuggestion = await _dbContext.ReorderSuggestions.FindAsync(new object[] { suggestionId });
            if (reorderSuggestion == null)
                return;

            _dbContext.ReorderSuggestions.Remove(reorderSuggestion);
            await _dbContext.SaveChangesAsync();
        }
    }
}
