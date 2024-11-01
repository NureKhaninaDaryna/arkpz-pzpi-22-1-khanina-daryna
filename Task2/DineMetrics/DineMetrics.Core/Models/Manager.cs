namespace DineMetrics.Core.Models
{
    public class Manager : BaseEntity
    {
        public virtual User User { get; set; } = null!;

        public virtual Eatery Eatery { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
