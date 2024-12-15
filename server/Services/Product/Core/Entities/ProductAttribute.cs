namespace Core.Entities
{
    public class ProductAttribute : BaseEntity
    {
        public required string AttributeCode { get; set; }

        public required string AttributeName { get; set; }

        public required string Type { get; set; }

        public bool IsRequired { get; set; } = false;

        public bool DisplayOnFrontend { get; set; } = false;

        public int SortOrder { get; set; } = 0;

        public bool IsFilterable { get; set; } = false;

        public List<AttributeOption> AttributeOptions { get; set; } = [];

        public List<ProductAttributeValueIndex> ProductAttributeValueIndexes { get; set; } = [];
    }
}
