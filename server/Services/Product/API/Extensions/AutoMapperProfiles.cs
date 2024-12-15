using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Extensions
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryDescription, CategoryDescriptionDto>().ReverseMap();
        }
    }
}
