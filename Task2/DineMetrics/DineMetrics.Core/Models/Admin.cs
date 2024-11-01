namespace DineMetrics.Core.Models
{
    public class Admin : BaseEntity
    {
        public virtual User User { get; set; } = null!;

        public DateOnly AppointmentDate { get; set; }
    }
}
