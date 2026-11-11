using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class DocumentFacade : IDocumentFacade
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentFacade(IDocumentRepository documentRepository)
        {
            ArgumentNullException.ThrowIfNull(documentRepository);
            _documentRepository = documentRepository;
        }

        public Task<List<Document>> GetDocumentsAsync(CancellationToken cancellationToken = default)
        {
            return _documentRepository.GetAllAsync(cancellationToken);
        }

        public Task<Document?> GetDocumentByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return _documentRepository.GetByIdAsync(documentId, cancellationToken);
        }

        public Task<List<Document>> GetDocumentsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _documentRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
