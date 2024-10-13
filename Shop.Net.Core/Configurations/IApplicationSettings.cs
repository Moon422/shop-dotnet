namespace Shop.Net.Core.Configurations;

public interface IApplicationSettings
{
    string? GetConnectionString();
    string? GetAuthenticationSecret();
}
