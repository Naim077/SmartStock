using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class PurchaseOrderFacade : IPurchaseOrderFacade
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrderFacade(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public async Task<List<PurchaseOrder>> GetPurchaseOrdersAsync()
        {
            return await _purchaseOrderRepository.GetAllAsync();
        }

        public async Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(long documentId)
        {
            return await _purchaseOrderRepository.GetByIdAsync(documentId);
        }

        public async Task AddPurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            ArgumentNullException.ThrowIfNull(purchaseOrder);
            await _purchaseOrderRepository.AddAsync(purchaseOrder);
        }

        public async Task<bool> UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            ArgumentNullException.ThrowIfNull(purchaseOrder);
            var existing = await _purchaseOrderRepository.GetByIdAsync(purchaseOrder.DocumentId);
            if (existing == null)
                return false;

            await _purchaseOrderRepository.UpdateAsync(purchaseOrder);
            return true;
        }

        public async Task<bool> DeletePurchaseOrderAsync(long documentId)
        {
            var existing = await _purchaseOrderRepository.GetByIdAsync(documentId);
            if (existing == null)
                return false;

            await _purchaseOrderRepository.DeleteAsync(documentId);
            return true;
        }
    }
}
