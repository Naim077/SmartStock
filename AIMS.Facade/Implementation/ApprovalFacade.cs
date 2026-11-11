using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class ApprovalFacade : IApprovalFacade
    {
        private readonly IApprovalRepository _approvalRepository;

        public ApprovalFacade(IApprovalRepository approvalRepository)
        {
            ArgumentNullException.ThrowIfNull(approvalRepository);
            _approvalRepository = approvalRepository;
        }

        public Task<List<Approval>> GetApprovalsAsync(CancellationToken cancellationToken = default)
        {
            return _approvalRepository.GetAllAsync(cancellationToken);
        }

        public Task<Approval?> GetApprovalByIdAsync(long approvalId, CancellationToken cancellationToken = default)
        {
            return _approvalRepository.GetByIdAsync(approvalId, cancellationToken);
        }

        public Task<List<Approval>> GetApprovalsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _approvalRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
