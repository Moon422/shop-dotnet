using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Web.Admin.Models.Auth;

public record PasswordModel
{
    [Required]
    [MaxLength(12)]
    [MinLength(6)]
    public string Password { get; set; }

    public int CustomerId { get; set; }
}
