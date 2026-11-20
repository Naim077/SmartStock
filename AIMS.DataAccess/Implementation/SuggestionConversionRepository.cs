using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class SuggestionConversionRepository : ISuggestionConversionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SuggestionConversionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SuggestionConversion>> GetAllAsync()
        {
            return await _dbContext.SuggestionConversions.ToListAsync();
        }

        public async Task<SuggestionConversion?> GetByIdAsync(long conversionId)
        {
            return await _dbContext.SuggestionConversions.FindAsync(new object[] { conversionId });
        }

        public async Task AddAsync(SuggestionConversion suggestionConversion)
        {
            await _dbContext.SuggestionConversions.AddAsync(suggestionConversion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(SuggestionConversion suggestionConversion)
        {
            _dbContext.SuggestionConversions.Update(suggestionConversion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long conversionId)
        {
            var suggestionConversion = await _dbContext.SuggestionConversions.FindAsync(new object[] { conversionId });
            if (suggestionConversion == null)
                return;

            _dbContext.SuggestionConversions.Remove(suggestionConversion);
            await _dbContext.SaveChangesAsync();
        }
    }
}
