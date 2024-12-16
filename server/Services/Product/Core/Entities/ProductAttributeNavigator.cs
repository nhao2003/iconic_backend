
namespace Core.Entities;

public class ProductAttributeNavigator
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int ProductAttributeId { get; set; }
    public ProductAttribute ProductAttribute { get; set; } = null!;
}