using AutoMapper;
using src.DTOs;
using src.Controllers.Models.Entities;
namespace src
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ---
            // ---
            // ---
            // User Mappings
            CreateMap<Users, UserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role!.RoleName))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status!.StatusDescription));
            CreateMap<CreateUserDto, Users>();
            CreateMap<UpdateUserDto, Users>();

            // ---
            // ---
            // ---
            // Product Mappings
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.CategoryName))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status!.StatusDescription));
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // ---
            // ---
            // ---
            // Category Mappings
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status!.StatusDescription));
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            // ---
            // ---
            // ---
            // Role Mappings
            CreateMap<Role, RoleDto>();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();

            // ---
            // ---
            // ---
            // Status Mappings
            CreateMap<Status, StatusDto>();
            CreateMap<CreateStatusDto, Status>();
            CreateMap<UpdateStatusDto, Status>();

            // ---
            // ---
            // ---
            // Address Mappings
            CreateMap<Address, AddressDto>()
                .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.Users!.Fullname))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status!.StatusDescription));
            CreateMap<CreateAddressDto, Address>();
            CreateMap<UpdateAddressDto, Address>();

            // ---
            // ---
            // ---
            // ShoppingCart & CartProduct Mappings
            CreateMap<ShoppingCart, ShoppingCartDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Users!.Fullname))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status!.StatusDescription))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartProducts));
            CreateMap<CartProduct, CartProductDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.ProductName))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status!.StatusDescription));
            CreateMap<CreateShoppingCartDto, ShoppingCart>();
            CreateMap<UpdateShoppingCartDto, ShoppingCart>();
            CreateMap<CreateCartProductDto, CartProduct>();
            CreateMap<UpdateCartProductDto, CartProduct>();

            // ---
            // ---
            // ---
            // Review Mappings
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.ProductName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User!.Fullname));
            CreateMap<CreateReviewDto, Review>();
            CreateMap<UpdateReviewDto, Review>();

            // ---
            // ---
            // ---
            // Image Mappings
            CreateMap<Image, ImageDto>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status!.StatusDescription));
            CreateMap<CreateImageDto, Image>();
            CreateMap<UpdateImageDto, Image>();

            // ---
            // ---
            // ---
            // Sale & SaleDetail Mappings
            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod!.MethodName))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status!.StatusDescription))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => $"{src.Address!.AddressLine}, {src.Address.City}"));
            CreateMap<SaleDetail, SaleDetailDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.ProductName));
            CreateMap<CreateSaleDto, Sale>();
            CreateMap<UpdateSaleDto, Sale>();
            CreateMap<CreateSaleDetailDto, SaleDetail>();
            CreateMap<UpdateSaleDetailDto, SaleDetail>();

            // ---
            // ---
            // ---
            // PaymentMethod Mappings
            CreateMap<PaymentMethod, PaymentMethodDto>();
            CreateMap<CreatePaymentMethodDto, PaymentMethod>();
            CreateMap<UpdatePaymentMethodDto, PaymentMethod>();
        }
    }
}