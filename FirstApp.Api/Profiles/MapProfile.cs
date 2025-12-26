using AutoMapper;
using FirstApp.Api.Models;

namespace FirstApp.Api.Profiles;

public class MapProfile:Profile
{
    public MapProfile()
    {
        CreateMap<Product, Dtos.Products.ProductReturnDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
    }
}
