using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Services.Customers;

namespace Shop.Net.Web.Admin.Attributes;

public class PermissionAuthorizeAttribute : TypeFilterAttribute
{
    public PermissionAuthorizeAttribute(string permissions) : base(typeof(PermissionAuthorizeFilter))
    {
        Arguments = new object[] { permissions };
    }

    public class PermissionAuthorizeFilter : IAuthorizationFilter
    {
        protected readonly IRoleService roleService;
        protected readonly IPermissionService permissionService;
        protected readonly string[] permissions;

        public PermissionAuthorizeFilter(string permissions,
            IRoleService roleService,
            IPermissionService permissionService)
        {
            this.permissions = permissions.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(permission => permission.Trim())
                .ToArray();

            this.permissionService = permissionService;
            this.roleService = roleService;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
            }

            if (!permissions.Any())
            {
                return;
            }

            ClaimsPrincipal user = context.HttpContext.User;

            IEnumerable<string> roleNames = user.FindAll(ClaimTypes.Role)
                .Select(rc => rc.Value);

            foreach (var roleName in roleNames)
            {
                if ((await roleService.GetRoleByNameAsync(roleName)) is Role role)
                {
                    var rolePermissions = await permissionService.GetRolePermissionsAsync(role.Id);
                    if (rolePermissions.Any(rolePermission => permissions.Contains(rolePermission)))
                    {
                        return;
                    }
                }
            }

            IEnumerable<string> permissionNames = user.FindAll(CustomClaimTypes.Permission)
                .Select(p => p.Value);

            if (permissionNames.Any(p => permissions.Contains(p)))
            {
                return;
            }

            context.Result = new ForbidResult();
        }
    }
}