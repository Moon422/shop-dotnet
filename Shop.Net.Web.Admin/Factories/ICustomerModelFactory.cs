using System.Threading.Tasks;
using Shop.Net.Core;
using Shop.Net.Web.Admin.Models;

namespace Shop.Net.Web.Admin.Factories;

public interface ICustomerModelFactory
{
    Task<CustomerSearchModel> PrepareCustomerSearchModelAsync(CustomerSearchModel searchModel);
}

[ScopeDependency(typeof(ICustomerModelFactory))]
public class CustomerModelFactory : ICustomerModelFactory
{
    public async Task<CustomerSearchModel> PrepareCustomerSearchModelAsync(CustomerSearchModel searchModel)
    {
        if (searchModel is null)
        {
            searchModel = new CustomerSearchModel();
        }

        return searchModel;
    }
}