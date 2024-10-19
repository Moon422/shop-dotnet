using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Net.Core;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Data;
using Shop.Net.Services.Caching;
using Shop.Net.Services.Common;

namespace Shop.Net.Services.Customers;

[ScopeDependency(typeof(IPermissionService))]
public class PermissionService : IPermissionService
{
    protected readonly IRepository<RolePermission> rolePermissionRepository;
    protected readonly IRepository<CustomerPermission> customerPermissionRepository;
    protected readonly IRoleService roleService;
    protected readonly ICacheService cacheService;
    protected readonly IWorkContext workContext;

    public PermissionService(IRepository<RolePermission> rolePermissionRepository,
        IRepository<CustomerPermission> customerPermissionRepository,
        IRoleService roleService,
        ICacheService cacheService,
        IWorkContext workContext)
    {
        this.rolePermissionRepository = rolePermissionRepository;
        this.customerPermissionRepository = customerPermissionRepository;
        this.roleService = roleService;
        this.cacheService = cacheService;
        this.workContext = workContext;
    }

    public async Task<IList<string>> GetRolePermissionsAsync(int roleId)
    {
        if (roleId <= 0)
        {
            throw new ArgumentException(nameof(roleId));
        }

        var cacheKey = cacheService.PrepareCacheKey(RoleCacheKeys.GetRolePermissionsCacheKey,
            roleId);

        return await cacheService.GetAsync<string>(cacheKey, async () =>
        {
            return await rolePermissionRepository.Table
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.Permission)
                .ToListAsync();
        });
    }

    public async Task<IList<string>> GetCustomerPermissionsAsync(int customerId)
    {
        if (customerId <= 0)
        {
            throw new ArgumentException(nameof(customerId));
        }

        var cacheKey = cacheService.PrepareCacheKey(CustomerCacheKeys.GetCustomerPermissionsCacheKey,
            customerId);

        return await cacheService.GetAsync<string>(cacheKey, async () =>
        {
            return await customerPermissionRepository.Table
                .Where(cp => cp.CustomerId == customerId)
                .Select(cp => cp.Permission)
                .ToListAsync();
        });
    }

    public async Task<bool> AuthorizeCustomerPermissionAsync(string permission)
    {
        if (string.IsNullOrWhiteSpace(permission))
        {
            return true;
        }

        if ((await workContext.GetActiveCustomerAsync()) is not Customer customer)
        {
            return false;
        }

        IList<Role> customerRoles = await roleService.GetCustomerRolesAsync(customer.Id);
        foreach (var customerRole in customerRoles)
        {
            if ((await GetRolePermissionsAsync(customerRole.Id)).Any(rp => rp.Equals(permission, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
        }

        if ((await GetCustomerPermissionsAsync(customer.Id)).Contains(permission))
        {
            return true;
        }

        return false;
    }
}