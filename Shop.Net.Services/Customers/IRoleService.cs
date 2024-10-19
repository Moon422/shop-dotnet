using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Net.Core.Domains;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Data;

namespace Shop.Net.Services.Customers;

public interface IRoleService
{
    Task<IList<Role>> GetAllRolesAsync();
    Task<PagedList<Role>> SearchRolesAsync(bool orderById, int pageIndex = 0, int pageSize = int.MaxValue);
    Task<Role?> GetRoleByIdAsync(int id);
    Task InsertRoleAsync(Role role, bool deferDbInsert = false, bool deferCacheClear = false);
    Task UpdateRoleAsync(Role role, bool deferDbUpdate = false, bool deferCacheClear = false);
    Task DeleteRoleAsync(Role role, bool deferDbDelete = false, bool deferCacheClear = false);
    Task<IList<Role>> GetCustomerRolesAsync(int customerId);
    Task<Role?> GetRoleByNameAsync(string role);
}
