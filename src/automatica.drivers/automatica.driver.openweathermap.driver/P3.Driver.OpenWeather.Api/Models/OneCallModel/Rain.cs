namespace P3.Driver.OpenWeather.Api.Models.OneCallModel;

/// <summary>
/// Information about the rain.
/// </summary>
public class Rain
{
    /// <summary>
    /// Rain volume for last hour, mm.
    /// </summary>
    [JsonProperty("1h")]
    public double? PastHourVolume { get; set; }
}