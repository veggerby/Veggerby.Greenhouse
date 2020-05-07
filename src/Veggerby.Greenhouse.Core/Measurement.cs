using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Veggerby.Greenhouse.Core
{
    public class Measurement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeasurementId { get; set; }

        [JsonPropertyName("deviceId")]
        public string DeviceId { get; set; }

        [JsonPropertyName("device")]
        public Device Device { get; set; }

        [JsonPropertyName("time")]
        public DateTime TimeUtc { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("pressure")]
        public double Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
