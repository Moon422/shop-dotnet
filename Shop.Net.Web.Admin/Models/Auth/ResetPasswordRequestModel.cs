using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Web.Admin.Models;

public record ResetPasswordRequestModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
}