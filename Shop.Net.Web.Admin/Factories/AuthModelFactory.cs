using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Net.Core;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Core.Settings;
using Shop.Net.Web.Admin.Models.Auth;

namespace Shop.Net.Web.Admin.Factories;

[ScopeDependency(typeof(IAuthModelFactory))]
public class AuthModelFactory : IAuthModelFactory
{
    protected readonly CustomerSettings customerSettings;

    public AuthModelFactory(CustomerSettings customerSettings)
    {
        this.customerSettings = customerSettings;
    }

    public async Task<LoginModel> PrepareLoginModelAsync(LoginModel model)
    {
        if (model is null)
            model = new LoginModel();

        model.PublicCustomerRegistrationEnabled = customerSettings.PublicCustomerRegistrationEnabled;

        return model;
    }

    public async Task<RegisterModel> PrepareRegisterModelAsync(RegisterModel model)
    {
        if (model is null)
            model = new RegisterModel();

        model.AvailableGenderOptions.Add(new SelectListItem("Select Gender", ""));
        model.AvailableGenderOptions.Add(new SelectListItem("Male", ((int)Gender.Male).ToString()));
        model.AvailableGenderOptions.Add(new SelectListItem("Female", ((int)Gender.Female).ToString()));

        return model;
    }
}