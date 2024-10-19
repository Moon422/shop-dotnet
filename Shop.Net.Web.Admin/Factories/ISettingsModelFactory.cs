using System.Threading.Tasks;
using Shop.Net.Core.Settings;
using Shop.Net.Web.Admin.Models.Settings;

namespace Shop.Net.Web.Admin.Factories;

public interface ISettingsModelFactory
{
    Task<CustomerSettingsModel> PrepareCustomerSettingsModelAsync(CustomerSettingsModel settingsModel, CustomerSettings customerSettings);
}
