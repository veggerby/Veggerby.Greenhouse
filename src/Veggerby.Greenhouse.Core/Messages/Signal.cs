using System;
using System.Text.Json.Serialization;

namespace Veggerby.Greenhouse.Core.Messages
{
    public class Signal
    {
        [JsonPropertyName("device")]
        public string Device { get; set; }

        [JsonPropertyName("sensor")]
        public string Sensor { get; set; }

        [JsonPropertyName("property")]
        public string Property { get; set; }

        [JsonPropertyName("time")]
        public DateTime TimeUtc { get; set; }

        [JsonPropertyName("value")]
        public double? Value { get; set; }
    }
}