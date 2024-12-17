namespace Core.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public required string ImageUrl { get; set; }
    public List<string> ImageCoverUrls { get; set; }
    public required string Type { get; set; }
    public required string Brand { get; set; }
    public int QuantityInStock { get; set; }

    public bool Visibility { get; set; } = true;
    public required string Sku { get; set; }
    public string? Barcode { get; set; }
    public decimal? Weight { get; set; }
    public Category? Category { get; set; }
    public List<ProductAttributeNavigator> ProductAttributes { get; set; } = new();
    public List<Variant> Variants { get; set; } = new();
    public List<ProductCustomOption> ProductCustomOptions { get; set; } = [];
}
