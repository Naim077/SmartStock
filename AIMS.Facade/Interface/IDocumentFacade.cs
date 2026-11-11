using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed Document records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IDocumentFacade
    {
        Task<List<Document>> GetDocumentsAsync(CancellationToken cancellationToken = default);
        Task<Document?> GetDocumentByIdAsync(long documentId, CancellationToken cancellationToken = default);
        Task<List<Document>> GetDocumentsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
