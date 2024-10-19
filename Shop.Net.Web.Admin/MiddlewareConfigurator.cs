using Microsoft.AspNetCore.Builder;

namespace Shop.Net.Web.Admin;

public static class MiddlewareConfigurator
{
    public static void ConfigureApplication(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}