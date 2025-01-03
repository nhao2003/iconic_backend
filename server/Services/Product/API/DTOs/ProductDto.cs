﻿using Core.Entities;

namespace API.DTOs
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public string ImageUrl { get; set; }
        public List<string> ImageCoverUrls { get; set; } = new List<string>();
        public string? Specifications { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int QuantityInStock { get; set; }
        public bool Visibility { get; set; } = true;
        public required string Sku { get; set; }
        public string? Barcode { get; set; }
        public decimal? Weight { get; set; }

        public CategoryDto? Category { get; set; }

        public List<ProductAttributeNavigatorDto> ProductAttributes { get; set; } = new();

        public List<VariantDto> Variants { get; set; }

        public List<ProductCustomOptionDto> ProductCustomOptions { get; set; } = [];
    }
}
