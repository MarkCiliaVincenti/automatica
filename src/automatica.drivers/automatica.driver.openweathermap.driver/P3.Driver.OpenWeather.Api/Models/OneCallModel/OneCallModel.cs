namespace P3.Driver.OpenWeather.Api.Models.OneCallModel;

/// <summary>
/// Contains all of the parameters of a One Call API response.
/// </summary>
public class OneCallModel
{
    /// <summary>
    /// The latitude of the location analyzed.
    /// </summary>
    [JsonProperty("lat")]
    public double Latitude { get; set; }

    /// <summary>
    /// The longitude of the location analyzed.
    /// </summary>
    [JsonProperty("lon")]
    public double Longitude { get; set; }

    /// <summary>
    /// The name of the location's timezone.
    /// </summary>
    [JsonProperty("timezone")]
    public string TimezoneName { get; set; }

    /// <summary>
    /// Shift in seconds from UTC.
    /// </summary>
    [JsonProperty("timezone_offset")]
    public int TimezoneOffset { get; set; }

#nullable enable

    /// <summary>
    /// The current weather, can be null if excluded.
    /// </summary>
    [JsonProperty("current")]
    public Current? CurrentWeather { get; set; }

    /// <summary>
    /// A list of minutely forecasts for 1 hour, that contain only the precipitation volume. Can be null if excluded or not available.
    /// </summary>
    [JsonProperty("minutely")]
    public Minutely[]? MinutelyForecasts { get; set; }

    /// <summary>
    /// A list of hourly forecasts for 2 days. Can be null if excluded.
    /// </summary>
    [JsonProperty("hourly")]
    public Hourly[]? HourlyForecasts { get; set; }

    /// <summary>
    /// A list of daily forecasts for a week, can be null if excluded or not available.
    /// </summary>
    [JsonProperty("daily")]
    public Daily[]? DailyForecasts { get; set; }

    /// <summary>
    /// A list of national alerts from major national weather warning systems for the previous 5 days,
    /// the alerts can be either in english or in a local language. Can be null if excluded or not available.
    /// </summary>
    [JsonProperty("alerts")]
    public Alerts[]? NationalAlerts { get; set; }
}