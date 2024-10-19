using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Net.Core.Domains.Customers;

namespace Shop.Net.Services.Customers;

public interface IRoleService
{
    Task<IList<Role>> GetCustomerRolesAsync(int customerId);
    Task<Role?> GetRoleByNameAsync(string role);
}
