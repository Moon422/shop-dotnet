namespace Shop.Net.Web.Admin.Models;

public record NavbarModel : BaseModel
{
    public CustomerModel Customer { get; set; }
}