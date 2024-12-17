using API.DTOs;
using API.RequestHelpers;
using API.Resolvers;
using Core.Specifications.Params;

namespace API.Schemas.Queries;

[ExtendObjectType("Query")]
public class ProductQuery(ProductResolver resolver)
{
    public async Task<Pagination<ProductDto>> GetProducts(ProductSpecParams specParams, [Service] ProductResolver resolver)
    {
        return await resolver.GetProducts(specParams);
    }

    public async Task<ProductDto> GetProduct(int id, [Service] ProductResolver resolver)
    {
        var product = await resolver.GetProductById(id);

        if (product == null)
        {
            throw new GraphQLException(new Error("Product not found.", "PRODUCT_NOT_FOUND"));
        }
        return product;
    }
}