using System;
using System.Text.Json.Serialization;

namespace Veggerby.Greenhouse
{
    /*

CREATE TABLE [dbo].[Measurement]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [TimeUtc] DATETIME NOT NULL,
    [Temperature] FLOAT NOT NULL,
    [Pressure] FLOAT NOT NULL,
    [Humidity] FLOAT NOT NULL
)

    */
    public class Measurement
    {
        [JsonPropertyName("id")]
        public Guid MeasurementId { get; set; } = Guid.NewGuid();

        [JsonPropertyName("time")]
        public DateTime TimeUtc { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("pressure")]
        public double Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }
    }
}
