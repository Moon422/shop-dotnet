using System;
using System.Threading.Tasks;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Data;

namespace Shop.Net.Services.Customers;

public class CustomerService
{
    protected readonly IRepository<Customer> customerRepository;

    public CustomerService(IRepository<Customer> customerRepository)
    {
        this.customerRepository = customerRepository;
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        if (customerId <= 0)
            throw new ArgumentException(nameof(customerId));

        return await customerRepository.GetOneByIdAsync(customerId);
    }
}