using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veggerby.Greenhouse.Core
{
    public class Sensor
    {
        [Column(Order = 0)]
        public string DeviceId { get; set; }

        [Column(Order = 1)]
        public string SensorId { get; set; }

        [Column(Order = 2)]
        public string Name { get; set; }

        [Column(Order = 3)]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        public Device Device { get; set; }

        public IList<Measurement> Measurements { get; set; }
    }
}
