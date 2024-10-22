﻿using DineMetrics.Core.Enums;

namespace DineMetrics.Core.Models
{
    public class Device : BaseEntity
    {
        public string SerialNumber { get; set; } = null!;

        public string Model { get; set; } = null!;

        public DeviceState State { get; set; } = DeviceState.Online;

        public Eatery Eatery { get; set; } = null!;

        public ICollection<CustomerMetric> CustomerMetrics { get; set; } = new List<CustomerMetric>();

        public ICollection<TemperatureMetric> TemperatureMetrics { get; set; } = new List<TemperatureMetric>();
    }
}
