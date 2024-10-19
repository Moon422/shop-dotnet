using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Net.Services.Customers;

public interface IPermissionService
{
    Task<IList<string>> GetRolePermissionsAsync(int roleId);

    Task<IList<string>> GetCustomerPermissionsAsync(int customerId);

    Task<bool> AuthorizeCustomerPermissionAsync(string permission);
}
