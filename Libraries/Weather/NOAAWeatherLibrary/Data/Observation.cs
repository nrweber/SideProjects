using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NOAAWeather
{
    public class AllObservations
    {
        [JsonPropertyName("@context")]
        public List<object> Context { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("features")]
        public List<Observation> Feature { get; set; }
    }

    public class Observation
    {
        [JsonPropertyName("@context")]
        public List<object> Context { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        [JsonPropertyName("properties")]
        public ObservationProperties Properties { get; set; }
    }


    public class ObservationProperties
    {
        [JsonPropertyName("@id")]
        public string Id { get; set; }

        [JsonPropertyName("@type")]
        public string Type { get; set; }

        [JsonPropertyName("elevation")]
        public Elevation Elevation { get; set; }

        [JsonPropertyName("station")]
        public string Station { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("rawMessage")]
        public string RawMessage { get; set; }

        [JsonPropertyName("textDescription")]
        public string TextDescription { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("presentWeather")]
        public List<PresentWeather> PresentWeather { get; set; }

        [JsonPropertyName("temperature")]
        public Temperature Temperature { get; set; }

        [JsonPropertyName("dewpoint")]
        public Dewpoint Dewpoint { get; set; }

        [JsonPropertyName("windDirection")]
        public WindDirection WindDirection { get; set; }

        [JsonPropertyName("windSpeed")]
        public WindSpeed WindSpeed { get; set; }

        [JsonPropertyName("windGust")]
        public WindGust WindGust { get; set; }

        [JsonPropertyName("barometricPressure")]
        public BarometricPressure BarometricPressure { get; set; }

        [JsonPropertyName("seaLevelPressure")]
        public SeaLevelPressure SeaLevelPressure { get; set; }

        [JsonPropertyName("visibility")]
        public Visibility Visibility { get; set; }

        [JsonPropertyName("maxTemperatureLast24Hours")]
        public MaxTemperatureLast24Hours MaxTemperatureLast24Hours { get; set; }

        [JsonPropertyName("minTemperatureLast24Hours")]
        public MinTemperatureLast24Hours MinTemperatureLast24Hours { get; set; }

        [JsonPropertyName("precipitationLastHour")]
        public PrecipitationLastHour PrecipitationLastHour { get; set; }

        [JsonPropertyName("precipitationLast3Hours")]
        public PrecipitationLast3Hours PrecipitationLast3Hours { get; set; }

        [JsonPropertyName("precipitationLast6Hours")]
        public PrecipitationLast6Hours PrecipitationLast6Hours { get; set; }

        [JsonPropertyName("relativeHumidity")]
        public RelativeHumidity RelativeHumidity { get; set; }

        [JsonPropertyName("windChill")]
        public WindChill WindChill { get; set; }

        [JsonPropertyName("heatIndex")]
        public HeatIndex HeatIndex { get; set; }

        [JsonPropertyName("cloudLayers")]
        public List<CloudLayer> CloudLayers { get; set; }
    }


    public class PresentWeather
    {
        [JsonPropertyName("intensity")]
        public string Intensity { get; set; }

        [JsonPropertyName("modifier")]
        public object Modifier { get; set; }

        [JsonPropertyName("weather")]
        public string Weather { get; set; }

        [JsonPropertyName("rawString")]
        public string RawString { get; set; }
    }

    public class Temperature
    {
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; }
    }

    public class Dewpoint
    {
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; }
    }

    public class WindDirection
    {
        [JsonPropertyName("value")]
        public int? Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; }
    }

    public class WindSpeed
    {
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; }
    }

    public class WindGust
    {
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; }
    }

    public class BarometricPressure
    {
        [JsonPropertyName("value")]
        public int? Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; }
    }

    public class SeaLevelPressure
    {
        [JsonPropertyName("value")]
        public object Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; }
    }

    public class Visibility
    {
        [JsonPropertyName("value")]
        public int? Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; }
    }

    public class MaxTemperatureLast24Hours
    {
        [JsonPropertyName("value")]
        public object Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public object QualityControl { get; set; }
    }

    public class MinTemperatureLast24Hours
    {
        [JsonPropertyName("value")]
        public object Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public object QualityControl { get; set; }
    }

    public class PrecipitationLastHour
    {
        [JsonPropertyName("value")]
        public object Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; }
    }

    public class PrecipitationLast3Hours
    {
        [JsonPropertyName("value")]
        public object Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; }
    }

    public class PrecipitationLast6Hours
    {
        [JsonPropertyName("value")]
        public object Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; }
    }

    public class RelativeHumidity
    {
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; }
    }

    public class WindChill
    {
        [JsonPropertyName("value")]
        public object Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; }
    }

    public class HeatIndex
    {
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; }
    }

    public class Base
    {
        [JsonPropertyName("value")]
        public int? Value { get; set; }

        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; }
    }

    public class CloudLayer
    {
        [JsonPropertyName("base")]
        public Base Base { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }
    }



}
