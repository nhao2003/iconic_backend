namespace Core.Entities
{
    public class Variant : BaseEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public string Sku { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public List<ProductAttributeValueIndex> AttributeValues { get; set; } = new();
    }
}
