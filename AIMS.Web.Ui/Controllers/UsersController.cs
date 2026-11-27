using AIMS.Entities;
using AIMS.Facade.Interface;
using AIMS.Web.Ui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIMS.Web.Ui.Controllers;

[Authorize]
public class UsersController : Controller
{
    private readonly IUserProfileFacade _profileFacade;
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public UsersController(
        IUserProfileFacade profileFacade,
        UserManager<IdentityUser<Guid>> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _profileFacade = profileFacade;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var profiles = await _profileFacade.GetUserProfilesAsync();
        var identities = _userManager.Users.ToDictionary(x => x.Id);
        var roles = new Dictionary<Guid, string>();
        var emails = new Dictionary<Guid, string>();

        foreach (var profile in profiles)
        {
            if (identities.TryGetValue(profile.UserId, out var identity))
            {
                emails[profile.UserId] = identity.Email ?? "";
                roles[profile.UserId] = string.Join(", ", await _userManager.GetRolesAsync(identity));
            }
        }

        ViewBag.Emails = emails;
        ViewBag.Roles = roles;
        return View(profiles.OrderBy(x => x.DisplayName).ToList());
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadRoles();
        return View(new UserFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserFormViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Password) || model.Password.Length < 8)
            ModelState.AddModelError(nameof(model.Password), "A password with at least 8 characters is required.");

        if (await _userManager.FindByEmailAsync(model.Email) != null)
            ModelState.AddModelError(nameof(model.Email), "This email is already registered.");

        if (!string.IsNullOrWhiteSpace(model.Role) && !await _roleManager.RoleExistsAsync(model.Role))
            ModelState.AddModelError(nameof(model.Role), "Select a role that exists in the system.");

        if (!ModelState.IsValid)
        {
            await LoadRoles();
            return View(model);
        }

        var user = new IdentityUser<Guid>
        {
            Id = Guid.NewGuid(),
            UserName = model.Email.Trim(),
            Email = model.Email.Trim(),
            EmailConfirmed = false,
            LockoutEnabled = true
        };

        var createResult = await _userManager.CreateAsync(user, model.Password!);
        if (!createResult.Succeeded)
        {
            AddIdentityErrors(createResult);
            await LoadRoles();
            return View(model);
        }

        try
        {
            var profile = new UserProfile
            {
                UserId = user.Id,
                DisplayName = model.DisplayName.Trim(),
                IsActive = model.IsActive,
                MustChangePassword = true,
                CreatedUtc = DateTime.UtcNow
            };
            await _profileFacade.AddUserProfileAsync(profile);

            if (!string.IsNullOrWhiteSpace(model.Role))
            {
                var roleResult = await _userManager.AddToRoleAsync(user, model.Role);
                if (!roleResult.Succeeded)
                    throw new InvalidOperationException(string.Join("; ", roleResult.Errors.Select(x => x.Description)));
            }

            if (!model.IsActive)
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);

            TempData["SuccessMessage"] = "User account created successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            var createdProfile = await _profileFacade.GetUserProfileByIdAsync(user.Id);
            if (createdProfile != null)
                await _profileFacade.DeleteUserProfileAsync(user.Id);

            await _userManager.DeleteAsync(user);
            ModelState.AddModelError(string.Empty, "The user profile or role could not be saved. The new account was rolled back.");
            await LoadRoles();
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var profile = await _profileFacade.GetUserProfileByIdAsync(id);
        var identity = await _userManager.FindByIdAsync(id.ToString());
        if (profile == null || identity == null)
            return NotFound();

