using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Config;

public class ProductAttributeNavigatorConfiguration : IEntityTypeConfiguration<ProductAttributeNavigator>
{
    public void Configure(EntityTypeBuilder<ProductAttributeNavigator> builder)
    {
        builder.HasKey(pan => new { pan.ProductId, pan.ProductAttributeId });

        builder.HasOne(pan => pan.Product)
               .WithMany(p => p.ProductAttributes)
               .HasForeignKey(pan => pan.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pan => pan.ProductAttribute)
               .WithMany(pa => pa.ProductAttributes)
               .HasForeignKey(pan => pan.ProductAttributeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
