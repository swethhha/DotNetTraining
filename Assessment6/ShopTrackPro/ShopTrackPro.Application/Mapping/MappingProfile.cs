using AutoMapper;
using ShopTrackPro.Core.DTO;
using ShopTrackPro.Core.Entities;

namespace ShopTrackPro.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product
            CreateMap<Product, ProductResponseDTO>();
            CreateMap<ProductRequestDTO, Product>();

            // User
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserRequestDTO, User>();

            // OrderItem
            CreateMap<OrderItem, OrderItemResponseDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));

            CreateMap<OrderItemRequestDTO, OrderItem>();

            // Order
            CreateMap<Order, OrderResponseDTO>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderRequestDTO, Order>();
        }
    }
}
