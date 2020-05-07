using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Veggerby.Greenhouse.Core
{
    public class Device
    {
        [JsonPropertyName("deviceId")]
        public string DeviceId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public IList<Measurement> Measurements { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}