using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Shop.Net.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ShopDbContext>
{
    public ShopDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ShopDbContext>();

        var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
        optionsBuilder.UseMySql("server=localhost;user=shopdb_user;password=shopdb;database=shopnetdb", serverVersion);

        return new ShopDbContext(optionsBuilder.Options);
    }
}
