using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Net.Core.Domains.Customers;

namespace Shop.Net.Web.Admin.Models.Auth;

public class RegisterModel
{
    [MaxLength(160)]
    [MinLength(1)]
    [Display(Name = "First Name", Prompt = "John")]
    public string FirstName { get; set; }

    [MaxLength(72)]
    [MinLength(1)]
    [Display(Name = "Last Name", Prompt = "Doe")]
    public string LastName { get; set; }

    [AllowedValues([(int)Gender.Male, (int)Gender.Female])]
    [Display(Name = "Gender")]
    public int GenderId { get; set; }

    [Phone]
    [MaxLength(14)]
    [MinLength(1)]
    [Display(Name = "Phone Number", Prompt = "xxx-xxx xxx")]
    public string PhoneNumber { get; set; }

    [EmailAddress]
    [MaxLength(128)]
    [MinLength(1)]
    [Display(Name = "Email", Prompt = "johndoe@email.com")]
    public string Email { get; set; }

    [PasswordPropertyText]
    [MaxLength(12)]
    [MinLength(6)]
    [Display(Name = "Password", Prompt = "••••••••")]
    public string Password { get; set; }

    [PasswordPropertyText]
    [MaxLength(12)]
    [MinLength(6)]
    [Display(Name = "Confirm Password", Prompt = "••••••••")]
    public string ConfirmPassword { get; set; }

    public IList<SelectListItem> AvailableGenderOptions { get; set; }

    public RegisterModel()
    {
        AvailableGenderOptions = new List<SelectListItem>();
    }
}