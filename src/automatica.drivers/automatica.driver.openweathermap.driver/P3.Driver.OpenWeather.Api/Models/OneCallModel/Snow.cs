namespace P3.Driver.OpenWeather.Api.Models.OneCallModel;

/// <summary>
/// Information about the snow.
/// </summary>
public class Snow
{
    /// <summary>
    /// Snow volume for last hour, mm.
    /// </summary>
    [JsonProperty("1h")]
    public double? PastHourVolume { get; set; }
}