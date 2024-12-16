using API.DTOs;
using API.RequestHelpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications.Params;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace API.Controllers
{
    public class AttributeController(IUnitOfWork unit, IMapper _mapper) : BaseApiController
    {
        [Cache(600)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductAttribute>>> GetAttributes(
        [FromQuery] AttributeSpecParams specParams)
        {
            var spec = new AttributeSpecification(specParams);

            return await CreatePagedResult<ProductAttribute, AttributeDto>(
                unit.Repository<ProductAttribute>(),
                spec,
                specParams.PageIndex,
                specParams.PageSize, _mapper
            );
        }

        [Cache(600)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductAttribute>> GetAttribute(int id)
        {
            var spec = new AttributeSpecification(id);

            var attribute = await unit.Repository<ProductAttribute>().GetEntityWithSpec(spec);

            if (attribute == null) return NotFound();

            return APISuccessResponse(
                _mapper.Map<AttributeDto>(attribute),
                "Attribute retrieved successfully"
            );
        }

        [InvalidateCache("api/attribute|")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductAttribute>> CreateAttribute(CreateAttributeDto createAttribute)
        {
            var attribute = _mapper.Map<ProductAttribute>(createAttribute);

            unit.Repository<ProductAttribute>().Add(attribute);

            if (await unit.Complete())
            {
                return CreatedAtAction("GetAttribute", new { id = attribute.Id }, new APISucessResponse
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "Attribute created successfully",
                    Data = _mapper.Map<AttributeDto>(attribute)
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
            if (updateAttribute.Id != id || !AttributeExists(id))
                return APIErrorResponse(
                    Guid.NewGuid(),
                    HttpStatusCode.BadRequest,
                    "Cannot update this attribute",
                    new List<string> { "Invalid attribute ID." });

            var attribute = _mapper.Map<ProductAttribute>(updateAttribute);

            unit.Repository<ProductAttribute>().Update(attribute);

            if (await unit.Complete())
            {
                var newAttribute = await unit.Repository<ProductAttribute>().GetByIdAsync(id);

                if (newAttribute == null) return NotFound();

                return APISuccessResponse(_mapper.Map<AttributeDto>(newAttribute), "Attribute updated successfully");
            }

            return APIErrorResponse(Guid.NewGuid(),
                HttpStatusCode.BadRequest,
                "Problem deleting the attribute",
                new List<string> { "Failed to delete the attribute." });
        }

        [InvalidateCache("api/attributes|")]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAttribute(int id)
        {
            var attribute = await unit.Repository<ProductAttribute>().GetByIdAsync(id);

            if (attribute == null)
                return APIErrorResponse(
                    Guid.NewGuid(),
                    HttpStatusCode.NotFound,
                    "Attribute not found",
                    new List<string> { "The specified attribute does not exist." });

            unit.Repository<ProductAttribute>().Remove(attribute);

            if (await unit.Complete())
            {
                return APISuccessResponse(id, "Attribute deleted successfully");
            }

            return APIErrorResponse(Guid.NewGuid(),
                HttpStatusCode.BadRequest,
                "Problem deleting the attribute",
                new List<string> { "Failed to delete the attribute." });
        }

        private bool AttributeExists(int id)
        {
            return unit.Repository<ProductAttribute>().Exists(id);
        }

        [Authorize(Roles = "Admin")]
        [InvalidateCache("api/attributes|")]
        [HttpPost("{attributeId:int}/options")]
        public async Task<ActionResult> AddAttributeOption(int attributeId, CreateAttributeOptionDto createOptionDto)
        {
            var attribute = await unit.Repository<ProductAttribute>().GetByIdAsync(attributeId);

            if (attribute == null)
                return APIErrorResponse(
                    Guid.NewGuid(),
                    HttpStatusCode.NotFound,
                    "Attribute not found",
                    new List<string> { "The specified attribute does not exist." });

            var option = _mapper.Map<AttributeOption>(createOptionDto);
            option.AttributeId = attributeId;
            option.AttributeCode = attribute.AttributeCode;

            attribute.AttributeOptions.Add(option);

            unit.Repository<ProductAttribute>().Update(attribute);

            if (await unit.Complete())
            {
                return APISuccessResponse(
                    _mapper.Map<AttributeOptionDto>(option),
                    "Attribute option added successfully"
                );
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
            var attribute = await unit.Repository<ProductAttribute>().GetByIdAsync(attributeId);

            if (attribute == null)
                return APIErrorResponse(
                    Guid.NewGuid(),
                    HttpStatusCode.NotFound,
                    "Attribute not found",
                    new List<string> { "The specified attribute does not exist." });

            var option = attribute.AttributeOptions.FirstOrDefault(o => o.Id == optionId);

            if (option == null)
                return APIErrorResponse(
                    Guid.NewGuid(),
                    HttpStatusCode.NotFound,
                    "Option not found",
                    new List<string> { "The specified option does not exist." });

            attribute.AttributeOptions.Remove(option);

            unit.Repository<ProductAttribute>().Update(attribute);

            if (await unit.Complete())
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
}
