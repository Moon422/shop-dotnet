using System.Threading.Tasks;
using Shop.Net.Web.Admin.Models;

namespace Shop.Net.Web.Admin.Factories;

public interface IRoleModelFactory
{
    Task<RoleSearchModel> PrepareRoleSearchModelAsync(RoleSearchModel roleSearchModel);
}
