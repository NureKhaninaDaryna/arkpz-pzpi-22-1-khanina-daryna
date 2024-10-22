using DineMetrics.Core.Enums;

namespace DineMetrics.Core.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public UserRole Role { get; set; } = UserRole.Manager;

        public Admin? Admin { get; set; }

        public Manager? Manager { get; set; }
    }
}
