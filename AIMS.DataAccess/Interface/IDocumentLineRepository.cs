using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IDocumentLineRepository : IReadOnlyRepository<DocumentLine>
    {
        Task<DocumentLine?> GetByIdAsync(long lineId, CancellationToken cancellationToken = default);
    }
}
