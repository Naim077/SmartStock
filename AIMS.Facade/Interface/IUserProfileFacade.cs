using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed UserProfile records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IUserProfileFacade
    {
        Task<List<UserProfile>> GetUserProfilesAsync(CancellationToken cancellationToken = default);
        Task<UserProfile?> GetUserProfileByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<List<UserProfile>> GetUserProfilesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
