using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Veggerby.Greenhouse.Core
{
    public class Property
    {
        [JsonPropertyName("propertyId")]
        [Column(Order = 0)]
        public string PropertyId { get; set; }

        [JsonPropertyName("name")]
        [Column(Order = 1)]
        public string Name { get; set; }

        [JsonPropertyName("unit")]
        [Column(Order = 2)]
        public string Unit { get; set; }

        [JsonPropertyName("tolerance")]
        [Column(Order = 3)]
        public double? Tolerance { get; set; } = 0.01;

        [JsonPropertyName("decimals")]
        [Column(Order = 4)]
        public int Decimals { get; set; } = 3;

        [JsonPropertyName("created")]
        [Column(Order = 5)]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public IList<Measurement> Measurements { get; set; }
    }
}
