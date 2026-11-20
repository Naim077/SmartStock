using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class OpeningAccountBalanceFacade : IOpeningAccountBalanceFacade
    {
        private readonly IOpeningAccountBalanceRepository _openingAccountBalanceRepository;

        public OpeningAccountBalanceFacade(IOpeningAccountBalanceRepository openingAccountBalanceRepository)
        {
            _openingAccountBalanceRepository = openingAccountBalanceRepository;
        }

        public async Task<List<OpeningAccountBalance>> GetOpeningAccountBalancesAsync()
        {
            return await _openingAccountBalanceRepository.GetAllAsync();
        }

        public async Task<OpeningAccountBalance?> GetOpeningAccountBalanceByIdAsync(long documentId)
        {
            return await _openingAccountBalanceRepository.GetByIdAsync(documentId);
        }

        public async Task AddOpeningAccountBalanceAsync(OpeningAccountBalance openingAccountBalance)
        {
            ArgumentNullException.ThrowIfNull(openingAccountBalance);
            await _openingAccountBalanceRepository.AddAsync(openingAccountBalance);
        }

        public async Task<bool> UpdateOpeningAccountBalanceAsync(OpeningAccountBalance openingAccountBalance)
        {
            ArgumentNullException.ThrowIfNull(openingAccountBalance);
            var existing = await _openingAccountBalanceRepository.GetByIdAsync(openingAccountBalance.DocumentId);
            if (existing == null)
                return false;

            await _openingAccountBalanceRepository.UpdateAsync(openingAccountBalance);
            return true;
        }

        public async Task<bool> DeleteOpeningAccountBalanceAsync(long documentId)
        {
            var existing = await _openingAccountBalanceRepository.GetByIdAsync(documentId);
            if (existing == null)
                return false;

            await _openingAccountBalanceRepository.DeleteAsync(documentId);
            return true;
        }
    }
}
