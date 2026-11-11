using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed SaleLine records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface ISaleLineFacade
    {
        Task<List<SaleLine>> GetSaleLinesAsync(CancellationToken cancellationToken = default);
        Task<SaleLine?> GetSaleLineByIdAsync(long lineId, CancellationToken cancellationToken = default);
        Task<List<SaleLine>> GetSaleLinesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
