using DineMetrics.Core.Enums;

namespace DineMetrics.Core.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public UserRole Role { get; set; } = UserRole.Manager;

        public virtual Admin? Admin { get; set; }

        public virtual Manager? Manager { get; set; }
    }
}
