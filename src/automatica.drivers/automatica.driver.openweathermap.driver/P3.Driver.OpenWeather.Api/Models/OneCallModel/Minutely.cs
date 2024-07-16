namespace P3.Driver.OpenWeather.Api.Models.OneCallModel;

/// <summary>
/// Gets the One Call API information about a minutely forecast of the weather.
/// </summary>
public class Minutely : ForecastModel
{
    /// <summary>
    /// The precipitation in milimeters.
    /// </summary>
    [JsonProperty("precipitation")]
    public double PrecipitationVolume { get; set; }
}