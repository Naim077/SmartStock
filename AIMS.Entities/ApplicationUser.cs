using Microsoft.AspNetCore.Identity;

namespace AIMS.Entities;

/// <summary>GUID-based Identity account; business fields belong to UserProfile.</summary>
public sealed class ApplicationUser : IdentityUser<Guid>
{
    public UserProfile? Profile { get; set; }
}
