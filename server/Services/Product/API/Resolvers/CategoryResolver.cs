using API.DTOs;
using API.RequestHelpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Specifications.Params;

namespace API.Resolvers;

public class CategoryResolver : BaseResolver
{
    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper;

    public CategoryResolver(IUnitOfWork unit, IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }

    public async Task<Pagination<CategoryDto>> GetCategories(CategorySpecParams specParams)
    {
        var spec = new CategorySpecification(specParams);
        return await CreatePagedResult<Category, CategoryDto>(
            _unit.Repository<Category>(),
            spec,
            specParams.PageIndex,
            specParams.PageSize,
            _mapper
        );
    }

    public async Task<CategoryDto> GetCategory(long id)
    {
        var spec = new CategorySpecification(id);
        var category = await _unit.Repository<Category>().GetEntityWithSpec(spec);
        if (category == null) return null;
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        var category = _mapper.Map<Category>(createCategoryDto);
        _unit.Repository<Category>().Add(category);

        if (!await _unit.Complete())
            throw new InvalidOperationException("Failed to save the category to the database.");

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto?> UpdateCategory(long id, UpdateCategoryDto updateCategoryDto)
    {
        if (updateCategoryDto.Id != id || !_unit.Repository<Category>().Exists(id))
            throw new ArgumentException("Invalid category ID.");

        var category = _mapper.Map<Category>(updateCategoryDto);
        _unit.Repository<Category>().Update(category);

        if (!await _unit.Complete())
            throw new InvalidOperationException("Failed to update the category.");

        var newCategory = await _unit.Repository<Category>().GetByIdAsync(id);

        return newCategory != null ? _mapper.Map<CategoryDto>(newCategory) : null;
    }

    public async Task<bool> DeleteCategory(long id)
    {
        var category = await _unit.Repository<Category>().GetByIdAsync(id);
        if (category == null) throw new Exception("The specified category does not exist.");

        _unit.Repository<Category>().Remove(category);

        if (!await _unit.Complete())
            throw new InvalidOperationException("Failed to delete the category.");

        return true;
    }
}
