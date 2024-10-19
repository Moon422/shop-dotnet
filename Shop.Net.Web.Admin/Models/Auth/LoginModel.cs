using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Web.Admin.Models.Auth;

public record LoginModel
{
    [EmailAddress]
    [Required]
    [Display(Name = "Email Address", Prompt = "johndoe@email.com")]
    public string Email { get; set; }

    [PasswordPropertyText]
    [Required]
    [Display(Name = "Password", Prompt = "••••••••")]
    public string Password { get; set; }

    [Display(Name = "Remember Me")]
    public bool IsPersistent { get; set; }

    public bool PublicCustomerRegistrationEnabled { get; set; }
}
