using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Services.Common;
using Shop.Net.Services.Customers;
using Shop.Net.Web.Admin.Helpers;

namespace Shop.Net.Web.Admin.Middlewares;

public class AuthRefreshMiddleware
{
    private readonly RequestDelegate next;
    // private readonly IWorkContext workContext;
    // private readonly AuthCookieRefreshHelper authCookieRefreshHelper;

    public AuthRefreshMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using var scope = context.RequestServices.CreateScope();

        var workContext = scope.ServiceProvider.GetRequiredService<IWorkContext>();
        if ((await workContext.GetActiveCustomerAsync()) is not Customer customer || !customer.RequireAuthRefresh)
        {
            await next(context);
            return;
        }

        var isPersistentValue = context.User.FindFirstValue(CustomClaimTypes.IsPersistent);
        var isPersistent = !string.IsNullOrWhiteSpace(isPersistentValue) && isPersistentValue == "true";

        var authCookieRefreshHelper = scope.ServiceProvider.GetRequiredService<AuthCookieRefreshHelper>();
        await authCookieRefreshHelper.RefreshCookie(customer, isPersistent);

        customer.RequireAuthRefresh = false;
        var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();
        await customerService.UpdateCustomerAsync(customer);

        await next(context);
    }
}
