using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shop.Net.Core;
using Shop.Net.Core.Configurations;
using Shop.Net.Core.Settings.Attributes;
using Shop.Net.Data;
using Shop.Net.Web.Admin.Configurations;

namespace Shop.Net.Web.Admin;

public static class DependencyRegistrar
{
    private static void ConfigureSettings(IServiceCollection services, IConfiguration configuration)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        var settingsTypes = assembly.GetTypes()
            .Where(t => t.IsClass && t.GetCustomAttribute<SettingsAttribute>() is not null)
            .ToList();

        settingsTypes.AddRange(assembly.GetReferencedAssemblies()
            .SelectMany(asm => Assembly.Load(asm).GetTypes().Where(t => t.IsClass && t.GetCustomAttribute<SettingsAttribute>() is not null)));

        foreach (var settingsType in settingsTypes)
        {
            if (settingsType.GetCustomAttribute<SettingsAttribute>() is not SettingsAttribute sa)
            {
                continue;
            }

            var directory = sa.Directory;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var filePath = sa.FullPath;
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            var settingsContent = File.ReadAllText(filePath);
            var settings = JsonConvert.DeserializeObject(settingsContent, settingsType) ?? Activator.CreateInstance(settingsType);

            if (settings is null)
            {
                continue;
            }

            services.AddSingleton(settingsType, settings);
        }
    }

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

        ConfigureSettings(services, configuration);

        services.AddAutoMapper(option => option.AddProfile<AutoMapperProfile>());

        services.AddSingleton<IApplicationConfig, ApplicationConfig>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        Assembly assembly = Assembly.GetExecutingAssembly();
        List<Type> scopedDependencyTypes = assembly.GetTypes()
            .Where(t => t.IsClass && t.GetCustomAttribute<ScopeDependencyAttribute>() is not null)
            .ToList();

        scopedDependencyTypes.AddRange(assembly.GetReferencedAssemblies()
            .SelectMany(asm => Assembly.Load(asm).GetTypes()
                .Where(t => t.IsClass && t.GetCustomAttribute<ScopeDependencyAttribute>() is not null)));

        foreach (var scopedDependencyType in scopedDependencyTypes)
        {
            if (scopedDependencyType.GetCustomAttribute<ScopeDependencyAttribute>() is ScopeDependencyAttribute sda)
            {
                services.AddScoped(sda.InterfaceType, scopedDependencyType);
            }
        }

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
    }
}
