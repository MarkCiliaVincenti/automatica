namespace P3.Driver.OpenWeather.Api.Models.OneCallModel;

/// <summary>
/// Gets the One Call API information about an hourly forecast of the weather.
/// </summary>
public class Hourly
{


    /// <summary>
    /// The temperature, depends on the measurement system.
    /// </summary>
    [JsonProperty("temp")]
    public double Temperature { get; set; }

    /// <summary>
    /// The temperature, accounting for the human perception of temperature. Depends on the measurement system.
    /// </summary>
    [JsonProperty("feels_like")]
    public double TemperaturePerception { get; set; }

    /// <summary>
    /// The atmospheric pressure on the sea level, in hPa.
    /// </summary>
    [JsonProperty("pressure")]
    public int AtmosphericPressure { get; set; }

    /// <summary>
    /// The humidity (%).
    /// </summary>
    [JsonProperty("humidity")]
    public int HumidityPercentage { get; set; }

    /// <summary>
    /// The atmospheric temperature (that varies according to pressure and humidity), below which water droplets begin to condense and dew can form.
    /// </summary>
    [JsonProperty("dew_point")]
    public double DewPoint { get; set; }

    /// <summary>
    /// The UV index. Can be null if not available.
    /// </summary>
    [JsonProperty("uvi")]
    public double? UVIndex { get; set; }

    /// <summary>
    /// The amount of clouds in the sky (%).
    /// </summary>
    [JsonProperty("clouds")]
    public int CloudPercentage { get; set; }

    /// <summary>
    /// The average visibility in meters.
    /// </summary>
    [JsonProperty("visibility")]
    public int? Visibility { get; set; }

    /// <summary>
    /// The speed of the wind, depends on the measurement system.
    /// </summary>
    [JsonProperty("wind_speed")]
    public double WindSpeed { get; set; }

    /// <summary>
    /// The direction of the wind, in degrees.
    /// </summary>
    [JsonProperty("wind_deg")]
    public int WindDirection { get; set; }

    /// <summary>
    /// Wind gust, can be null if not available.
    /// </summary>
    [JsonProperty("wind_gust")]
    public double? WindGust { get; set; }

    /// <summary>
    /// The probability of precepitation, can be null if not available.
    /// </summary>
    [JsonProperty("pop")]
    public double? PrecipitationProbability { get; set; }

#nullable enable

    /// <summary>
    /// Information about the rain.
    /// </summary>
    [JsonProperty("rain")]
    public Models.Rain? Rain { get; set; }

    /// <summary>
    /// Information about the rain.
    /// </summary>
    [JsonProperty("snow")]
    public Snow? Snow { get; set; }

#nullable disable

    /// <summary>
    /// Information and a description of the weather.
    /// </summary>
    [JsonProperty("weather")]
    public Weather[] Description { get; set; }
}