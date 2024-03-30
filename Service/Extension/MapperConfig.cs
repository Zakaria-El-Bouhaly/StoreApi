using AutoMapper;
using Shared.Dto;
using Shared.Models;

namespace Service.Extension
{

    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();

        }
    }
}