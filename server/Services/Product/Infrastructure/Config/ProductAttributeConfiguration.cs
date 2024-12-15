using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Config;

public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
{
    public void Configure(EntityTypeBuilder<ProductAttribute> builder)
    {
        builder.Property(pa => pa.AttributeCode).IsRequired().HasMaxLength(50);
        builder.Property(pa => pa.AttributeName).IsRequired().HasMaxLength(100);
        builder.Property(pa => pa.Type).IsRequired().HasMaxLength(50);
        builder.HasMany(pa => pa.AttributeOptions)
               .WithOne(ao => ao.Attribute)
               .HasForeignKey(ao => ao.AttributeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}