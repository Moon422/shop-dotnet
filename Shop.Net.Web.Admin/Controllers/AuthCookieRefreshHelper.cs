using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Services.Customers;

namespace Shop.Net.Web.Admin.Configurations;

public class AuthCookieRefreshHelper
{
    protected readonly IRoleService roleService;
    protected readonly IPermissionService permissionService;
    protected readonly IHttpContextAccessor httpContextAccessor;

    public AuthCookieRefreshHelper(IRoleService roleService,
        IPermissionService permissionService,
        IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.roleService = roleService;
        this.permissionService = permissionService;
    }

    public async Task RefreshCookie(Customer customer, bool isPersistent)
    {
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentNullException.ThrowIfNull(httpContextAccessor.HttpContext);

        IList<Role> customerRoles = await roleService.GetCustomerRolesAsync(customer.Id);
        IList<string> customerPermissions = await permissionService.GetCustomerPermissionsAsync(customer.Id);

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, customer.Email)
        };

        foreach (var customerRole in customerRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, customerRole.Name));
        }

        foreach (var customerPermission in customerPermissions)
        {
            claims.Add(new Claim(CustomClaimTypes.Permission, customerPermission));
        }

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        AuthenticationProperties authProperties = new AuthenticationProperties
        {
            IsPersistent = isPersistent
        };

        await httpContextAccessor.HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);
    }
}
