namespace P3.Driver.OpenWeather.Api.Models.OneCallModel;

/// <summary>
/// Contains information about the temperature in the day.
/// </summary>
public class DayTemperature
{
    /// <summary>
    /// The morning temperature.
    /// </summary>
    [JsonProperty("morn")]
    public double Morning { get; set; }

    /// <summary>
    /// The day temperature.
    /// </summary>
    [JsonProperty("day")]
    public double Day { get; set; }

    /// <summary>
    /// The evening temperature.
    /// </summary>
    [JsonProperty("eve")]
    public double Evening { get; set; }

    /// <summary>
    /// The night temperature.
    /// </summary>
    [JsonProperty("night")]
    public double Night { get; set; }

    /// <summary>
    /// The minimum temperature of the day.
    /// </summary>
    [JsonProperty("min")]
    public double Minimum { get; set; }

    /// <summary>
    /// The maximum temperature of the day.
    /// </summary>
    [JsonProperty("max")]
    public double Maximum { get; set; }
}