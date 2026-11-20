using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IPostingFacade
    {
        Task<List<Posting>> GetPostingsAsync();
        Task<Posting?> GetPostingByIdAsync(long postingId);
        Task AddPostingAsync(Posting posting);
        Task<bool> UpdatePostingAsync(Posting posting);
        Task<bool> DeletePostingAsync(long postingId);
    }
}
