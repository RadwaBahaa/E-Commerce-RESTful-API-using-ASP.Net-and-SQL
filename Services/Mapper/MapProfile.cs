using AutoMapper;
using DTOs.DTOs.Orders;
using DTOs.DTOs.Category;
using DTOs.DTOs.Products;
using Models.Models;
using DTOs.DTOs.OrderProducts;

namespace Services.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            // Mapping Category Model______________________________________________
            CreateMap<ReadAllCategoryDTO,Category>().ReverseMap();
            CreateMap<CreateOrUpdateCategoryDTO, Category>().ReverseMap();

            // Mapping Product Model______________________________________________
            CreateMap<CreateOrUpdateProductDTO, Product>().ReverseMap();
            CreateMap<ReadAllProductsDTO, Product>().ReverseMap();
            CreateMap<ReadProductsByCategoryDTO, Product>().ReverseMap();

            // Mapping Order Model______________________________________________
            CreateMap<ReadAllOrdersDTO, Order>().ReverseMap();
            CreateMap<CreateOrderDTO, Order>().ReverseMap();
            CreateMap<UpdateOrderDTO,Order>().ReverseMap();
            CreateMap<ReadUpdatedorCreaterOrderDTO, Order>().ReverseMap();

            // Mapping OrderProduct Model______________________________________________
            CreateMap<ReadOrCreateOrderProductDTO, OrderProduct>().ReverseMap();
        }
    }
}
