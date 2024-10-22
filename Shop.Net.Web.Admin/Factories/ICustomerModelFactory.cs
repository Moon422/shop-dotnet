using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Net.Core;
using Shop.Net.Services.Customers;
using Shop.Net.Web.Admin.Models;
using Shop.Net.Web.Admin.Models.Common;
using Shop.Net.Web.Admin.Models.Customers;

namespace Shop.Net.Web.Admin.Factories;

public interface ICustomerModelFactory
{
    Task<CustomerSearchModel> PrepareCustomerSearchModelAsync(CustomerSearchModel searchModel);

    Task<PagedListModel<CustomerModel>> PrepareCustomerPagedListAsync(CustomerSearchModel searchModel);
}

[ScopeDependency(typeof(ICustomerModelFactory))]
public class CustomerModelFactory : ICustomerModelFactory
{
    protected readonly ICustomerService customerService;
    protected readonly IMapper mapper;

    public CustomerModelFactory(ICustomerService customerService,
    IMapper mapper)
    {
        this.customerService = customerService;
        this.mapper = mapper;
    }

    public async Task<CustomerSearchModel> PrepareCustomerSearchModelAsync(CustomerSearchModel searchModel)
    {
        if (searchModel is null)
        {
            searchModel = new CustomerSearchModel();
        }

        return searchModel;
    }

    public async Task<PagedListModel<CustomerModel>> PrepareCustomerPagedListAsync(CustomerSearchModel searchModel)
    {
        ArgumentNullException.ThrowIfNull(searchModel);

        var searchResult = await customerService.SearchCustomerAsync(pageIndex: searchModel.PageIndex - 1, pageSize: searchModel.PageSize);
        var model = mapper.Map<PagedListModel<CustomerModel>>(searchResult);
        model.Data = searchResult.Data
            .Select(c => mapper.Map<CustomerModel>(c))
            .ToList();

        return model;
    }
}