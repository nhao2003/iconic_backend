namespace API.DTOs
{
    public class ProductAttributeValueIndexDto
    {
        public int Id { get; set; }
        public string? OptionText { get; set; }
        public int VariantId { get; set; }
        public int AttributeId { get; set; }
        public AttributeOptionDto? Option { get; set; }
    }

    public class CreateProductAttributeValueIndexDto
    {
        public string? OptionText { get; set; }
        public int VariantId { get; set; }
        public int AttributeId { get; set; }
        public int OptionId { get; set; }
    }
}
