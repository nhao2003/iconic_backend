
namespace Core.Entities;

public class ProductAttributeNavigator
{
    public long ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public long ProductAttributeId { get; set; }
    public ProductAttribute ProductAttribute { get; set; } = null!;
}