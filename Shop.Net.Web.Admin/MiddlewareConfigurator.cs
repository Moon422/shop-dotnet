using Microsoft.AspNetCore.Builder;
using Shop.Net.Web.Admin.Middlewares;

namespace Shop.Net.Web.Admin;

public static class MiddlewareConfigurator
{
    public static void ConfigureApplication(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<ThemeToggleMiddleware>();
        app.UseMiddleware<AuthRefreshMiddleware>();
    }
}