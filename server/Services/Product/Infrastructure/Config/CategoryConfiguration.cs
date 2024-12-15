using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Core.Enums;

namespace Infrastructure.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Slug).IsRequired().HasMaxLength(100);
            builder.HasMany(c => c.CategoryDescriptions)
                   .WithOne(cd => cd.Category)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(c => c.Products)
                   .WithOne(p => p.Category)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(c => c.ParentCategory)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);
            builder.Property(c => c.ParentStatus)
                   .HasConversion(
                       ps => ps.ToString(),
                       ps => (ParentStatus)Enum.Parse(typeof(ParentStatus), ps)
                   );
        }
    }
}
