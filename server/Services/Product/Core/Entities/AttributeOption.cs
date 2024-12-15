namespace Core.Entities
{
    public class AttributeOption : BaseEntity
    {
        public int AttributeId { get; set; }
        public ProductAttribute Attribute { get; set; }

        public required string AttributeCode { get; set; }

        public required string OptionText { get; set; }

        public string? Description { get; set; }

        public List<ProductAttributeValueIndex> ProductAttributeValueIndexes { get; set; } = [];
    }
}
