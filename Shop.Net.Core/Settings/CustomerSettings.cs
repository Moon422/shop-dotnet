using Shop.Net.Core.Settings.Attributes;

namespace Shop.Net.Core.Settings;

[Settings("CustomerSettings.json")]
public class CustomerSettings : ISettings
{
    public bool PublicCustomerRegistrationEnabled { get; set; }
}