namespace DineMetrics.Core.Models
{
    public class Manager : BaseEntity
    {
        public User User { get; set; } = null!;

        public Eatery Eatery { get; set; } = null!;

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
