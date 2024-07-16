namespace P3.Driver.OpenWeather.Api.Models.OneCallModel
{
    public class ForecastModel
    { 
        [JsonProperty("dt")]
        public long AnalysisDateUnix { get; set; }

        public DateTime AnalysisDate => DateTimeOffset.FromUnixTimeSeconds(AnalysisDateUnix).UtcDateTime;
    }
}
