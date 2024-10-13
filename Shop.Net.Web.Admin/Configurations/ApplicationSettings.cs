using Microsoft.Extensions.Configuration;
using Shop.Net.Core.Configurations;

namespace Shop.Net.Web.Admin.Configurations;

public class ApplicationSettings : IApplicationSettings
{
    protected readonly IConfiguration configuration;

    public ApplicationSettings(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string? GetAuthenticationSecret()
    {
        return configuration.GetSection("AuthSecret").Value;
    }

    public string? GetConnectionString()
    {
        return configuration.GetConnectionString("mysql");
    }
}