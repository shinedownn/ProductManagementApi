using AutoMapper;
using ProductManagementApi.Entities.Concrete;
using ProductManagementApi.Entities.Dtos.ProductDto;
using ProductManagementApi.Entities.EndpointParams.User;

namespace ProductManagementApi.Mapping
{
    public class UserParamsProfile : Profile
    {
        public UserParamsProfile()
        {
            CreateMap<UserEntity, LoginParams>().ReverseMap(); 
        }
    }
}
