using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class ApprovalFacade : IApprovalFacade
    {
        private readonly IApprovalRepository _approvalRepository;

        public ApprovalFacade(IApprovalRepository approvalRepository)
        {
            _approvalRepository = approvalRepository;
        }

        public async Task<List<Approval>> GetApprovalsAsync()
        {
            return await _approvalRepository.GetAllAsync();
        }

        public async Task<Approval?> GetApprovalByIdAsync(long approvalId)
        {
            return await _approvalRepository.GetByIdAsync(approvalId);
        }

        public async Task AddApprovalAsync(Approval approval)
        {
            ArgumentNullException.ThrowIfNull(approval);
            await _approvalRepository.AddAsync(approval);
        }

        public async Task<bool> UpdateApprovalAsync(Approval approval)
        {
            ArgumentNullException.ThrowIfNull(approval);
            var existing = await _approvalRepository.GetByIdAsync(approval.ApprovalId);
            if (existing == null)
                return false;

            await _approvalRepository.UpdateAsync(approval);
            return true;
        }

        public async Task<bool> DeleteApprovalAsync(long approvalId)
        {
            var existing = await _approvalRepository.GetByIdAsync(approvalId);
            if (existing == null)
                return false;

            await _approvalRepository.DeleteAsync(approvalId);
            return true;
        }
    }
}
