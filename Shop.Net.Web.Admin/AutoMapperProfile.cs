using Shop.Net.Core.Domains.Customers;
using Shop.Net.Web.Admin.Models.Customers;
using Shop.Net.Web.Admin.Models.Auth;
using Shop.Net.Core.Settings;
using Shop.Net.Web.Admin.Models.Settings;
using Shop.Net.Data;
using Shop.Net.Web.Admin.Models.Common;
using Shop.Net.Core.Domains;

namespace Shop.Net.Web.Admin;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Customer, CustomerModel>();
        CreateMap<PasswordModel, Password>();
        CreateMap<CustomerSettings, CustomerSettingsModel>()
            .ReverseMap();
        CreateMap(typeof(PagedList<>), typeof(PagedListModel<>))
            .ForMember(nameof(PagedList<BaseEntity>.Data), opt => opt.Ignore());
    }
}