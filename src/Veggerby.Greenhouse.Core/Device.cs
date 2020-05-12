using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Veggerby.Greenhouse.Core
{
    public class Device
    {
        [JsonPropertyName("deviceId")]
        [Column(Order = 0)]
        public string DeviceId { get; set; }

        [JsonPropertyName("name")]
        [Column(Order = 1)]
        public string Name { get; set; }

        [JsonPropertyName("created")]
        [Column(Order = 2)]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public IList<Measurement> Measurements { get; set; }
    }
}