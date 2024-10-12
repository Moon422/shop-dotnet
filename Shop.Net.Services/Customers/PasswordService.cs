using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Net.Core.Configurations;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Data;
using Shop.Net.Services.Caching;

namespace Shop.Net.Services.Customers;

public class PasswordService : IPasswordService
{
    protected readonly IApplicationSettings applicationSettings;
    protected readonly IRepository<Password> passwordRepository;
    protected readonly ICacheService cacheService;

    public PasswordService(IApplicationSettings applicationSettings,
        IRepository<Password> passwordRepository,
        ICacheService cacheService)
    {
        this.applicationSettings = applicationSettings;
        this.passwordRepository = passwordRepository;
        this.cacheService = cacheService;
    }

    public string HashPassword(string rawPassword)
    {
        return BCrypt.Net.BCrypt.HashPassword(rawPassword);
    }

    public bool VerifyPassword(string rawPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(rawPassword, hashedPassword);
    }

    public async Task<Password?> GetPasswordByCustomerIdAsync(int customerId)
    {
        if (customerId <= 0)
        {
            throw new ArgumentException($"{nameof(customerId)} cannot be zero or less than zero");
        }

        var cacheKey = cacheService.PrepareCacheKey(CustomerCacheKeys.GetPasswordByCustomerIdKey,
            customerId);

        return await cacheService.GetAsync(cacheKey, async () =>
        {
            return await passwordRepository.Table
                .FirstOrDefaultAsync(p => p.CustomerId == customerId);
        });
    }

    public async Task InsertPasswordAsync(Password entity, bool deferDbInsert = true, bool deferCacheClear = false)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await passwordRepository.InsertAsync(entity, deferDbInsert);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(CustomerCacheKeys.PREFIX);
        }
    }

    public async Task UpdatePasswordAsync(Password entity, bool deferDbUpdate = true, bool deferCacheClear = false)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await passwordRepository.UpdateAsync(entity, deferDbUpdate);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(CustomerCacheKeys.PREFIX);
        }
    }

    public async Task DeletePasswordAsync(Password entity, bool deferDbDelete = true, bool deferCacheClear = false)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await passwordRepository.DeleteAsync(entity, deferDbDelete);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(CustomerCacheKeys.PREFIX);
        }
    }
}