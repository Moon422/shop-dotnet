using System;
using System.Threading.Tasks;
using Shop.Net.Core.Settings;
using Shop.Net.Web.Admin.Attributes;
using Shop.Net.Web.Admin.Models.Settings;

namespace Shop.Net.Web.Admin.Factories;

public interface ISettingsModelFactory
{
    Task<CustomerSettingsModel> PrepareCustomerSettingsModelAsync(CustomerSettingsModel settingsModel, CustomerSettings customerSettings);
}

[ScopeDependency(typeof(ISettingsModelFactory))]
public class SettingsModelFactory : ISettingsModelFactory
{
    public async Task<CustomerSettingsModel> PrepareCustomerSettingsModelAsync(CustomerSettingsModel settingsModel, CustomerSettings customerSettings)
    {
        ArgumentNullException.ThrowIfNull(settingsModel);
        ArgumentNullException.ThrowIfNull(customerSettings);

        return settingsModel;
    }
}