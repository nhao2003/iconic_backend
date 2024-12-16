using Core.Enums;

namespace Core.Entities;

public class Category : BaseEntity
{
    public required string Slug { get; set; }
    public bool Status { get; set; }
    public bool IncludeInNav { get; set; }
    public short? Position { get; set; }
    public List<CategoryDescription> CategoryDescriptions { get; set; }
    public List<Product> Products { get; set; }
    public Category? ParentCategory { get; set; }
    public ParentStatus ParentStatus { get; set; } = ParentStatus.Published;
}