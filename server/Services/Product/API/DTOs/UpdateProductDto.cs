using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class UpdateProductDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    public decimal? DiscountedPrice { get; set; }

    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    public string Type { get; set; } = string.Empty;

    [Required]
    public string Brand { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Quantity in stock must be at least 1")]
    public int QuantityInStock { get; set; }

    public bool Visibility { get; set; } = true;

    [Required]
    public string Sku { get; set; } = string.Empty;

    [Required]
    public string Barcode { get; set; } = string.Empty;

    public decimal? Weight { get; set; }

    public int CategoryId { get; set; }

    public List<int> AttributeIds { get; set; } = new List<int>();

    public List<UpdateVariantDto> Variants { get; set; } = new List<UpdateVariantDto> { };

    public List<ProductCustomOptionDto> ProductCustomOptions { get; set; } = new List<ProductCustomOptionDto>();
}
