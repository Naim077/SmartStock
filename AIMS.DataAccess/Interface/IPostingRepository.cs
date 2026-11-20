using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IPostingRepository
    {
        Task<List<Posting>> GetAllAsync();
        Task<Posting?> GetByIdAsync(long postingId);
        Task AddAsync(Posting posting);
        Task UpdateAsync(Posting posting);
        Task DeleteAsync(long postingId);
    }
}
