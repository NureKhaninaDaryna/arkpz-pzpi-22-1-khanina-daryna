namespace DineMetrics.Core.Dto;

public class TemperatureMetricDto
{
    public int Value { get; set; } 

    public DateTime Time { get; set; }
    
    public Guid DeviceId { get; set; }
}