        await LoadRoles();
        var currentRoles = await _userManager.GetRolesAsync(identity);
        return View(new UserFormViewModel
        {
            UserId = id,
            DisplayName = profile.DisplayName,
            Email = identity.Email ?? "",
            IsActive = profile.IsActive,
            Role = currentRoles.FirstOrDefault()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UserFormViewModel model)
    {
        if (id != model.UserId)
            return BadRequest();

        var profile = await _profileFacade.GetUserProfileByIdAsync(id);
        var identity = await _userManager.FindByIdAsync(id.ToString());
        if (profile == null || identity == null)
            return NotFound();

        if (!string.IsNullOrWhiteSpace(model.Role) && !await _roleManager.RoleExistsAsync(model.Role))
            ModelState.AddModelError(nameof(model.Role), "Select a role that exists in the system.");

        if (!model.IsActive && User.FindFirstValue(ClaimTypes.NameIdentifier) == id.ToString())
            ModelState.AddModelError(nameof(model.IsActive), "You cannot deactivate your own signed-in account.");

        var emailOwner = await _userManager.FindByEmailAsync(model.Email);
        if (emailOwner != null && emailOwner.Id != id)
            ModelState.AddModelError(nameof(model.Email), "This email is already used by another account.");

        if (!ModelState.IsValid)
        {
            await LoadRoles();
            return View(model);
        }

        var newEmail = model.Email.Trim();
        identity.Email = newEmail;
        identity.UserName = newEmail;
        identity.NormalizedEmail = _userManager.NormalizeEmail(newEmail);
        identity.NormalizedUserName = _userManager.NormalizeName(newEmail);
        var identityResult = await _userManager.UpdateAsync(identity);
        if (!identityResult.Succeeded)
        {
            AddIdentityErrors(identityResult);
            await LoadRoles();
            return View(model);
        }

        profile.DisplayName = model.DisplayName.Trim();
        profile.IsActive = model.IsActive;
        if (!model.IsActive)
            profile.DeactivatedUtc = DateTime.UtcNow;
        else
            profile.DeactivatedUtc = null;

        await _profileFacade.UpdateUserProfileAsync(profile);

        var currentRoles = await _userManager.GetRolesAsync(identity);
        if (currentRoles.Count > 0)
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(identity, currentRoles);
            if (!removeResult.Succeeded)
            {
                AddIdentityErrors(removeResult);
                await LoadRoles();
                return View(model);
            }
        }

        if (!string.IsNullOrWhiteSpace(model.Role))
        {
            var addResult = await _userManager.AddToRoleAsync(identity, model.Role);
            if (!addResult.Succeeded)
            {
                AddIdentityErrors(addResult);
                await LoadRoles();
                return View(model);
            }
        }

        await _userManager.SetLockoutEnabledAsync(identity, true);
        await _userManager.SetLockoutEndDateAsync(identity,
            model.IsActive ? null : DateTimeOffset.MaxValue);

        TempData["SuccessMessage"] = "User profile and role updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        // Identity users are referenced by operational records and audit history.
        // This action therefore performs a reversible soft-delete (deactivation).
        var profile = await _profileFacade.GetUserProfileByIdAsync(id);
        var identity = await _userManager.FindByIdAsync(id.ToString());
        if (profile == null || identity == null)
            return NotFound();

        if (User.FindFirstValue(ClaimTypes.NameIdentifier) == id.ToString())
        {
            TempData["ErrorMessage"] = "You cannot deactivate your own signed-in account.";
            return RedirectToAction(nameof(Index));
        }

        profile.IsActive = false;
        profile.DeactivatedUtc = DateTime.UtcNow;
        await _profileFacade.UpdateUserProfileAsync(profile);
        await _userManager.SetLockoutEnabledAsync(identity, true);
        await _userManager.SetLockoutEndDateAsync(identity, DateTimeOffset.MaxValue);

        TempData["SuccessMessage"] = "User deactivated. Historical records have been preserved.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Activate(Guid id)
    {
        var profile = await _profileFacade.GetUserProfileByIdAsync(id);
        var identity = await _userManager.FindByIdAsync(id.ToString());
        if (profile == null || identity == null)
            return NotFound();

        profile.IsActive = true;
        profile.DeactivatedUtc = null;
        await _profileFacade.UpdateUserProfileAsync(profile);
        await _userManager.SetLockoutEndDateAsync(identity, null);

        TempData["SuccessMessage"] = "User activated successfully.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadRoles()
    {
        ViewBag.RoleList = _roleManager.Roles
            .Where(x => x.Name != null)
            .Select(x => x.Name!)
            .OrderBy(x => x)
            .ToList();
    }

    private void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);
    }
}
