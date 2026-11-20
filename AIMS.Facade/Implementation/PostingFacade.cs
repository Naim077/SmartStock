using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class PostingFacade : IPostingFacade
    {
        private readonly IPostingRepository _postingRepository;

        public PostingFacade(IPostingRepository postingRepository)
        {
            _postingRepository = postingRepository;
        }

        public async Task<List<Posting>> GetPostingsAsync()
        {
            return await _postingRepository.GetAllAsync();
        }

        public async Task<Posting?> GetPostingByIdAsync(long postingId)
        {
            return await _postingRepository.GetByIdAsync(postingId);
        }

        public async Task AddPostingAsync(Posting posting)
        {
            ArgumentNullException.ThrowIfNull(posting);
            await _postingRepository.AddAsync(posting);
        }

        public async Task<bool> UpdatePostingAsync(Posting posting)
        {
            ArgumentNullException.ThrowIfNull(posting);
            var existing = await _postingRepository.GetByIdAsync(posting.PostingId);
            if (existing == null)
                return false;

            await _postingRepository.UpdateAsync(posting);
            return true;
        }

        public async Task<bool> DeletePostingAsync(long postingId)
        {
            var existing = await _postingRepository.GetByIdAsync(postingId);
            if (existing == null)
                return false;

            await _postingRepository.DeleteAsync(postingId);
            return true;
        }
    }
}
