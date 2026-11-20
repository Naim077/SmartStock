using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class ApprovalRepository : IApprovalRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ApprovalRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Approval>> GetAllAsync()
        {
            return await _dbContext.Approvals.ToListAsync();
        }

        public async Task<Approval?> GetByIdAsync(long approvalId)
        {
            return await _dbContext.Approvals.FindAsync(new object[] { approvalId });
        }

        public async Task AddAsync(Approval approval)
        {
            await _dbContext.Approvals.AddAsync(approval);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Approval approval)
        {
            _dbContext.Approvals.Update(approval);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long approvalId)
        {
            var approval = await _dbContext.Approvals.FindAsync(new object[] { approvalId });
            if (approval == null)
                return;

            _dbContext.Approvals.Remove(approval);
            await _dbContext.SaveChangesAsync();
        }
    }
}
