namespace Core.Entities;

public class CategoryDescription : BaseEntity
{
    public required string Name { get; set; }
    public string? ShortDescription { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public required string UrlKey { get; set; }
    public Category Category { get; set; }
}