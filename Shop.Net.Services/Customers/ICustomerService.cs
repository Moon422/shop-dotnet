using System.Threading.Tasks;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Data;

namespace Shop.Net.Services.Customers;

public interface ICustomerService
{
    Task<Customer?> GetCustomerByIdAsync(int customerId);

    Task<Customer?> GetCustomerByEmailAsync(string email, bool skipInactive = true);

    Task InsertCustomerAsync(Customer customer, bool deferDbInsert = false, bool deferCacheClear = false);

    Task UpdateCustomerAsync(Customer customer, bool deferDbUpdate = false, bool deferCacheClear = false);

    Task DeleteCustomerAsync(Customer customer, bool deferDbDelete = false, bool deferCacheClear = false);

    Task<PagedList<Customer>> SearchCustomerAsync(string firstName = "",
        string lastName = "",
        int genderId = 0,
        string phoneNumber = "",
        string email = "",
        bool hideDeleted = true,
        int pageIndex = 0,
        int pageSize = int.MaxValue);
}