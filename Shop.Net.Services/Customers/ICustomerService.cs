using System.Threading.Tasks;
using Shop.Net.Core.Domains.Customers;

namespace Shop.Net.Services.Customers;

public interface ICustomerService
{
    Task<Customer?> GetCustomerByIdAsync(int customerId);

    Task<Customer?> GetCustomerByEmailAsync(string email, bool skipInactive = true);

    Task InsertCustomerAsync(Customer customer, bool deferDbInsert = false, bool deferCacheClear = false);

    Task UpdateCustomerAsync(Customer customer, bool deferDbUpdate = false, bool deferCacheClear = false);

    Task DeleteCustomerAsync(Customer customer, bool deferDbDelete = false, bool deferCacheClear = false);
}