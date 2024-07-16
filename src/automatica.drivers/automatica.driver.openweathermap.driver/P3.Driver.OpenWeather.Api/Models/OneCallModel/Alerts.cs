namespace P3.Driver.OpenWeather.Api.Models.OneCallModel;

/// <summary>
/// Contains national weather alerts from major national weather warning systems of the area.
/// Might be in english, or in the local language.
/// </summary>
public class Alerts
{
    /// <summary>
    /// The name of the sender of the alert.
    /// </summary>
    [JsonProperty("sender_name")]
    public string Sender { get; set; }

    /// <summary>
    /// The event of the alert.
    /// </summary>
    [JsonProperty("event")]
    public string Event { get; set; }

    /// <summary>
    /// The date and time of the start of the alert in unix seconds, UTC.
    /// </summary>
    [JsonProperty("start")]
    public long StartUnix { get; set; }

    /// <summary>
    /// The date and time of the start of the alert, UTC.
    /// </summary>
    public DateTime Start
    {
        get { return DateTimeOffset.FromUnixTimeSeconds(StartUnix).UtcDateTime; }
    }

    /// <summary>
    /// The date and time of the end of the alert in unix seconds, UTC.
    /// </summary>
    [JsonProperty("end")]
    public long EndUnix { get; set; }

    /// <summary>
    /// The date and time of the end of the alert, UTC.
    /// </summary>
    public DateTime End
    {
        get { return DateTimeOffset.FromUnixTimeSeconds(EndUnix).UtcDateTime; }
    }

    /// <summary>
    /// The alert.
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }
    
    /// <summary>
    /// Tags of the alert.
    /// </summary>
    [JsonProperty("tags")]
    public string[] Tags { get; set; }
}