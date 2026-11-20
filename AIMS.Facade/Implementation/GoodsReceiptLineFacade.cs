using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class GoodsReceiptLineFacade : IGoodsReceiptLineFacade
    {
        private readonly IGoodsReceiptLineRepository _goodsReceiptLineRepository;

        public GoodsReceiptLineFacade(IGoodsReceiptLineRepository goodsReceiptLineRepository)
        {
            _goodsReceiptLineRepository = goodsReceiptLineRepository;
        }

        public async Task<List<GoodsReceiptLine>> GetGoodsReceiptLinesAsync()
        {
            return await _goodsReceiptLineRepository.GetAllAsync();
        }

        public async Task<GoodsReceiptLine?> GetGoodsReceiptLineByIdAsync(long lineId)
        {
            return await _goodsReceiptLineRepository.GetByIdAsync(lineId);
        }

        public async Task AddGoodsReceiptLineAsync(GoodsReceiptLine goodsReceiptLine)
        {
            ArgumentNullException.ThrowIfNull(goodsReceiptLine);
            await _goodsReceiptLineRepository.AddAsync(goodsReceiptLine);
        }

        public async Task<bool> UpdateGoodsReceiptLineAsync(GoodsReceiptLine goodsReceiptLine)
        {
            ArgumentNullException.ThrowIfNull(goodsReceiptLine);
            var existing = await _goodsReceiptLineRepository.GetByIdAsync(goodsReceiptLine.LineId);
            if (existing == null)
                return false;

            await _goodsReceiptLineRepository.UpdateAsync(goodsReceiptLine);
            return true;
        }

        public async Task<bool> DeleteGoodsReceiptLineAsync(long lineId)
        {
            var existing = await _goodsReceiptLineRepository.GetByIdAsync(lineId);
            if (existing == null)
                return false;

            await _goodsReceiptLineRepository.DeleteAsync(lineId);
            return true;
        }
    }
}
