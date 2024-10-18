using Newtonsoft.Json;
using Shop.Net.Core.Settings.Attributes;

namespace Shop.Net.Core.Settings;

[Settings("CustomerSettings.json")]
public class CustomerSettings
{
    public bool PublicCustomerRegistrationEnabled { get; set; }
}