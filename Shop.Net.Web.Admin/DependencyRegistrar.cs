using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Net.Core.Configurations;
using Shop.Net.Data;
using Shop.Net.Services.Caching;
using Shop.Net.Services.Customers;
using Shop.Net.Web.Admin.Configurations;

namespace Shop.Net.Web.Admin;

public static class DependencyRegistrar
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IApplicationSettings, ApplicationSettings>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ITransactionManager, TransactionManager>();

        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IPasswordService, PasswordService>();
    }
}