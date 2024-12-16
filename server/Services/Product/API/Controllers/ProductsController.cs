using API.DTOs;
using API.RequestHelpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Specifications.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

public class ProductsController(IUnitOfWork unit, IMapper _mapper) : BaseApiController
{
    [Cache(600)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(
        [FromQuery] ProductSpecParams specParams)
    {
        var spec = new ProductSpecification(specParams);

        return await CreatePagedResult<Product, ProductDto>(unit.Repository<Product>(), spec, specParams.PageIndex, specParams.PageSize, _mapper);
    }

    [Cache(600)]
    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var spec = new ProductSpecification(id);

        var product = await unit.Repository<Product>().GetEntityWithSpec(spec);

        if (product == null) return NotFound();

        return APISuccessResponse(
            _mapper.Map<ProductDto>(product),
            "Product retrieved successfully"
        );
    }

    [InvalidateCache("api/products|")]
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(CreateProductDto createProduct)
    {
        var product = _mapper.Map<Product>(createProduct);

        // get category by Id
        var category = await unit.Repository<Category>().GetByIdAsync(createProduct.CategoryId);
        if (category != null)
        {
            product.Category = category;
        }

        // get Attributes by Ids
        var attributes = new List<ProductAttribute>();
        foreach (var attributeId in createProduct.AttributeIds)
        {
            var attr = await unit.Repository<ProductAttribute>().GetByIdAsync(attributeId);
            if (attr != null)
            {
                attributes.Add(attr);
            }
        }
        // Populate the ProductAttributes collection
        product.ProductAttributes = attributes.Select(attr => new ProductAttributeNavigator
        {
            Product = product,
            ProductAttribute = attr
        }).ToList();

        // get AttributeOptions by Ids
        var attributeOptionMap = new Dictionary<int, AttributeOption>();
        foreach (var variant in createProduct.Variants)
        {
            foreach (var attribute in variant.AttributeValues)
            {
                if (!attributeOptionMap.ContainsKey(attribute.OptionId))
                {
                    var option = await unit.Repository<AttributeOption>().GetByIdAsync(attribute.OptionId);
                    if (option != null)
                    {
                        attributeOptionMap[attribute.AttributeId] = option;
                    }
                }
            }
        }

        foreach (var productVariant in product.Variants)
        {
            foreach (var productAttributeValue in productVariant.AttributeValues)
            {
                if (attributeOptionMap.TryGetValue(productAttributeValue.AttributeId, out var option))
                {
                    productAttributeValue.Option = option;
                }
            }
        }

        unit.Repository<Product>().Add(product);

        if (await unit.Complete())
        {
            return CreatedAtAction("GetProduct", new { id = product.Id },
                new APISuccessResponse
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "Product created successfully",
                    Data = _mapper.Map<ProductDto>(product)
                }
            );
        }

        return APIErrorResponse(
            Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Problem creating product",
            new List<string> { "Failed to save the product to the database." }
        );
    }

    [InvalidateCache("api/products|")]
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, UpdateProductDto updateProduct)
    {
        if (updateProduct.Id != id || !ProductExists(id))
            return BadRequest("Cannot update this product");

        var product = _mapper.Map<Product>(updateProduct);

        // get category by Id
        var category = await unit.Repository<Category>().GetByIdAsync(updateProduct.CategoryId);
        if (category != null)
        {
            product.Category = category;
        }

        // get Attributes by Ids
        var attributes = new List<ProductAttribute>();
        foreach (var attributeId in updateProduct.AttributeIds)
        {
            var attr = await unit.Repository<ProductAttribute>().GetByIdAsync(attributeId);
            if (attr != null)
            {
                attributes.Add(attr);
            }
        }
        // Populate the ProductAttributes collection
        product.ProductAttributes = attributes.Select(attr => new ProductAttributeNavigator
        {
            Product = product,
            ProductAttribute = attr
        }).ToList();

        // get AttributeOptions by Ids
        var attributeOptionMap = new Dictionary<int, AttributeOption>();
        foreach (var variant in updateProduct.Variants)
        {
            foreach (var attribute in variant.AttributeValues)
            {
                if (!attributeOptionMap.ContainsKey(attribute.OptionId))
                {
                    var option = await unit.Repository<AttributeOption>().GetByIdAsync(attribute.OptionId);
                    if (option != null)
                    {
                        attributeOptionMap[attribute.AttributeId] = option;
                    }
                }
            }
        }

        foreach (var productVariant in product.Variants)
        {
            foreach (var productAttributeValue in productVariant.AttributeValues)
            {
                if (attributeOptionMap.TryGetValue(productAttributeValue.AttributeId, out var option))
                {
                    productAttributeValue.Option = option;
                }
            }
        }

        unit.Repository<Product>().Update(product);

        if (await unit.Complete())
        {
            return CreatedAtAction("GetProduct", new { id = product.Id },
            new APISuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Product updated successfully",
                Data = _mapper.Map<ProductDto>(product)
            }
        );

        }

        return APIErrorResponse(
            Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Problem updating product",
            new List<string> { "Failed to save the product to the database." }
        );
    }

    [InvalidateCache("api/products|")]
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await unit.Repository<Product>().GetByIdAsync(id);

        if (product == null) return NotFound();

        unit.Repository<Product>().Remove(product);

        if (await unit.Complete())
        {
            return NoContent();
        }

        return BadRequest("Problem deleting the product");
    }

    [Cache(10000)]
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();

        return Ok(await unit.Repository<Product>().ListAsync(spec));
    }

    [Cache(10000)]
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpecification();

        return Ok(await unit.Repository<Product>().ListAsync(spec));
    }

    private bool ProductExists(int id)
    {
        return unit.Repository<Product>().Exists(id);
    }
}
