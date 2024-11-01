using DineMetrics.Core.Enums;

namespace DineMetrics.Core.Dto;

public class EateryDto
{
    public string Name { get; set; }

    public string Address { get; set; }
    
    public EateryType Type { get; set; }
        
    public DateOnly OpeningDay { get; set; }
}