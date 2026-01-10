using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace ProductManagementApi.Entities.Concrete
{
    public record UserEntity : IEntity
    {
        public int? Id { get; set; } = null;
        public string Username { get; set; } 
        public string PasswordHash { get; set; }
        public bool? IsActive { get; set; } = null;
        public DateTime? CreatedAt { get; set; } = null;
    }
}
