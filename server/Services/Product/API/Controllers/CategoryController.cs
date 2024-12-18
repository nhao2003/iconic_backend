using API.DTOs;
using API.RequestHelpers;
using API.Resolvers;
using Core.Entities;
using Core.Specifications.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

public class CategoryController(CategoryResolver resolver) : BaseApiController
{
    [Cache(600)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories(
        [FromQuery] CategorySpecParams specParams)
    {
        var pagination = await resolver.GetCategories(specParams);

        return APISuccessResponse(pagination, "Data retrieved successfully");
    }

    [Cache(600)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Category>> GetCategory(long id)
    {
        var category = await resolver.GetCategory(id);

        if (category == null) return APIErrorResponse(
            Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Category not Found",
            new List<string> { "Failed when get the category by Id from the database." }
        );

        return APISuccessResponse(
            category,
            "Category retrieved successfully"
        );
    }

    [InvalidateCache("api/categories|")]
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(CreateCategoryDto createCategory)
    {
        var category = await resolver.CreateCategory(createCategory);

        if (category != null)
        {
            return CreatedAtAction("GetCategory", new { id = category.Id }, new APISuccessResponse
            {
                StatusCode = HttpStatusCode.Created,
                Message = "Category created successfully",
                Data = category
            });
        }

        return APIErrorResponse(
            Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Problem creating category",
            new List<string> { "Failed to save the category to the database." }
        );
    }

    [InvalidateCache("api/categories|")]
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateCategory(long id, UpdateCategoryDto updateCategory)
    {
        var category = await resolver.UpdateCategory(id, updateCategory);

        if (category != null)
        {
            return APISuccessResponse(category, "Category updated successfully");
        }

        return APIErrorResponse(Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Problem deleting the category",
            new List<string> { "Failed to delete the category." });
    }

    [InvalidateCache("api/categories|")]
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCategory(long id)
    {
        var result = await resolver.DeleteCategory(id);
        if (result)
        {
            return APISuccessResponse(id, "Category deleted successfully");
        }

        return APIErrorResponse(Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Problem deleting the category",
            new List<string> { "Failed to delete the category." });
    }
}