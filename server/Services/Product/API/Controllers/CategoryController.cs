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

public class CategoryController(IUnitOfWork unit, IMapper _mapper) : BaseApiController
{
    [Cache(600)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories(
        [FromQuery] CategorySpecParams specParams)
    {
        var spec = new CategorySpecification(specParams);

        return await CreatePagedResult<Category, CategoryDto>(
            unit.Repository<Category>(),
            spec,
            specParams.PageIndex,
            specParams.PageSize, _mapper
        );
    }

    [Cache(600)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var spec = new CategorySpecification(id);

        var category = await unit.Repository<Category>().GetEntityWithSpec(spec);

        if (category == null) return NotFound();

        return APISuccessResponse(
            _mapper.Map<CategoryDto>(category),
            "Category retrieved successfully"
        );
    }

    [InvalidateCache("api/categories|")]
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(CreateCategoryDto createCategory)
    {
        var category = _mapper.Map<Category>(createCategory);

        unit.Repository<Category>().Add(category);

        if (await unit.Complete())
        {
            return CreatedAtAction("GetCategory", new { id = category.Id }, new APISucessResponse
            {
                StatusCode = HttpStatusCode.Created,
                Message = "Category created successfully",
                Data = _mapper.Map<CategoryDto>(category)
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
    public async Task<ActionResult> UpdateCategory(int id, UpdateCategoryDto updateCategory)
    {
        if (updateCategory.Id != id || !CategoryExists(id))
            return APIErrorResponse(
                Guid.NewGuid(),
                HttpStatusCode.BadRequest,
                "Cannot update this category",
                new List<string> { "Invalid category ID." });

        var category = _mapper.Map<Category>(updateCategory);

        unit.Repository<Category>().Update(category);

        if (await unit.Complete())
        {
            return APISuccessResponse(_mapper.Map<CategoryDto>(category), "Category updated successfully");
        }

        return APIErrorResponse(Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Problem deleting the category",
            new List<string> { "Failed to delete the category." });
    }

    [InvalidateCache("api/categories|")]
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var category = await unit.Repository<Category>().GetByIdAsync(id);

        if (category == null)
            return APIErrorResponse(
                Guid.NewGuid(),
                HttpStatusCode.NotFound,
                "Category not found",
                new List<string> { "The specified category does not exist." });

        unit.Repository<Category>().Remove(category);

        if (await unit.Complete())
        {
            return APISuccessResponse(id, "Category deleted successfully");
        }

        return APIErrorResponse(Guid.NewGuid(),
            HttpStatusCode.BadRequest,
            "Problem deleting the category",
            new List<string> { "Failed to delete the category." });
    }

    private bool CategoryExists(int id)
    {
        return unit.Repository<Category>().Exists(id);
    }
}