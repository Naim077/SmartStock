using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IApprovalRepository
    {
        Task<List<Approval>> GetAllAsync();
        Task<Approval?> GetByIdAsync(long approvalId);
        Task AddAsync(Approval approval);
        Task UpdateAsync(Approval approval);
        Task DeleteAsync(long approvalId);
    }
}
