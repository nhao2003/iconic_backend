using API.DTOs;
using API.Resolvers;
using API.RequestHelpers;
using Core.Entities;
using Core.Specifications.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

public class AttributeController(AttributeResolver resolver) : BaseApiController
{
    [Cache(600)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductAttribute>>> GetAttributes([FromQuery] AttributeSpecParams specParams)
    {
        var attributes = await resolver.GetAttributes(specParams);

        return APISuccessResponse(attributes, "Attributes retrieved successfully");
    }

    [Cache(600)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductAttribute>> GetAttribute(int id)
    {
        var attribute = await resolver.GetAttributeById(id);

        if (attribute == null)
        {
            return APIErrorResponse(
                Guid.NewGuid(),
                HttpStatusCode.NotFound,
                "Attribute not found",
                new List<string> { "The specified attribute does not exist." }
            );
        }

        return APISuccessResponse(attribute, "Attribute retrieved successfully");
    }

    [InvalidateCache("api/attributes|")]
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ProductAttribute>> CreateAttribute(CreateAttributeDto createAttribute)
    {
        var attribute = await resolver.CreateAttribute(createAttribute);

        if (attribute != null)
        {
            return CreatedAtAction("GetAttribute", new { id = attribute.Id },
                new APISuccessResponse
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "Attribute created successfully",
                    Data = attribute
                });
        }

        return APIErrorResponse(
            Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Problem creating attribute",
            new List<string> { "Failed to save the attribute to the database." }
        );
    }

    [InvalidateCache("api/attributes|")]
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAttribute(int id, UpdateAttributeDto updateAttribute)
    {
        var attribute = await resolver.UpdateAttribute(id, updateAttribute);

        if (attribute != null)
        {
            return APISuccessResponse(attribute, "Attribute updated successfully");
        }

        return APIErrorResponse(
            Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Problem updating the attribute",
            new List<string> { "Failed to update the attribute." });
    }

    [InvalidateCache("api/attributes|")]
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAttribute(int id)
    {
        var result = await resolver.DeleteAttribute(id);

        if (result)
        {
            return APISuccessResponse(id, "Attribute deleted successfully");
        }

        return APIErrorResponse(
            Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Problem deleting the attribute",
            new List<string> { "Failed to delete the attribute." });
    }

    [Authorize(Roles = "Admin")]
    [InvalidateCache("api/attributes|")]
    [HttpPost("{attributeId:int}/options")]
    public async Task<ActionResult> AddAttributeOption(int attributeId, CreateAttributeOptionDto createOptionDto)
    {
        var option = await resolver.AddAttributeOption(attributeId, createOptionDto);

        if (option != null)
        {
            return APISuccessResponse(option, "Attribute option added successfully");
        }

        return APIErrorResponse(
            Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Problem adding the attribute option",
            new List<string> { "Failed to add the option to the database." });
    }

    [Authorize(Roles = "Admin")]
    [InvalidateCache("api/attributes|")]
    [HttpDelete("{attributeId:int}/options/{optionId:int}")]
    public async Task<ActionResult> RemoveAttributeOption(int attributeId, int optionId)
    {
        var result = await resolver.RemoveAttributeOption(attributeId, optionId);

        if (result)
        {
            return APISuccessResponse(optionId, "Attribute option removed successfully");
        }

        return APIErrorResponse(
            Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Problem removing the attribute option",
            new List<string> { "Failed to remove the option from the database." });
    }
}
