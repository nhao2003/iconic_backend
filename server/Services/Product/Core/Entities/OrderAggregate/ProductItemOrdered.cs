namespace Core.Entities.OrderAggregate;

public class ProductItemOrdered
{
    public long ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string PictureUrl { get; set; }
}
