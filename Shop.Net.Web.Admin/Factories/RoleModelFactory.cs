using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Net.Core;
using Shop.Net.Web.Admin.Models;

namespace Shop.Net.Web.Admin.Factories;

[ScopeDependency(typeof(IRoleModelFactory))]
public class RoleModelFactory : IRoleModelFactory
{
    public async Task<RoleSearchModel> PrepareRoleSearchModelAsync(RoleSearchModel roleSearchModel)
    {
        if (roleSearchModel is null)
        {
            ArgumentNullException.ThrowIfNull(roleSearchModel);
        }

        return roleSearchModel;
    }
}
