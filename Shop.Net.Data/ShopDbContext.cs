using Microsoft.EntityFrameworkCore;
using Shop.Net.Core.Domains.Directories;
using Shop.Net.Core.Domains.Inventories;

namespace Shop.Net.Data;

public class ShopDbContext : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<StateProvince> StateProvinces { get; set; }
    public DbSet<City> Cities { get; set; }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategoryMapping> ProductCategoryMappings { get; set; }

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

        base.OnModelCreating(modelBuilder);
    }
}
