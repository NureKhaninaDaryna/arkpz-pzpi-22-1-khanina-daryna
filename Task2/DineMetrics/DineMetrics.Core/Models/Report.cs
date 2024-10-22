namespace DineMetrics.Core.Models
{
    public class Report : BaseEntity
    {
        public ICollection<TemperatureMetric> TemperatureMetrics { get; set; } = new List<TemperatureMetric>();

        public ICollection<CustomerMetric> CustomerMetrics { get; set; } = new List<CustomerMetric>();

        public double AverageTemperature { get; set; }

        public int TotalCustomers { get; set; }

        public DateOnly ReportDate { get; set; }
    }
}
