namespace DineMetrics.Core.Dto;

public class TemperatureMetricDto
{
    public double Value { get; set; } 

    public DateTime Time { get; set; }
    
    public Guid DeviceId { get; set; }
}