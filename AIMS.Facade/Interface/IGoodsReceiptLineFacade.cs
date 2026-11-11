using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed GoodsReceiptLine records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IGoodsReceiptLineFacade
    {
        Task<List<GoodsReceiptLine>> GetGoodsReceiptLinesAsync(CancellationToken cancellationToken = default);
        Task<GoodsReceiptLine?> GetGoodsReceiptLineByIdAsync(long lineId, CancellationToken cancellationToken = default);
        Task<List<GoodsReceiptLine>> GetGoodsReceiptLinesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
