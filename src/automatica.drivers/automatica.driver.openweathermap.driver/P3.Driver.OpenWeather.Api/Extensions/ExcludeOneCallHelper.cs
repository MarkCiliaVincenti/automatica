using P3.Driver.OpenWeather.Api.Enums;

namespace P3.Driver.OpenWeather.Api.Extensions;

internal static class ExcludeOneCallHelper
{
    private static readonly string[] options = { "current", "minutely", "hourly", "daily", "alerts" };

    internal static string Convert(this ExcludeOneCall option) => options[(int)option];
}