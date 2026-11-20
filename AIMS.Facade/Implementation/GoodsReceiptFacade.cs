using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class GoodsReceiptFacade : IGoodsReceiptFacade
    {
        private readonly IGoodsReceiptRepository _goodsReceiptRepository;

        public GoodsReceiptFacade(IGoodsReceiptRepository goodsReceiptRepository)
        {
            _goodsReceiptRepository = goodsReceiptRepository;
        }

        public async Task<List<GoodsReceipt>> GetGoodsReceiptsAsync()
        {
            return await _goodsReceiptRepository.GetAllAsync();
        }

        public async Task<GoodsReceipt?> GetGoodsReceiptByIdAsync(long documentId)
        {
            return await _goodsReceiptRepository.GetByIdAsync(documentId);
        }

        public async Task AddGoodsReceiptAsync(GoodsReceipt goodsReceipt)
        {
            ArgumentNullException.ThrowIfNull(goodsReceipt);
            await _goodsReceiptRepository.AddAsync(goodsReceipt);
        }

        public async Task<bool> UpdateGoodsReceiptAsync(GoodsReceipt goodsReceipt)
        {
            ArgumentNullException.ThrowIfNull(goodsReceipt);
            var existing = await _goodsReceiptRepository.GetByIdAsync(goodsReceipt.DocumentId);
            if (existing == null)
                return false;

            await _goodsReceiptRepository.UpdateAsync(goodsReceipt);
            return true;
        }

        public async Task<bool> DeleteGoodsReceiptAsync(long documentId)
        {
            var existing = await _goodsReceiptRepository.GetByIdAsync(documentId);
            if (existing == null)
                return false;

            await _goodsReceiptRepository.DeleteAsync(documentId);
            return true;
        }
    }
}
