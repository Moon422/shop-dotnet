using System.Threading.Tasks;
using Shop.Net.Web.Admin.Models.Auth;

namespace Shop.Net.Web.Admin.Factories;

public interface IAuthModelFactory
{
    Task<LoginModel> PrepareLoginModelAsync(LoginModel model);
    Task<RegisterModel> PrepareRegisterModelAsync(RegisterModel model);
}
