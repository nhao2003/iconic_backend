using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications.Params;
using Core.Specifications;
using API.RequestHelpers;

namespace API.Resolvers;

public class AttributeResolver : BaseResolver
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AttributeResolver(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<AttributeDto>> GetAttributes(AttributeSpecParams specParams)
    {
        var spec = new AttributeSpecification(specParams);
        return await CreatePagedResult<ProductAttribute, AttributeDto>(
            _unitOfWork.Repository<ProductAttribute>(), spec, specParams.PageIndex, specParams.PageSize, _mapper);
    }

    public async Task<AttributeDto?> GetAttributeById(long id)
    {
        var spec = new AttributeSpecification(id);
        var attribute = await _unitOfWork.Repository<ProductAttribute>().GetEntityWithSpec(spec);
        return attribute == null ? null : _mapper.Map<AttributeDto>(attribute);
    }

    public async Task<AttributeDto?> CreateAttribute(CreateAttributeDto createAttribute)
    {
        var attribute = _mapper.Map<ProductAttribute>(createAttribute);
        _unitOfWork.Repository<ProductAttribute>().Add(attribute);

        if (await _unitOfWork.Complete())
        {
            return _mapper.Map<AttributeDto>(attribute);
        }
        return null;
    }

    public async Task<AttributeDto?> UpdateAttribute(long id, UpdateAttributeDto updateAttribute)
    {
        if (updateAttribute.Id != id) return null;

        var attribute = _mapper.Map<ProductAttribute>(updateAttribute);
        _unitOfWork.Repository<ProductAttribute>().Update(attribute);

        if (await _unitOfWork.Complete())
        {
            var updatedAttribute = await _unitOfWork.Repository<ProductAttribute>().GetByIdAsync(id);
            return updatedAttribute != null ? _mapper.Map<AttributeDto>(updatedAttribute) : null;
        }
        return null;
    }

    public async Task<bool> DeleteAttribute(long id)
    {
        var attribute = await _unitOfWork.Repository<ProductAttribute>().GetByIdAsync(id);
        if (attribute == null) return false;

        _unitOfWork.Repository<ProductAttribute>().Remove(attribute);
        return await _unitOfWork.Complete();
    }

    public async Task<AttributeOptionDto?> AddAttributeOption(int attributeId, CreateAttributeOptionDto createOptionDto)
    {
        var attribute = await _unitOfWork.Repository<ProductAttribute>().GetByIdAsync(attributeId);
        if (attribute == null) return null;

        var option = _mapper.Map<AttributeOption>(createOptionDto);
        option.AttributeId = attributeId;
        option.AttributeCode = attribute.AttributeCode;

        attribute.AttributeOptions.Add(option);
        _unitOfWork.Repository<ProductAttribute>().Update(attribute);

        if (await _unitOfWork.Complete())
        {
            return _mapper.Map<AttributeOptionDto>(option);
        }
        return null;
    }

    public async Task<bool> RemoveAttributeOption(int attributeId, int optionId)
    {
        var attribute = await _unitOfWork.Repository<ProductAttribute>().GetByIdAsync(attributeId);
        if (attribute == null) return false;

        var option = attribute.AttributeOptions.FirstOrDefault(o => o.Id == optionId);
        if (option == null) return false;

        attribute.AttributeOptions.Remove(option);
        _unitOfWork.Repository<ProductAttribute>().Update(attribute);

        return await _unitOfWork.Complete();
    }
}
