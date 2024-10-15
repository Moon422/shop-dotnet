using System.Collections.Generic;

namespace Shop.Net.Web.Admin.Models;

public record CustomerDetailsModel : BaseModel
{
    public CustomerModel Customer { get; set; }
    public IList<RoleModel> Roles { get; set; }

    public IList<CustomerPermissionModel> CustomerPermissions { get; set; }
}