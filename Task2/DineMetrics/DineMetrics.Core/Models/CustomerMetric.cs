namespace DineMetrics.Core.Models
{
    public class CustomerMetric : BaseEntity
    {
        public int Count { get; set; } 

        public DateTime Time { get; set; }

        public Device Device { get; set; } = null!;

        public Report Report { get; set; } = null!;
    }
}
