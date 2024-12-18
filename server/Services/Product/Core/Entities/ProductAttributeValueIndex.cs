namespace Core.Entities;

public class ProductAttributeValueIndex : BaseEntity
{
    public string? OptionText { get; set; }

    public long VariantId { get; set; }

    public Variant Variant { get; set; } = null!;

    public long AttributeId { get; set; }

    public ProductAttribute Attribute { get; set; } = null!;

    public AttributeOption? Option { get; set; }
}