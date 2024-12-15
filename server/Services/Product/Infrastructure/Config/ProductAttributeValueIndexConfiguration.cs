using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Config;

public class ProductAttributeValueIndexConfiguration : IEntityTypeConfiguration<ProductAttributeValueIndex>
{
    public void Configure(EntityTypeBuilder<ProductAttributeValueIndex> builder)
    {
        builder.HasOne(pavi => pavi.Attribute)
               .WithMany(a => a.ProductAttributeValueIndexes)
               .HasForeignKey(pavi => pavi.AttributeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pavi => pavi.Variant)
               .WithMany(v => v.AttributeValues)
               .HasForeignKey(pavi => pavi.VariantId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pavi => pavi.Option)
               .WithMany(o => o.ProductAttributeValueIndexes)
               .OnDelete(DeleteBehavior.SetNull);
    }
}