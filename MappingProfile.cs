using AutoMapper;
using test_LK_ecommerce.Controllers.Models.Entities;
using test_LK_ecommerce.DTOs;

namespace test_LK_ecommerce
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            // Product Mappings
            // For Reading (Entity -> DTO)
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName,
                           opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.StatusName,
                           opt => opt.MapFrom(src => src.Status.StatusDescription));

            // For Writing (DTO -> Entity)
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // ---

            // Category Mappings
            // For Reading (Entity -> DTO)
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.StatusName,
                           opt => opt.MapFrom(src => src.Status.StatusDescription));

            // For Writing (DTO -> Entity)
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
