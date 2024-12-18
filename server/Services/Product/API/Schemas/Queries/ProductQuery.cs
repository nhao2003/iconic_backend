using API.DTOs;
using API.RequestHelpers;
using API.Resolvers;
using Core.Specifications.Params;
using Microsoft.AspNetCore.Mvc;

namespace API.Schemas.Queries;

[ExtendObjectType("Query")]
public class ProductQuery(ProductResolver resolver)
{
    public async Task<Pagination<ProductDto>> GetProducts(ProductSpecParams specParams, [Service] ProductResolver resolver)
    {
        return await resolver.GetProducts(specParams);
    }

    public async Task<ProductDto> GetProduct(long id, [Service] ProductResolver resolver)
    {
        var product = await resolver.GetProductById(id);

        if (product == null)
        {
            throw new GraphQLException(new Error("Product not found.", "PRODUCT_NOT_FOUND"));
        }
        return product;
    }

    public async Task<IReadOnlyList<string>> GetBrands([Service] ProductResolver resolver)
    {
        return await resolver.GetBrands();
    }

    public async Task<IReadOnlyList<string>> GetTypes([Service] ProductResolver resolver)
    {
        return await resolver.GetTypes();
    }
}