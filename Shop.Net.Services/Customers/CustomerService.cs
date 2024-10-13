using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Data;
using Shop.Net.Data.Exceptions;
using Shop.Net.Services.Caching;

namespace Shop.Net.Services.Customers;

public class CustomerService : ICustomerService
{
    protected readonly IRepository<Customer> customerRepository;
    protected readonly ITransactionManager transactionManager;
    protected readonly ICacheService cacheService;
    protected readonly IPasswordService passwordService;

    public CustomerService(IRepository<Customer> customerRepository,
        ITransactionManager transactionManager,
        ICacheService cacheService,
        IPasswordService passwordService)
    {
        this.customerRepository = customerRepository;
        this.transactionManager = transactionManager;
        this.cacheService = cacheService;
        this.passwordService = passwordService;
    }

    protected async Task<Customer?> GetCustomerByEmailAsync(string email, bool skipInactive = true)
    {
        var cacheKey = cacheService.PrepareCacheKey(CustomerCacheKeys.GetCustomerByEmailKey,
            email, skipInactive);

        return await cacheService.GetAsync(cacheKey,
            async () =>
            {
                var query = customerRepository.Table;

                if (skipInactive)
                {
                    query = query.Where(c => !c.IsDeleted);
                }

                return await query.FirstOrDefaultAsync();
            });
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        if (customerId <= 0)
        {
            throw new ArgumentException(nameof(customerId));
        }

        var cacheKey = cacheService.PrepareCacheKey(CustomerCacheKeys.GetCustomerByIdKey,
            customerId);

        return await cacheService.GetAsync(cacheKey,
            async () => await customerRepository.GetOneByIdAsync(customerId));
    }

    public async Task InsertCustomerAsync(Customer customer, bool deferDbInsert = false, bool deferCacheClear = false)
    {
        ArgumentNullException.ThrowIfNull(customer);

        var existing = await GetCustomerByEmailAsync(customer.Email);
        if (existing is not null)
        {
            throw new EntityDuplicateException();
        }

        await customerRepository.InsertAsync(customer, deferDbInsert);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(CustomerCacheKeys.PREFIX);
        }
    }

    public async Task UpdateCustomerAsync(Customer customer, bool deferDbUpdate = false, bool deferCacheClear = false)
    {
        ArgumentNullException.ThrowIfNull(customer);

        await customerRepository.UpdateAsync(customer, deferDbUpdate);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(CustomerCacheKeys.PREFIX);
        }
    }

    public async Task DeleteCustomerAsync(Customer customer, bool deferDbDelete = false, bool deferCacheClear = false)
    {
        ArgumentNullException.ThrowIfNull(customer);

        await customerRepository.DeleteAsync(customer, deferDbDelete);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(CustomerCacheKeys.PREFIX);
        }
    }

    public async Task<Customer> RegisterCustomerAsync(Customer customer, Password password)
    {
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentNullException.ThrowIfNull(password);

        await transactionManager.RunTransactionAsync(async () =>
        {
            await InsertCustomerAsync(customer, true, true);
            await passwordService.InsertPasswordAsync(password, true, true);
        });

        cacheService.RemoveByPrefix(CustomerCacheKeys.PREFIX);
        return customer;
    }

    public async Task<Customer?> LoginCustomerAsync(string email, string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        var customer = await GetCustomerByEmailAsync(email);
        if (customer is null)
        {
            return null;
        }

        var passwordEntity = await passwordService.GetPasswordByCustomerIdAsync(customer.Id);
        if (passwordEntity is null)
        {
            return null;
        }

        if (!passwordService.VerifyPassword(password, passwordEntity.PasswordHash))
        {
            return null;
        }

        return customer;
    }
}