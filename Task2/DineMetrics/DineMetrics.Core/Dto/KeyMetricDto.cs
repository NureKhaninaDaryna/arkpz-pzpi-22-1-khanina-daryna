namespace DineMetrics.Core.Dto;

public class KeyMetricDto
{
    public Guid DeviceId { get; set; }
    
    public double Value { get; set; }
    
    public DateTime Time { get; set; }
}