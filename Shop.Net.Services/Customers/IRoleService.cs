using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Data;
using Shop.Net.Services.Caching;

namespace Shop.Net.Services.Customers;

public interface IRoleService
{
    Task<IList<Role>> GetCustomerRolesAsync(int customerId);
}

public class RoleService : IRoleService
{
    protected readonly IRepository<Role> roleRepository;
    protected readonly IRepository<CustomerRoleMapping> customerRoleMappingRepository;
    protected readonly ICacheService cacheService;

    public RoleService(IRepository<Role> roleRepository,
        IRepository<CustomerRoleMapping> customerRoleMappingRepository,
        ICacheService cacheService)
    {
        this.roleRepository = roleRepository;
        this.customerRoleMappingRepository = customerRoleMappingRepository;
        this.cacheService = cacheService;
    }

    public async Task<IList<Role>> GetCustomerRolesAsync(int customerId)
    {
        if (customerId <= 0)
        {
            return [];
        }

        var cacheKey = cacheService.PrepareCacheKey(CustomerCacheKeys.GetCustomerRolesCacheKey,
            customerId);

        return await cacheService.GetAllAsync<Role>(cacheKey, async () =>
        {
            return await customerRoleMappingRepository.Table
                .Where(crm => crm.CustomerId == customerId)
                .Join(roleRepository.Table,
                    crm => crm.RoleId,
                    r => r.Id,
                    (crm, r) => r)
                .ToListAsync();
        });
    }
}