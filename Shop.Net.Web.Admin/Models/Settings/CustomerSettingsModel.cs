using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Web.Admin.Models.Settings;

public record CustomerSettingsModel : BaseEntityModel
{
    [Display(Name = "Public Customer Registration Enabled")]
    public bool PublicCustomerRegistrationEnabled { get; set; }
}