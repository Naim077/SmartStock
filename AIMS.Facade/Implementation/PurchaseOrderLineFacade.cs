using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class PurchaseOrderLineFacade : IPurchaseOrderLineFacade
    {
        private readonly IPurchaseOrderLineRepository _purchaseOrderLineRepository;

        public PurchaseOrderLineFacade(IPurchaseOrderLineRepository purchaseOrderLineRepository)
        {
            _purchaseOrderLineRepository = purchaseOrderLineRepository;
        }

        public async Task<List<PurchaseOrderLine>> GetPurchaseOrderLinesAsync()
        {
            return await _purchaseOrderLineRepository.GetAllAsync();
        }

        public async Task<PurchaseOrderLine?> GetPurchaseOrderLineByIdAsync(long lineId)
        {
            return await _purchaseOrderLineRepository.GetByIdAsync(lineId);
        }

        public async Task AddPurchaseOrderLineAsync(PurchaseOrderLine purchaseOrderLine)
        {
            ArgumentNullException.ThrowIfNull(purchaseOrderLine);
            await _purchaseOrderLineRepository.AddAsync(purchaseOrderLine);
        }

        public async Task<bool> UpdatePurchaseOrderLineAsync(PurchaseOrderLine purchaseOrderLine)
        {
            ArgumentNullException.ThrowIfNull(purchaseOrderLine);
            var existing = await _purchaseOrderLineRepository.GetByIdAsync(purchaseOrderLine.LineId);
            if (existing == null)
                return false;

            await _purchaseOrderLineRepository.UpdateAsync(purchaseOrderLine);
            return true;
        }

        public async Task<bool> DeletePurchaseOrderLineAsync(long lineId)
        {
            var existing = await _purchaseOrderLineRepository.GetByIdAsync(lineId);
            if (existing == null)
                return false;

            await _purchaseOrderLineRepository.DeleteAsync(lineId);
            return true;
        }
    }
}
