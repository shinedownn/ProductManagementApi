namespace ProductManagementApi.Entities.Dtos.UserDto
{
    public class CreateUserDto : IDto
    { 
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
