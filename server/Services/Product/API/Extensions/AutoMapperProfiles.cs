using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Extensions
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Category
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryDescription, CategoryDescriptionDto>().ReverseMap();

            CreateMap<CreateCategoryDto, Category>()
            .ForMember(dest => dest.CategoryDescriptions, opt => opt.MapFrom(src => new List<CategoryDescription>
            {
                new CategoryDescription
                {
                    Name = src.Name,
                    ShortDescription = src.ShortDescription,
                    Description = src.Description,
                    Image = src.Image,
                    UrlKey = src.UrlKey
                }
            }));

            CreateMap<UpdateCategoryDto, Category>()
            .ForMember(dest => dest.CategoryDescriptions, opt => opt.MapFrom(src => new List<CategoryDescription>
            {
                new CategoryDescription
                {
                    Name = src.Name,
                    ShortDescription = src.ShortDescription,
                    Description = src.Description,
                    Image = src.Image,
                    UrlKey = src.UrlKey
                }
            }));

            // Attribute
            CreateMap<ProductAttribute, AttributeDto>().ReverseMap();
            CreateMap<AttributeOption, AttributeOptionDto>().ReverseMap();

            CreateMap<ProductAttribute, CreateAttributeDto>()
                .ReverseMap()
                .AfterMap((dto, entity) =>
                {
                    foreach (var option in entity.AttributeOptions)
                    {
                        option.AttributeCode = dto.AttributeCode;
                    }
                });

            CreateMap<AttributeOption, CreateAttributeOptionDto>().ReverseMap();

            CreateMap<ProductAttribute, UpdateAttributeDto>()
                .ReverseMap()
                .AfterMap((dto, entity) =>
                {
                    foreach (var option in entity.AttributeOptions)
                    {
                        option.AttributeCode = dto.AttributeCode;
                    }
                });

            CreateMap<AttributeOption, UpdateAttributeOptionDto>().ReverseMap();
        }
    }
}
