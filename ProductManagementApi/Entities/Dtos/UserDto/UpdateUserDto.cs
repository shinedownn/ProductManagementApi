namespace ProductManagementApi.Entities.Dtos.UserDto
{
    public class UpdateUserDto : IDto
    { 
        public string Username { get; set; }
        public bool IsActive { get; set; } 
    }
}
