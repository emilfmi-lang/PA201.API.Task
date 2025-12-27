using AutoMapper;
using FirstApp.Api.Dtos.Categories;
using FirstApp.Api.Dtos.Products;
using FirstApp.Api.Models;

namespace FirstApp.Api.Profiles;

public class MapProfile:Profile
{
    public MapProfile()
    {
        CreateMap<Product, ProductReturnDto>();
            //.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
        CreateMap<Category, CategoryInProductReturnDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<Category, CategoriesReturnDto>()
            .ForMember(dest => dest.PCount, opt => opt.MapFrom(src => src.Products.Count));
        CreateMap<Product, ProductInCategoryDto>();
    }
}
