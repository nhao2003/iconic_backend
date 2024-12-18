using Core.Entities;

namespace API.DTOs
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public required string Slug { get; set; }

        public bool Status { get; set; }

        public bool IncludeInNav { get; set; }

        public short? Position { get; set; }

        public List<CategoryDescriptionDto> CategoryDescriptions { get; set; } = [];
    }

    public class CategoryDescriptionDto
    {
        public long Id { get; set; }
        public required string Name { get; set; }

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public required string UrlKey { get; set; }
    }

    public class CreateCategoryDto
    {
        public required string Slug { get; set; }

        public bool Status { get; set; }

        public bool IncludeInNav { get; set; }

        public short? Position { get; set; }

        public required string Name { get; set; }

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public required string UrlKey { get; set; }
    }

    public class UpdateCategoryDto
    {
        public long Id { get; set; }

        public required string Slug { get; set; }

        public bool Status { get; set; }

        public bool IncludeInNav { get; set; }

        public short? Position { get; set; }

        public required string Name { get; set; }

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public required string UrlKey { get; set; }
    }
}
