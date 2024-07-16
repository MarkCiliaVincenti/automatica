namespace P3.Driver.OpenWeather.Api.Models.OneCallModel;

/// <summary>
/// Gets the One Call API information about the current weather.
/// </summary>
public class Current
{
    /// <summary>
    /// The analysis date in unix seconds, UTC.
    /// </summary>
    [JsonProperty("dt")]
    public long AnalysisDateUnix { get; set; }

    /// <summary>
    /// The analysis date, UTC.
    /// </summary>
    public DateTime AnalysisDate
    {
        get { return DateTimeOffset.FromUnixTimeSeconds(AnalysisDateUnix).UtcDateTime; }
    }

    /// <summary>
    /// The sunrise time in unix seconds, UTC.
    /// </summary>
    [JsonProperty("sunrise")]
    public long SunriseTimeUnix { get; set; }

    /// <summary>
    /// The sunrise time, UTC.
    /// </summary>
    public DateTime SunriseTime
    {
        get { return DateTimeOffset.FromUnixTimeSeconds(SunriseTimeUnix).UtcDateTime; }
    }

    /// <summary>
    /// The sunset time in unix seconds, UTC.
    /// </summary>
    [JsonProperty("sunset")]
    public long SunsetTimeUnix { get; set; }

    /// <summary>
    /// The sunset time, UTC.
    /// </summary>
    public DateTime SunsetTime
    {
        get { return DateTimeOffset.FromUnixTimeSeconds(SunsetTimeUnix).UtcDateTime; }
    }

    /// <summary>
    /// The temperature of the location, depends on the measurement system.
    /// </summary>
    [JsonProperty("temp")]
    public double Temperature { get; set; }

    /// <summary>
    /// The temperature of the location, depends on the measurement system.
    /// </summary>
    [JsonProperty("feels_like")]
    public double TemperaturePerception { get; set; }

    /// <summary>
    /// The atmospheric pressure in the sea level, in hPa.
    /// </summary>
    [JsonProperty("pressure")]
    public int AtmosphericPressure { get; set; }

    /// <summary>
    /// The humidity (%) of the location.
    /// </summary>
    [JsonProperty("humidity")]
    public int HumidityPercentage { get; set; }

    /// <summary>
    /// The atmospheric temperature (that varies according to pressure and humidity), below which water droplets begin to condense and dew can form. Depends on the measurement system.
    /// </summary>
    [JsonProperty("dew_point")]
    public double DewPoint { get; set; }

    /// <summary>
    /// The current UV index, can be null in places where it is not available.
    /// </summary>
    [JsonProperty("uvi")]
    public double? UVIndex { get; set; }

    /// <summary>
    /// The percentage of clouds in the sky.
    /// </summary>
    [JsonProperty("clouds")]
    public int CloudPercentage { get; set; }

    /// <summary>
    /// The average visibility in meters.
    /// </summary>
    [JsonProperty("visibility")]
    public int Visibility { get; set; }

    /// <summary>
    /// The speed of the wind, depends on the measurement system.
    /// </summary>
    [JsonProperty("wind_speed")]
    public double WindSpeed { get; set; }

    /// <summary>
    /// The direction of the wind in degrees.
    /// </summary>
    [JsonProperty("wind_deg")]
    public int WindDirection { get; set; }

    /// <summary>
    /// Wind gust, can be null if not available.
    /// </summary>
    [JsonProperty("wind_gust")]
    public double? WindGust { get; set; }

#nullable enable

    /// <summary>
    /// Information about the rain, null if not available.
    /// </summary>
    [JsonProperty("rain")]
    public Models.Rain? Rain { get; set; }

    /// <summary>
    /// Information about the snow, null if not available.
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