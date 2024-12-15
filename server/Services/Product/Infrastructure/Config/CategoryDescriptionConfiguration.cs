using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Config;

public class CategoryDescriptionConfiguration : IEntityTypeConfiguration<CategoryDescription>
{
    public void Configure(EntityTypeBuilder<CategoryDescription> builder)
    {
        builder.Property(cd => cd.Name).IsRequired().HasMaxLength(100);
        builder.Property(cd => cd.UrlKey).IsRequired().HasMaxLength(150);
        builder.HasOne(cd => cd.Category)
               .WithMany(c => c.CategoryDescriptions)
               .OnDelete(DeleteBehavior.Cascade);
    }
}