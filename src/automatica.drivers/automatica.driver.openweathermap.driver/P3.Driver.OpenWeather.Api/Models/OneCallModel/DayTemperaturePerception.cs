namespace P3.Driver.OpenWeather.Api.Models.OneCallModel;

/// <summary>
/// Contains information about the temperature of the day, accounting for the human perception of temperature.
/// </summary>
public class DayTemperaturePerception
{
    /// <summary>
    /// The morning temperature, accounting for the human perception of temperature.
    /// </summary>
    [JsonProperty("morn")]
    public double Morning { get; set; }

    /// <summary>
    /// The day temperature, accounting for the human perception of temperature.
    /// </summary>
    [JsonProperty("day")]
    public double Day { get; set; }

    /// <summary>
    /// The evening temperature, accounting for the human perception of temperature.
    /// </summary>
    [JsonProperty("eve")]
    public double Evening { get; set; }

    /// <summary>
    /// The night temperature, accounting for the human perception of temperature.
    /// </summary>
    [JsonProperty("night")]
    public double Night { get; set; }
}