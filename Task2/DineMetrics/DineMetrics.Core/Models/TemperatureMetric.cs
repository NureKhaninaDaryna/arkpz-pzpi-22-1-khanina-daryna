namespace DineMetrics.Core.Models
{
    public class TemperatureMetric : BaseEntity
    {
        public double Value { get; set; }

        public DateTime Time { get; set; }

        public Device Device { get; set; } = null!;

        public Report Report { get; set; } = null!;
    }
}
