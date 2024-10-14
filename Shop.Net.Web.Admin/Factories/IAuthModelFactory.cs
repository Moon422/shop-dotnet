using System.Threading.Tasks;
using Shop.Net.Web.Admin.Models;

namespace Shop.Net.Web.Admin.Factories;

public interface IAuthModelFactory
{
    Task<LoginModel> PrepareLoginModelAsync(LoginModel model);
}

public class AuthModelFactory : IAuthModelFactory
{
    public async Task<LoginModel> PrepareLoginModelAsync(LoginModel model)
    {
        if (model is null)
            model = new LoginModel();

        return model;
    }
}