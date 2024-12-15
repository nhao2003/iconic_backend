using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Config;

public class AttributeOptionConfiguration : IEntityTypeConfiguration<AttributeOption>
{
    public void Configure(EntityTypeBuilder<AttributeOption> builder)
    {
        builder.Property(ao => ao.AttributeCode).IsRequired().HasMaxLength(50);
        builder.Property(ao => ao.OptionText).IsRequired().HasMaxLength(100);
        builder.HasOne(ao => ao.Attribute)
               .WithMany(a => a.AttributeOptions)
               .HasForeignKey(ao => ao.AttributeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
