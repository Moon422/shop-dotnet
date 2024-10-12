using Microsoft.EntityFrameworkCore;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Core.Domains.Directories;
using Shop.Net.Core.Domains.Inventories;

namespace Shop.Net.Data;

public class ShopDbContext : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<Division> StateProvinces { get; set; }
    public DbSet<District> Cities { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategoryMapping> ProductCategoryMappings { get; set; }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<CustomerRoleMapping> CustomerRoleMappings { get; set; }
    public DbSet<CustomerPermission> CustomerPermissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    public ShopDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.ThreeLetterCode)
                .IsFixedLength();

            entity.Property(e => e.TwoLetterCode)
                .IsFixedLength();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasMany(p => p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity<ProductCategoryMapping>(
                    r => r.HasOne(e => e.Category).WithMany().HasForeignKey(e => e.CategoryId).IsRequired(),
                    l => l.HasOne(e => e.Product).WithMany().HasForeignKey(e => e.ProductId).IsRequired()
                );
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.PhoneNumber)
                .IsFixedLength();

            entity.HasMany(c => c.Roles)
                .WithMany(r => r.Customers)
                .UsingEntity<CustomerRoleMapping>(r => r.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId).IsRequired(),
                    l => l.HasOne(e => e.Customer).WithMany().HasForeignKey(e => e.CustomerId).IsRequired());
        });

        base.OnModelCreating(modelBuilder);
    }
}
