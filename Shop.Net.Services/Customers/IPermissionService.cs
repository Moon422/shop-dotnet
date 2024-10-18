using System.Threading.Tasks;
using Shop.Net.Core.Domains.Customers;

namespace Shop.Net.Services.Customers;

public interface IPermissionService
{

}

public class PermissionService : IPermissionService
{
    public async Task<bool> AuthorizeAsync(string permission)
    {
        if (string.IsNullOrWhiteSpace(permission))
        {
            return true;
        }


    }
}