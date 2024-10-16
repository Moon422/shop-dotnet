using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Net.Core.Configurations;
using Shop.Net.Data;
using Shop.Net.Services.Caching;
using Shop.Net.Services.Common;
using Shop.Net.Services.Customers;
using Shop.Net.Web.Admin.Configurations;
using Shop.Net.Web.Admin.Factories;
using Shop.Net.Web.Admin.Services;

namespace Shop.Net.Web.Admin;

public static class DependencyRegistrar
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        services.AddDbContext<ShopDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("mysql");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("Connection string missing");
            }

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
            options.UseMySql(connectionString, serverVersion);
        });

        var authenticationConfigurations = configuration.GetSection("Authentication");

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "auth";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(authenticationConfigurations?.GetValue<int?>("ExpireTimeSpan") ?? 30);
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
                options.SlidingExpiration = true;
            });

        services.AddAutoMapper(option => option.AddProfile<AutoMapperProfile>());

        services.AddSingleton<IApplicationSettings, ApplicationSettings>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ITransactionManager, TransactionManager>();

        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IWorkContext, WorkContext>();

        services.AddScoped<IAuthModelFactory, AuthModelFactory>();
        services.AddScoped<ICommonModelFactory, CommonModelFactory>();
    }

    public static void ConfigureApplication(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }

}