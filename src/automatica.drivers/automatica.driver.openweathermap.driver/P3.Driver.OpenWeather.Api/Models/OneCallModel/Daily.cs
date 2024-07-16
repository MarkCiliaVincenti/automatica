namespace P3.Driver.OpenWeather.Api.Models.OneCallModel;

/// <summary>
/// Gets the One Call API information about a daily forecast.
/// </summary>
public class Daily
{
    /// <summary>
    /// Time of the forecasted data in unix seconds, UTC.
    /// </summary>
    [JsonProperty("dt")]
    public long AnalysisDateUnix { get; set; }

    /// <summary>
    /// Time of the forecasted data, UTC.
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
    /// The moonrise time in unix seconds, UTC.
    /// </summary>
    [JsonProperty("moonrise")]
    public long MoonriseTimeUnix { get; set; }

    /// <summary>
    /// The moonrise time, UTC.
    /// </summary>
    public DateTime MoonriseTime
    {
        get { return DateTimeOffset.FromUnixTimeSeconds(MoonriseTimeUnix).UtcDateTime; }
    }

    /// <summary>
    /// The moonset time in unix seconds, UTC.
    /// </summary>
    [JsonProperty("moonset")]
    public long MoonsetTimeUnix { get; set; }

    /// <summary>
    /// The moonset time, UTC.
    /// </summary>
    public DateTime MoonsetTime
    {
        get { return DateTimeOffset.FromUnixTimeSeconds(MoonsetTimeUnix).UtcDateTime; }
    }

    /// <summary>
    /// The moon phase:<br />
    /// - 0 and 1: New moon.<br />
    /// - 0 to 0.25 (exclusive): Waxing crescent.<br />
    /// - 0.25: First quarter moon.<br />
    /// - 0.25 to 0.5 (exclusive): Waxing gibous.<br />
    /// - 0.5: Full moon.<br />
    /// - 0.5 to 0.75 (exclusive): Waning gibous.<br />
    /// - 0.75: Last quarter moon.<br />
    /// - 0.75 to 1 (exclusive): Waning crescent.
    /// </summary>
    [JsonProperty("moon_phase")]
    public double MoonPhaseIndex { get; set; }

    /// <summary>
    /// The moon phase in english.
    /// </summary>
    public string MoonPhase {
        get
        {
            if (MoonPhaseIndex == 0 || MoonPhaseIndex == 1)
                return "new moon";

            else if (MoonPhaseIndex == 0.25)
                return "first quarter moon";

            else if (MoonPhaseIndex == 0.5)
                return "full moon";
            
            else if (MoonPhaseIndex == 0.75)
                return "last quarter moon";
            
            else if (MoonPhaseIndex < 0.25)
                return "waxing crescent";
            
            else if (MoonPhaseIndex < 0.5)
                return "waxing gibous";
            
            else if (MoonPhaseIndex < 0.75)
                return "waning gibous";
            
            else return "waning crescent";
        }
    }

    /// <summary>
    /// Description of the temperature of the day, depends on the measurement system.
    /// </summary>
    [JsonProperty("temp")]
    public DayTemperature Temperature { get; set; }

    /// <summary>
    /// Description of the temperature of the day, accounting for the human perception of temperature. Depends on the measurement system.
    /// </summary>
    [JsonProperty("feels_like")]
    public DayTemperaturePerception TemperaturePerception { get; set; }

    /// <summary>
    /// Atmospheric pressure on the sea level, in hPa.
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
    /// Depends on the measurement system.
    /// </summary>
    [JsonProperty("dew_point")]
    public double DewPoint { get; set; }

    /// <summary>
    /// The speed of the wind. Depends on the measurement system.
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

    /// <summary>
    /// The amount of clouds in the sky (%).
    /// </summary>
    [JsonProperty("clouds")]
    public int CloudPercentage { get; set; }

    /// <summary>
    /// The maximum UV index of the day.
    /// </summary>
    [JsonProperty("uvi")]
    public double UVIndex { get; set; }

    /// <summary>
    /// The probability of precipitation.
    /// </summary>
    [JsonProperty("pop")]
    public double PrecipitationProbability { get; set; }

    /// <summary>
    /// Precipitation volume in mm, can be null if not available.
    /// </summary>
    [JsonProperty("rain")]
    public double? Rain { get; set; }

    /// <summary>
    /// Snow volume in mm, can be null if not available.
    /// </summary>
    [JsonProperty("snow")]
    public double? Snow { get; set; }

    /// <summary>
    /// Information and a description of the weather.
    /// </summary>
    [JsonProperty("weather")]
    public Weather[] Description { get; set; }

    [JsonProperty("summary")]
    public string Summary { get; set; }
}