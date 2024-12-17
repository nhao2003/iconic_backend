using API.DTOs;
using API.RequestHelpers;
using API.Resolvers;
using Core.Entities;
using Core.Specifications.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

public class ProductsController(ProductResolver resolver) : BaseApiController
{
    //[Cache(600)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(
        [FromQuery] ProductSpecParams specParams)
    {
        var pagination = await resolver.GetProducts(specParams);

        return APISuccessResponse(pagination, "Data retrieved successfully");
    }

    [Cache(600)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await resolver.GetProductById(id);

        if (product == null) return APIErrorResponse(
            Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Product not Found",
            new List<string> { "Failed when get the product by Id from the database." }
        );

        return APISuccessResponse(
            product,
            "Product retrieved successfully"
        );
    }

    [InvalidateCache("api/products|")]
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(CreateProductDto createProduct)
    {
        var product = await resolver.CreateProduct(createProduct);

        if (product != null)
        {
            return CreatedAtAction("GetProduct", new { id = product.Id },
                new APISuccessResponse
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "Product created successfully",
                    Data = product
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
        var product = await resolver.UpdateProduct(id, updateProduct);

        if (product != null)
        {
            return CreatedAtAction("GetProduct", new { id = product.Id },
                new APISuccessResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Product updated successfully",
                    Data = product
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
        var product = await resolver.DeleteProduct(id);
        if (product != null)
        {
            return CreatedAtAction("DeleteProduct", new { id = product.Id },
            new APISuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Product Deleted successfully",
                Data = product
            });
        }

        return APIErrorResponse(
            Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Problem deleting the product",
            new List<string> { "Failed to delete the product from the database." }
        );
    }

    [Cache(10000)]
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var brands = await resolver.GetBrands();

        return APISuccessResponse(brands, "Get Brands successfully");
    }

    [Cache(10000)]
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var types = await resolver.GetTypes();

        return APISuccessResponse(types, "Get Types successfully");
    }
}
