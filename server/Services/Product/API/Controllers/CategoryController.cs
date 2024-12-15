using API.DTOs;
using API.RequestHelpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Specifications.Params;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CategoryController(IUnitOfWork unit, IMapper _mapper) : BaseApiController
{
    [Cache(600)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories(
        [FromQuery] CategorySpecParams specParams)
    {
        var spec = new CategorySpecification(specParams);

        //return await CreatePagedResult(unit.Repository<Category>(), spec, specParams.PageIndex, specParams.PageSize);
        return await CreatePagedResult<Category, CategoryDto>(unit.Repository<Category>(), spec, specParams.PageIndex,
            specParams.PageSize, _mapper);
    }

    [Cache(600)]
    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await unit.Repository<Product>().GetByIdAsync(id);

        if (product == null) return NotFound();

        return product;
    }
}