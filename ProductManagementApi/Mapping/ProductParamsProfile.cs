using AutoMapper;
using ProductManagementApi.Entities.Concrete;
using ProductManagementApi.Entities.Dtos.ProductDto;
using ProductManagementApi.Entities.EndpointParams.Product;

namespace ProductManagementApi.Mapping
{
    public class ProductParamsProfile : Profile
    {
        public ProductParamsProfile()
        { 
            CreateMap<ProductEntity, CreateProductParams>().ReverseMap();
            CreateMap<ProductEntity, DeleteProductParams>().ReverseMap();
            CreateMap<ProductEntity, UpdateProductParams>().ReverseMap();
        }
    }
}
