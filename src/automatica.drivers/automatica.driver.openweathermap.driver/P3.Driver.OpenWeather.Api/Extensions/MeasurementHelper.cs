using P3.Driver.OpenWeather.Api.Enums;

namespace P3.Driver.OpenWeather.Api.Extensions;

internal static class MeasurementHelper
{
    private static readonly string[] measurementSystems = { "standard", "metric", "imperial" };
    internal static string Convert(this Measurement system) => measurementSystems[(int)system];
}