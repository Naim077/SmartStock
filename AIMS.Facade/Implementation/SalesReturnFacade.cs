using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class SalesReturnFacade : ISalesReturnFacade
    {
        private readonly ISalesReturnRepository _salesReturnRepository;

        public SalesReturnFacade(ISalesReturnRepository salesReturnRepository)
        {
            _salesReturnRepository = salesReturnRepository;
        }

        public async Task<List<SalesReturn>> GetSalesReturnsAsync()
        {
            return await _salesReturnRepository.GetAllAsync();
        }

        public async Task<SalesReturn?> GetSalesReturnByIdAsync(long documentId)
        {
            return await _salesReturnRepository.GetByIdAsync(documentId);
        }

        public async Task AddSalesReturnAsync(SalesReturn salesReturn)
        {
            ArgumentNullException.ThrowIfNull(salesReturn);
            await _salesReturnRepository.AddAsync(salesReturn);
        }

        public async Task<bool> UpdateSalesReturnAsync(SalesReturn salesReturn)
        {
            ArgumentNullException.ThrowIfNull(salesReturn);
            var existing = await _salesReturnRepository.GetByIdAsync(salesReturn.DocumentId);
            if (existing == null)
                return false;

            await _salesReturnRepository.UpdateAsync(salesReturn);
            return true;
        }

        public async Task<bool> DeleteSalesReturnAsync(long documentId)
        {
            var existing = await _salesReturnRepository.GetByIdAsync(documentId);
            if (existing == null)
                return false;

            await _salesReturnRepository.DeleteAsync(documentId);
            return true;
        }
    }
}
