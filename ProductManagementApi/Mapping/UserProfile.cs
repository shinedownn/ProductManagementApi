using AutoMapper;
using ProductManagementApi.Entities.Concrete;
using ProductManagementApi.Entities.Dtos.UserDto;

namespace ProductManagementApi.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, GetUserDto>().ReverseMap();
            CreateMap<UserEntity, CreateUserDto>().ReverseMap();
            CreateMap<UserEntity, DeleteUserDto>().ReverseMap(); 
            CreateMap<UserEntity, UpdateUserDto>().ReverseMap(); 
        }
    }
}
