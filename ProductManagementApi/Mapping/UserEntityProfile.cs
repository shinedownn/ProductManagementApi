using AutoMapper;
using ProductManagementApi.Entities.Concrete;
using ProductManagementApi.Entities.Dtos.UserDto;

namespace ProductManagementApi.Mapping
{
    public class UserEntityProfile : Profile
    {
        public UserEntityProfile()
        {
            CreateMap<UserEntity, GetUserDto>().ReverseMap();
            CreateMap<UserEntity, CreateUserDto>().ReverseMap();
            CreateMap<UserEntity, DeleteUserDto>().ReverseMap(); 
            CreateMap<UserEntity, UpdateUserDto>().ReverseMap(); 
        }
    }
}
