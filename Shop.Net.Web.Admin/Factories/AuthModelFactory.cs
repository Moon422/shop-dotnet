using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Web.Admin.Models;

namespace Shop.Net.Web.Admin.Factories;

public class AuthModelFactory : IAuthModelFactory
{
    public async Task<LoginModel> PrepareLoginModelAsync(LoginModel model)
    {
        if (model is null)
            model = new LoginModel();

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