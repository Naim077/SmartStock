using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed GoodsReceipt records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IGoodsReceiptFacade
    {
        Task<List<GoodsReceipt>> GetGoodsReceiptsAsync(CancellationToken cancellationToken = default);
        Task<GoodsReceipt?> GetGoodsReceiptByIdAsync(long documentId, CancellationToken cancellationToken = default);
        Task<List<GoodsReceipt>> GetGoodsReceiptsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
