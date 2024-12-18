using Core.Entities;

namespace API.DTOs
{
    public class VariantDto
    {
        public long Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public List<ProductAttributeValueIndexDto> AttributeValues { get; set; } = new();
    }

    public class CreateVariantDto
    {
        public string Sku { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public List<CreateProductAttributeValueIndexDto> AttributeValues { get; set; } = new();
    }

    public class UpdateVariantDto
    {
        public long Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public List<CreateProductAttributeValueIndexDto> AttributeValues { get; set; } = new();
    }
}
