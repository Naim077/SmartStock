using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class PostingRepository : IPostingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PostingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Posting>> GetAllAsync()
        {
            return await _dbContext.Postings.ToListAsync();
        }

        public async Task<Posting?> GetByIdAsync(long postingId)
        {
            return await _dbContext.Postings.FindAsync(new object[] { postingId });
        }

        public async Task AddAsync(Posting posting)
        {
            await _dbContext.Postings.AddAsync(posting);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Posting posting)
        {
            _dbContext.Postings.Update(posting);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long postingId)
        {
            var posting = await _dbContext.Postings.FindAsync(new object[] { postingId });
            if (posting == null)
                return;

            _dbContext.Postings.Remove(posting);
            await _dbContext.SaveChangesAsync();
        }
    }
}
