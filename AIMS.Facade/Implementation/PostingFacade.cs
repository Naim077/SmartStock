using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class PostingFacade : IPostingFacade
    {
        private readonly IPostingRepository _postingRepository;

        public PostingFacade(IPostingRepository postingRepository)
        {
            ArgumentNullException.ThrowIfNull(postingRepository);
            _postingRepository = postingRepository;
        }

        public Task<List<Posting>> GetPostingsAsync(CancellationToken cancellationToken = default)
        {
            return _postingRepository.GetAllAsync(cancellationToken);
        }

        public Task<Posting?> GetPostingByIdAsync(long postingId, CancellationToken cancellationToken = default)
        {
            return _postingRepository.GetByIdAsync(postingId, cancellationToken);
        }

        public Task<List<Posting>> GetPostingsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _postingRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
