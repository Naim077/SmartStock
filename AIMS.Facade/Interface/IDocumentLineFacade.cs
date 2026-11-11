using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed DocumentLine records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IDocumentLineFacade
    {
        Task<List<DocumentLine>> GetDocumentLinesAsync(CancellationToken cancellationToken = default);
        Task<DocumentLine?> GetDocumentLineByIdAsync(long lineId, CancellationToken cancellationToken = default);
        Task<List<DocumentLine>> GetDocumentLinesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
