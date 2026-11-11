using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class GoodsReceiptReversalFacade : IGoodsReceiptReversalFacade
    {
        private readonly IGoodsReceiptReversalRepository _goodsReceiptReversalRepository;

        public GoodsReceiptReversalFacade(IGoodsReceiptReversalRepository goodsReceiptReversalRepository)
        {
            ArgumentNullException.ThrowIfNull(goodsReceiptReversalRepository);
            _goodsReceiptReversalRepository = goodsReceiptReversalRepository;
        }

        public Task<List<GoodsReceiptReversal>> GetGoodsReceiptReversalsAsync(CancellationToken cancellationToken = default)
        {
            return _goodsReceiptReversalRepository.GetAllAsync(cancellationToken);
        }

        public Task<GoodsReceiptReversal?> GetGoodsReceiptReversalByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return _goodsReceiptReversalRepository.GetByIdAsync(documentId, cancellationToken);
        }

        public Task<List<GoodsReceiptReversal>> GetGoodsReceiptReversalsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _goodsReceiptReversalRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
