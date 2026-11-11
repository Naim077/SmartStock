using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed GoodsReceiptReversal records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IGoodsReceiptReversalFacade
    {
        Task<List<GoodsReceiptReversal>> GetGoodsReceiptReversalsAsync(CancellationToken cancellationToken = default);
        Task<GoodsReceiptReversal?> GetGoodsReceiptReversalByIdAsync(long documentId, CancellationToken cancellationToken = default);
        Task<List<GoodsReceiptReversal>> GetGoodsReceiptReversalsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
