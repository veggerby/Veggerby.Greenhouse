using System;

namespace Veggerby.Greenhouse.Web.Models
{
    public class MeasurementModel
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }
        public double? AverageValue { get; set; }
        public int SignalCount { get; set; }
        public AnnotationModel[] Annotations { get; set; }
    }
}