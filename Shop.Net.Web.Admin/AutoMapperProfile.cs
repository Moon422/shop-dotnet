using Shop.Net.Core.Domains.Customers;
using Shop.Net.Web.Admin.Models;

namespace Shop.Net.Web.Admin;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Customer, CustomerModel>();
        CreateMap<PasswordModel, Password>();
    }
}