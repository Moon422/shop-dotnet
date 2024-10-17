namespace Shop.Net.Core.Configurations;

public interface IApplicationConfig
{
    string? GetConnectionString();
    string? GetAuthenticationSecret();
}
