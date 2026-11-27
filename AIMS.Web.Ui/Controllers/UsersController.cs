using AIMS.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AIMS.Web.Ui.Controllers;

[Authorize]
public class UsersController : Controller
{
    private readonly IUserProfileFacade _profileFacade;
    private readonly UserManager<IdentityUser<Guid>> _userManager;

    public UsersController(IUserProfileFacade profileFacade, UserManager<IdentityUser<Guid>> userManager)
    {
        _profileFacade = profileFacade;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var profiles = await _profileFacade.GetUserProfilesAsync();
        var identities = _userManager.Users.ToDictionary(x => x.Id);
        var roles = new Dictionary<Guid, string>();
        foreach (var profile in profiles)
        {
            if (identities.TryGetValue(profile.UserId, out var identity))
                roles[profile.UserId] = string.Join(", ", await _userManager.GetRolesAsync(identity));
        }

        ViewBag.Emails = identities.ToDictionary(x => x.Key, x => x.Value.Email ?? "");
        ViewBag.Roles = roles;
        return View(profiles.OrderBy(x => x.DisplayName).ToList());
    }
}
