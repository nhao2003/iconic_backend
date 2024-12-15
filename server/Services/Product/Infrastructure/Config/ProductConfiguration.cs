using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        builder.Property(p => p.DiscountedPrice).HasColumnType("decimal(18,2)");
        builder.Property(p => p.ImageUrl).IsRequired();
        builder.Property(p => p.Sku).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Barcode).HasMaxLength(100);
        builder.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(p => p.Attributes)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(p => p.Variants)
               .WithOne(v => v.Product)
               .HasForeignKey(v => v.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(p => p.ProductCustomOptions)
               .WithOne(v => v.Product)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
