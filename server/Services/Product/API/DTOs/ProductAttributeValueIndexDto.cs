namespace API.DTOs
{
    public class ProductAttributeValueIndexDto
    {
        public long Id { get; set; }
        public string? OptionText { get; set; }
        public long VariantId { get; set; }
        public long AttributeId { get; set; }
        public AttributeOptionDto? Option { get; set; }
    }

    public class CreateProductAttributeValueIndexDto
    {
        public string? OptionText { get; set; }
        public long VariantId { get; set; }
        public long AttributeId { get; set; }
        public long OptionId { get; set; }
    }

    public class UpdateProductAttributeValueIndexDto
    {
        public long Id { get; set; }

        public string? OptionText { get; set; }
        public long VariantId { get; set; }
        public long AttributeId { get; set; }
        public long OptionId { get; set; }
    }
}
