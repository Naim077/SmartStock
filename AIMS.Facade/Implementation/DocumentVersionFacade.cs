using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class DocumentVersionFacade : IDocumentVersionFacade
    {
        private readonly IDocumentVersionRepository _documentVersionRepository;

        public DocumentVersionFacade(IDocumentVersionRepository documentVersionRepository)
        {
            ArgumentNullException.ThrowIfNull(documentVersionRepository);
            _documentVersionRepository = documentVersionRepository;
        }

        public Task<List<DocumentVersion>> GetDocumentVersionsAsync(CancellationToken cancellationToken = default)
        {
            return _documentVersionRepository.GetAllAsync(cancellationToken);
        }

        public Task<DocumentVersion?> GetDocumentVersionByIdAsync(long documentId, int versionNo, CancellationToken cancellationToken = default)
        {
            return _documentVersionRepository.GetByIdAsync(documentId, versionNo, cancellationToken);
        }

        public Task<List<DocumentVersion>> GetDocumentVersionsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _documentVersionRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
