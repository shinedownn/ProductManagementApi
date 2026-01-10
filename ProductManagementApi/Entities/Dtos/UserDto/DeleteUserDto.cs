namespace ProductManagementApi.Entities.Dtos.UserDto
{
    public class DeleteUserDto : IDto
    {
        public int Id { get; set; }
        public string Username { get; set; } 
        public bool IsActive { get; set; } 
    }
}
