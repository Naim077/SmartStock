using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class GoodsReceiptFacade : IGoodsReceiptFacade
    {
        private readonly IGoodsReceiptRepository _goodsReceiptRepository;

        public GoodsReceiptFacade(IGoodsReceiptRepository goodsReceiptRepository)
        {
            ArgumentNullException.ThrowIfNull(goodsReceiptRepository);
            _goodsReceiptRepository = goodsReceiptRepository;
        }

        public Task<List<GoodsReceipt>> GetGoodsReceiptsAsync(CancellationToken cancellationToken = default)
        {
            return _goodsReceiptRepository.GetAllAsync(cancellationToken);
        }

        public Task<GoodsReceipt?> GetGoodsReceiptByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return _goodsReceiptRepository.GetByIdAsync(documentId, cancellationToken);
        }

        public Task<List<GoodsReceipt>> GetGoodsReceiptsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _goodsReceiptRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
