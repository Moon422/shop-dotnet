using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Net.Core;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Data;
using Shop.Net.Data.Exceptions;
using Shop.Net.Services.Caching;

namespace Shop.Net.Services.Customers;

[ScopeDependency(typeof(IRoleService))]
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

    public async Task<IList<Role>> GetAllRolesAsync()
    {
        var cacheKey = cacheService.PrepareCacheKey(RoleCacheKeys.GetAllRolesCacheKey);

        return await cacheService.GetAsync(cacheKey, async () =>
        {
            return await roleRepository.GetAllAsync();
        });
    }

    public async Task<PagedList<Role>> SearchRolesAsync(bool orderById,
        int pageIndex = 0,
        int pageSize = int.MaxValue)
    {
        var query = roleRepository.Table;

        if (orderById)
        {
            query = query.OrderByDescending(e => e.Id);
        }
        else
        {
            query = query.OrderBy(e => e.Id);
        }

        var data = await query.Skip(pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<Role>
        {
            Data = data,
            PageIndex = pageIndex,
            TotalItemCount = await query.CountAsync(),
            MaximumPageItemCount = pageSize
        };
    }

    public async Task<Role?> GetRoleByIdAsync(int id)
    {
        if (0 >= id)
        {
            throw new ArgumentException(nameof(id));
        }

        var cacheKey = cacheService.PrepareCacheKey(RoleCacheKeys.GetRoleByIdCacheKey, id);
        return await cacheService.GetAsync(cacheKey, async () =>
        {
            return await roleRepository.GetOneByIdAsync(id);
        });
    }

    public async Task InsertRoleAsync(Role role, bool deferDbInsert = false, bool deferCacheClear = false)
    {
        ArgumentNullException.ThrowIfNull(role);

        if ((await GetRoleByNameAsync(role.Name)) is Role)
        {
            throw new EntityDuplicateException();
        }

        await roleRepository.InsertAsync(role, deferDbInsert);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(RoleCacheKeys.PREFIX);
        }
    }

    public async Task UpdateRoleAsync(Role role, bool deferDbUpdate = false, bool deferCacheClear = false)
    {
        ArgumentNullException.ThrowIfNull(role);

        await roleRepository.UpdateAsync(role, deferDbUpdate);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(RoleCacheKeys.PREFIX);
        }
    }

    public async Task DeleteRoleAsync(Role role, bool deferDbDelete = false, bool deferCacheClear = false)
    {
        ArgumentNullException.ThrowIfNull(role);

        await roleRepository.DeleteAsync(role, deferDbDelete);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(RoleCacheKeys.PREFIX);
        }
    }

    public async Task<IList<Role>> GetCustomerRolesAsync(int customerId)
    {
        if (customerId <= 0)
        {
            return [];
        }

        var cacheKey = cacheService.PrepareCacheKey(CustomerCacheKeys.GetCustomerRolesCacheKey,
            customerId);

        return await cacheService.GetAsync<Role>(cacheKey, async () =>
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

    public async Task<Role?> GetRoleByNameAsync(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            return null;
        }

        var cacheKey = cacheService.PrepareCacheKey(RoleCacheKeys.GetRoleByNameCacheKey,
            roleName);

        return await cacheService.GetAsync(cacheKey, async () =>
        {
            return await roleRepository.Table
                .FirstOrDefaultAsync(role => role.Name == roleName);
        });
    }
}