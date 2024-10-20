using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Services.Common;
using Shop.Net.Services.Customers;
using Shop.Net.Web.Admin.Helpers;

namespace Shop.Net.Web.Admin.Filters;

public class PermissionAuthorizeAttribute : TypeFilterAttribute
{
    public PermissionAuthorizeAttribute(string permissions) : base(typeof(PermissionAuthorizeFilter))
    {
        Arguments = new object[] { permissions };
    }

    public class PermissionAuthorizeFilter : IAsyncAuthorizationFilter
    {
        protected readonly IRoleService roleService;
        protected readonly IPermissionService permissionService;
        protected readonly IWorkContext workContext;
        protected readonly string[] permissions;

        public PermissionAuthorizeFilter(string permissions,
            IRoleService roleService,
            IWorkContext workContext,
            IPermissionService permissionService)
        {
            this.permissions = permissions.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(permission => permission.Trim())
                .ToArray();

            this.permissionService = permissionService;
            this.roleService = roleService;
            this.workContext = workContext;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if ((await workContext.GetActiveCustomerAsync()) is not Customer customer || customer.RequireAuthRefresh)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!permissions.Any())
            {
                return;
            }

            IList<Role> customerRoles = await roleService.GetCustomerRolesAsync(customer.Id);
            foreach (var customerRole in customerRoles)
            {
                if ((await permissionService.GetRolePermissionsAsync(customerRole.Id)).Any(rp => permissions.Contains(rp, new RoleEqualityComparer())))
                {
                    return;
                }
            }

            if ((await permissionService.GetCustomerPermissionsAsync(customer.Id)).Any(cp => permissions.Contains(cp, new PermissionEqualityComparer())))
            {
                return;
            }

            context.Result = new ForbidResult();
        }
    }
}
