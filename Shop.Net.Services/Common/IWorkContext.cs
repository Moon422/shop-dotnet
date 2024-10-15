using System.Threading.Tasks;
using Shop.Net.Core.Domains.Customers;

namespace Shop.Net.Services.Common;

public interface IWorkContext
{
    Task<Customer?> GetActiveCustomerAsync();
}
