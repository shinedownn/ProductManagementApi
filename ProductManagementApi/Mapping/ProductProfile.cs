using AutoMapper;
using ProductManagementApi.Entities.Concrete;
using ProductManagementApi.Entities.Dtos.ProductDto;

namespace ProductManagementApi.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductEntity, GetProductDto>().ReverseMap();
            CreateMap<ProductEntity, CreateProductDto>().ReverseMap();
            CreateMap<ProductEntity, DeleteProductDto>().ReverseMap();
            CreateMap<ProductEntity, UpdateProductDto>().ReverseMap();
        }
    }
}
