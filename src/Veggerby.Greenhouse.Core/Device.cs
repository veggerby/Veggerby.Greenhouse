using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veggerby.Greenhouse.Core
{
    public class Device
    {
        [Column(Order = 0)]
        public string DeviceId { get; set; }

        [Column(Order = 1)]
        public string Name { get; set; }

        [Column(Order = 2)]
        public bool Enabled { get; set; } = true;

        [Column(Order = 3)]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        public IList<Measurement> Measurements { get; set; }

        public IList<Sensor> Sensors { get; set; }
    }
}