using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed DocumentVersion records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IDocumentVersionFacade
    {
        Task<List<DocumentVersion>> GetDocumentVersionsAsync(CancellationToken cancellationToken = default);
        Task<DocumentVersion?> GetDocumentVersionByIdAsync(long documentId, int versionNo, CancellationToken cancellationToken = default);
        Task<List<DocumentVersion>> GetDocumentVersionsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
