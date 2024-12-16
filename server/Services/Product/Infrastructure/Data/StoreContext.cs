using Core.Entities;
using Core.Entities.OrderAggregate;
using Infrastructure.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }
    public DbSet<ProductAttributeValueIndex> ProductAttributeValueIndexes { get; set; }
    public DbSet<AttributeOption> AttributeOptions { get; set; }
    public DbSet<Variant> Variants { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryDescription> CategoryDescriptions { get; set; }
    public DbSet<ProductCustomOption> ProductCustomOptions { get; set; }
    public DbSet<ProductCustomOptionValue> ProductCustomOptionValues { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ProductAttributeNavigator> ProductAttributeNavigators { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}
