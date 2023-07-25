using AutoMapper;
using WebBookStore.Dto;
using WebBookStore.Models;

namespace WebBookStore.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Cartitem, CartitemDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<User, UserLogin>().ReverseMap();
            CreateMap<Cartitem, CartitemCreate>().ReverseMap();
        }
    }
}
