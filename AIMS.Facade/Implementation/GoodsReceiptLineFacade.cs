using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class GoodsReceiptLineFacade : IGoodsReceiptLineFacade
    {
        private readonly IGoodsReceiptLineRepository _goodsReceiptLineRepository;

        public GoodsReceiptLineFacade(IGoodsReceiptLineRepository goodsReceiptLineRepository)
        {
            ArgumentNullException.ThrowIfNull(goodsReceiptLineRepository);
            _goodsReceiptLineRepository = goodsReceiptLineRepository;
        }

        public Task<List<GoodsReceiptLine>> GetGoodsReceiptLinesAsync(CancellationToken cancellationToken = default)
        {
            return _goodsReceiptLineRepository.GetAllAsync(cancellationToken);
        }

        public Task<GoodsReceiptLine?> GetGoodsReceiptLineByIdAsync(long lineId, CancellationToken cancellationToken = default)
        {
            return _goodsReceiptLineRepository.GetByIdAsync(lineId, cancellationToken);
        }

        public Task<List<GoodsReceiptLine>> GetGoodsReceiptLinesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _goodsReceiptLineRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
