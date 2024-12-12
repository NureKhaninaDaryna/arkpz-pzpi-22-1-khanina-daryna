namespace DineMetrics.Core.Dto;

public class CustomerMetricDto
{
    public int Count { get; set; } 

    public DateTime Time { get; set; }
    
    public Guid DeviceId { get; set; }
}