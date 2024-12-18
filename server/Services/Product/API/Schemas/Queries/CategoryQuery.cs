using API.DTOs;
using API.RequestHelpers;
using API.Resolvers;
using Core.Specifications.Params;

namespace API.Schemas.Queries;

[ExtendObjectType("Query")]
public class CategoryQuery
{
    public async Task<Pagination<CategoryDto>> GetCategorys(CategorySpecParams specParams, [Service] CategoryResolver resolver)
    {
        return await resolver.GetCategories(specParams);
    }

    public async Task<CategoryDto> GetCategory(int id, [Service] CategoryResolver resolver)
    {
        var category = await resolver.GetCategory(id);

        if (category == null)
        {
            throw new GraphQLException(new Error("category not found.", "CATEGORY_NOT_FOUND"));
        }

        return category;
    }
}