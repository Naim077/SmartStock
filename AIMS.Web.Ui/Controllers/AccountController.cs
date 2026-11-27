
using AIMS.DataAccess;
using AIMS.Entities;
using AIMS.Web.Ui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AIMS.Web.Ui.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    private readonly SignInManager<IdentityUser<Guid>> _signInManager;
    private readonly ApplicationDbContext _context;

    public AccountController(
        UserManager<IdentityUser<Guid>> userManager,
        SignInManager<IdentityUser<Guid>> signInManager,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var existingUser = await _userManager
            .FindByEmailAsync(model.Email);

        if (existingUser != null)
        {
            ModelState.AddModelError(
                "Email",
                "This email is already registered.");

            return View(model);
        }

        var isFirstRegisteredUser = !_userManager.Users.Any();

        var user = new IdentityUser<Guid>
        {
            Id = Guid.NewGuid(),
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(
            user,
            model.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(
                    string.Empty,
                    error.Description);
            }

            return View(model);
        }

        UserProfile? profile = null;
        try
        {
            profile = new UserProfile
            {
                UserId = user.Id,
                DisplayName = model.DisplayName,
                IsActive = true,
                MustChangePassword = false,
                CreatedUtc = DateTime.UtcNow
            };

            await _context.UserProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();

            // The first registered account becomes the initial Owner.
            if (isFirstRegisteredUser)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "Owner");
                if (!roleResult.Succeeded)
                    throw new InvalidOperationException(string.Join("; ", roleResult.Errors.Select(x => x.Description)));
            }

            await _signInManager.SignInAsync(
                user,
                isPersistent: false);

            return RedirectToAction(
                "Index",
                "Dashboard");
        }
        catch
        {
            // Remove the profile and Identity account if initial registration setup fails.
            if (profile != null)
            {
                _context.UserProfiles.Remove(profile);
                await _context.SaveChangesAsync();
            }

            await _userManager.DeleteAsync(user);

            ModelState.AddModelError(
                string.Empty,
                "Registration failed. Please try again.");

            return View(model);
        }
    }


    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(
        LoginViewModel model,
        string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: true
        );

        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(returnUrl) &&
                Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(
                string.Empty,
                "Your account is temporarily locked. Please try again later."
            );

            return View(model);
        }

        ModelState.AddModelError(
            string.Empty,
            "Invalid email or password."
        );

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Login", "Account");
    }

}
