using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class SaleFacade : ISaleFacade
    {
        private readonly ISaleRepository _saleRepository;

        public SaleFacade(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<List<Sale>> GetSalesAsync()
        {
            return await _saleRepository.GetAllAsync();
        }

        public async Task<Sale?> GetSaleByIdAsync(long documentId)
        {
            return await _saleRepository.GetByIdAsync(documentId);
        }

        public async Task AddSaleAsync(Sale sale)
        {
            ArgumentNullException.ThrowIfNull(sale);
            await _saleRepository.AddAsync(sale);
        }

        public async Task<bool> UpdateSaleAsync(Sale sale)
        {
            ArgumentNullException.ThrowIfNull(sale);
            var existing = await _saleRepository.GetByIdAsync(sale.DocumentId);
            if (existing == null)
                return false;

            await _saleRepository.UpdateAsync(sale);
            return true;
        }

        public async Task<bool> DeleteSaleAsync(long documentId)
        {
            var existing = await _saleRepository.GetByIdAsync(documentId);
            if (existing == null)
                return false;

            await _saleRepository.DeleteAsync(documentId);
            return true;
        }
    }
}
