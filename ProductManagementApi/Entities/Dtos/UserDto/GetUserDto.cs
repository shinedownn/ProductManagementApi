namespace ProductManagementApi.Entities.Dtos.UserDto
{
    public class GetUserDto : IDto
    {
        public int? Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
