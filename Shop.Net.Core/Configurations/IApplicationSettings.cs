namespace Shop.Net.Core.Configurations;

public interface IApplicationSettings
{
    string GetConnectionString();
    string GetAuthenticationSecret();
}

// public class ApplicationSettings : IApplicationSettings
// {
//     private readonly IConfiguration configuration;

//     public AppSettings(IConfiguration configuration)
//     {
//         this.configuration = configuration;
//     }

//     public string GetConnectionString()
//     {
//         return configuration.GetConnectionString("DefaultConnection");
//     }

//     public string GetSecret()
//     {
//         return configuration["Secrets:SomeSecret"];
//     }
// }
