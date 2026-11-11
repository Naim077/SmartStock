using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class DocumentLineFacade : IDocumentLineFacade
    {
        private readonly IDocumentLineRepository _documentLineRepository;

        public DocumentLineFacade(IDocumentLineRepository documentLineRepository)
        {
            ArgumentNullException.ThrowIfNull(documentLineRepository);
            _documentLineRepository = documentLineRepository;
        }

        public Task<List<DocumentLine>> GetDocumentLinesAsync(CancellationToken cancellationToken = default)
        {
            return _documentLineRepository.GetAllAsync(cancellationToken);
        }

        public Task<DocumentLine?> GetDocumentLineByIdAsync(long lineId, CancellationToken cancellationToken = default)
        {
            return _documentLineRepository.GetByIdAsync(lineId, cancellationToken);
        }

        public Task<List<DocumentLine>> GetDocumentLinesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _documentLineRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
