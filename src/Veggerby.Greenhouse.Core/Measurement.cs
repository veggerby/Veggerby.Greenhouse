using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veggerby.Greenhouse.Core
{
    public class Measurement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int MeasurementId { get; set; }

        [Column(Order = 1)]
        public string DeviceId { get; set; }

        [Column(Order = 2)]
        public string SensorId { get; set; }

        [Column(Order = 3)]
        public string PropertyId { get; set; }

        [Column(Order = 4)]
        public DateTime FirstTimeUtc { get; set; }

        [Column(Order = 5)]
        public DateTime LastTimeUtc { get; set; }

        [Column(Order = 6)]
        public double Value { get; set; }

        [Column(Order = 7)]
        public double SumValue { get; set; }

        [Column(Order = 8)]
        public int Count { get; set; } = 1;

        [Column(Order = 9)]
        public double MinValue { get; set; }

        [Column(Order = 10)]
        public double MaxValue { get; set; }

        [Column(Order = 11)]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        [Column(Order = 12)]
        public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow;

        public Device Device { get; set; }
        public Sensor Sensor { get; set; }
        public Property Property { get; set; }

        public double AverageValue => SumValue / Count;
    }
}