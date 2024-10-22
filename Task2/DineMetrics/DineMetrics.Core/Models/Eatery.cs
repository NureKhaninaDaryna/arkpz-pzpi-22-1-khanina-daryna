using DineMetrics.Core.Enums;

namespace DineMetrics.Core.Models
{
    public class Eatery : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public EateryType Type { get; set; }
        
        public DateOnly OpeningDay { get; set; }

        public ICollection<Device> Devices { get; set; } = new List<Device>();

        public ICollection<Manager> Managers { get; set; } = new List<Manager>();
    }
}
