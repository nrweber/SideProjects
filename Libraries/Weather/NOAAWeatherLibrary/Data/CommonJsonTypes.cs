using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NOAAWeather
{
    public class Elevation
    {
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string? UnitCode { get; set; }
    }

    public class Geometry
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<double?> Coordinates { get; set; } = new();
    }
}
