using Shop.Net.Core.Domains.Customers;
using Shop.Net.Web.Admin.Models.Customers;
using Shop.Net.Web.Admin.Models.Auth;
using Shop.Net.Core.Settings;
using Shop.Net.Web.Admin.Models.Settings;

namespace Shop.Net.Web.Admin;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Customer, CustomerModel>();
        CreateMap<PasswordModel, Password>();
        CreateMap<CustomerSettings, CustomerSettingsModel>()
            .ReverseMap();
    }
}