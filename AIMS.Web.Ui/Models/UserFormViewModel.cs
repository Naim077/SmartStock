using System.ComponentModel.DataAnnotations;

namespace AIMS.Web.Ui.Models;

public class UserFormViewModel
{
    public Guid UserId { get; set; }

    [Required, StringLength(200)]
    public string DisplayName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(256)]
    public string Email { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Password { get; set; }

    public string? Role { get; set; }

    public bool IsActive { get; set; } = true;
}
