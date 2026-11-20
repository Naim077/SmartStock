using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class GoodsReceiptReversalFacade : IGoodsReceiptReversalFacade
    {
        private readonly IGoodsReceiptReversalRepository _goodsReceiptReversalRepository;

        public GoodsReceiptReversalFacade(IGoodsReceiptReversalRepository goodsReceiptReversalRepository)
        {
            _goodsReceiptReversalRepository = goodsReceiptReversalRepository;
        }

        public async Task<List<GoodsReceiptReversal>> GetGoodsReceiptReversalsAsync()
        {
            return await _goodsReceiptReversalRepository.GetAllAsync();
        }

        public async Task<GoodsReceiptReversal?> GetGoodsReceiptReversalByIdAsync(long documentId)
        {
            return await _goodsReceiptReversalRepository.GetByIdAsync(documentId);
        }

        public async Task AddGoodsReceiptReversalAsync(GoodsReceiptReversal goodsReceiptReversal)
        {
            ArgumentNullException.ThrowIfNull(goodsReceiptReversal);
            await _goodsReceiptReversalRepository.AddAsync(goodsReceiptReversal);
        }

        public async Task<bool> UpdateGoodsReceiptReversalAsync(GoodsReceiptReversal goodsReceiptReversal)
        {
            ArgumentNullException.ThrowIfNull(goodsReceiptReversal);
            var existing = await _goodsReceiptReversalRepository.GetByIdAsync(goodsReceiptReversal.DocumentId);
            if (existing == null)
                return false;

            await _goodsReceiptReversalRepository.UpdateAsync(goodsReceiptReversal);
            return true;
        }

        public async Task<bool> DeleteGoodsReceiptReversalAsync(long documentId)
        {
            var existing = await _goodsReceiptReversalRepository.GetByIdAsync(documentId);
            if (existing == null)
                return false;

            await _goodsReceiptReversalRepository.DeleteAsync(documentId);
            return true;
        }
    }
}
