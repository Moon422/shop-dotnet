using System;
using System.Threading.Tasks;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Data;
using Shop.Net.Services.Caching;

namespace Shop.Net.Services.Customers;

public class CustomerService
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

    public async Task RegisterCustomer(Customer customer, Password password)
    {
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentNullException.ThrowIfNull(password);

        await transactionManager.RunTransactionAsync(async () =>
        {
            await InsertCustomerAsync(customer, true, true);
            await passwordService.InsertPasswordAsync(password, true, true);
        });

        cacheService.RemoveByPrefix(CustomerCacheKeys.PREFIX);
    }
}