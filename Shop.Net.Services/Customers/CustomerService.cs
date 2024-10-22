using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Net.Core;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Data;
using Shop.Net.Data.Exceptions;
using Shop.Net.Services.Caching;

namespace Shop.Net.Services.Customers;

[ScopeDependency(typeof(ICustomerService))]
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

    public async Task<Customer?> GetCustomerByEmailAsync(string email, bool skipInactive = true)
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
            cacheService.RemoveByPrefix(CustomerCacheKeys.CUSTOMER_PREFIX);
        }
    }

    public async Task UpdateCustomerAsync(Customer customer, bool deferDbUpdate = false, bool deferCacheClear = false)
    {
        ArgumentNullException.ThrowIfNull(customer);

        await customerRepository.UpdateAsync(customer, deferDbUpdate);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(CustomerCacheKeys.CUSTOMER_PREFIX);
        }
    }

    public async Task DeleteCustomerAsync(Customer customer, bool deferDbDelete = false, bool deferCacheClear = false)
    {
        ArgumentNullException.ThrowIfNull(customer);

        await customerRepository.DeleteAsync(customer, deferDbDelete);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(CustomerCacheKeys.CUSTOMER_PREFIX);
        }
    }

    public async Task<PagedList<Customer>> SearchCustomerAsync(string firstName = "",
        string lastName = "",
        int genderId = 0,
        string phoneNumber = "",
        string email = "",
        bool hideInactive = true,
        int pageIndex = 0,
        int pageSize = int.MaxValue)
    {
        if (pageIndex < 0)
        {
            throw new ArgumentException(nameof(pageIndex));
        }

        if (pageSize <= 0)
        {
            throw new ArgumentException(nameof(pageSize));
        }

        var query = customerRepository.Table;

        if (hideInactive)
        {
            query = query.Where(c => !c.IsDeleted);
        }

        if (!string.IsNullOrWhiteSpace(firstName))
        {
            query = query.Where(c => c.FirstName.Contains(firstName));
        }

        if (!string.IsNullOrWhiteSpace(lastName))
        {
            query = query.Where(c => c.LastName.Contains(lastName));
        }

        if (!string.IsNullOrWhiteSpace(phoneNumber))
        {
            query = query.Where(c => c.PhoneNumber.Contains(phoneNumber));
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            query = query.Where(c => c.Email.Contains(email));
        }

        var data = await query.OrderBy(c => c.Id)
            .Skip(pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<Customer>
        {
            Data = data,
            PageIndex = pageIndex,
            TotalItemCount = await customerRepository.Table.CountAsync(),
            MaximumPageItemCount = pageSize
        };
    }
}