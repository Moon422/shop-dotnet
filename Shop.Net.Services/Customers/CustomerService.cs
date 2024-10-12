using System;
using System.Threading.Tasks;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Data;
using Shop.Net.Services.Caching;

namespace Shop.Net.Services.Customers;

public class CustomerService
{
    protected readonly IRepository<Customer> customerRepository;
    protected readonly ICacheService cacheService;

    public CustomerService(IRepository<Customer> customerRepository,
        ICacheService cacheService)
    {
        this.customerRepository = customerRepository;
        this.cacheService = cacheService;
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        if (customerId <= 0)
            throw new ArgumentException(nameof(customerId));

        var cacheKey = cacheService.PrepareCacheKey(CustomerCacheKeys.GetCustomerByIdKey,
            customerId);

        return await cacheService.GetAsync(cacheKey,
            async () => await customerRepository.GetOneByIdAsync(customerId));
    }

    public async Task InsertCustomerAsync(Customer customer, bool deferDbInsert = false)
    {
        ArgumentNullException.ThrowIfNull(customer);

        await customerRepository.InsertAsync(customer, deferDbInsert);
        cacheService.RemoveByPrefix(CustomerCacheKeys.PREFIX);
    }

    public async Task UpdateCustomerAsync(Customer customer, bool deferDbUpdate = false)
    {
        ArgumentNullException.ThrowIfNull(customer);

        await customerRepository.UpdateAsync(customer, deferDbUpdate);
        cacheService.RemoveByPrefix(CustomerCacheKeys.PREFIX);
    }

    public async Task DeleteCustomerAsync(Customer customer, bool deferDbDelete = false)
    {
        ArgumentNullException.ThrowIfNull(customer);

        await customerRepository.DeleteAsync(customer, deferDbDelete);
        cacheService.RemoveByPrefix(CustomerCacheKeys.PREFIX);
    }
}