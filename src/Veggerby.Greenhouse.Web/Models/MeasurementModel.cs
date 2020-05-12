using System;

namespace Veggerby.Greenhouse.Web.Models
{
    public class MeasurementModel
    {
        public DeviceModel Device { get; set; }
        public PropertyModel Property { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double AverageValue { get; set; }
        public int SignalCount { get; set; }
    }
}