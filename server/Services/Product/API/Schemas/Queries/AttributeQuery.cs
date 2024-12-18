using API.DTOs;
using API.RequestHelpers;
using API.Resolvers;
using Core.Specifications.Params;

namespace API.Schemas.Queries;

[ExtendObjectType("Query")]
public class AttributeQuery()
{
    public async Task<Pagination<AttributeDto>> GetAttributes(AttributeSpecParams specParams, [Service] AttributeResolver resolver)
    {
        return await resolver.GetAttributes(specParams);
    }

    public async Task<AttributeDto> GetAttribute(int id, [Service] AttributeResolver resolver)
    {
        var attribute = await resolver.GetAttributeById(id);

        if (attribute == null)
        {
            throw new GraphQLException(new Error("attribute not found.", "ATTRIBUTE_NOT_FOUND"));
        }

        return attribute;
    }
}