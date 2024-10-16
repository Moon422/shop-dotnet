using Shop.Net.Web.Admin.Models.Customers;

namespace Shop.Net.Web.Admin.Models.Common;

public record NavbarModel : BaseModel
{
    public CustomerModel Customer { get; set; }
}