using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Automatica.Core.Driver;
using P3.Driver.OpenWeather.Api.Models.OneCallModel;

namespace P3.Driver.OpenWeatherMap.DriverFactory.Forecast.Hourly
{
    internal class ForecastHourlyNode(IDriverContext driverContext) : OpenWeatherMapDriverBaseNode(driverContext)
    {
        private readonly List<ForecastHourlyValueNode> _nodes = new();
        private int _index = -1;

        public override Task<bool> Init(CancellationToken token = new CancellationToken())
        {
            _index = GetPropertyValueInt("openweathermap-forecast-index");
            return base.Init(token);
        }

        internal override object GetValue(OneCallModel weatherResponse)
        {
            foreach (var node in _nodes)
            {
                if (weatherResponse is { HourlyForecasts: not null } && weatherResponse.HourlyForecasts.Length > _index)
                {
                    var value = node.GetValue(weatherResponse.HourlyForecasts[_index]);
                    if (value != null)
                    {
                        node.DispatchRead(value);
                    }
                }
            }

            return null;
        }

        protected override Task<bool> Read(IReadContext readContext, CancellationToken token = new CancellationToken())
        {
            return Parent.Read(token);
        }

        public override IDriverNode CreateDriverNode(IDriverContext ctx)
        {
            ForecastHourlyValueNode node = null;
            switch (ctx.NodeInstance.This2NodeTemplateNavigation.Key)
            {
                case "openweathermap-forecast-humidity":
                {
                    node = new ForecastHourlyValueNode(ctx, (y) => y.HumidityPercentage);
                    break;
                }
                case "openweathermap-forecast-pressure":
                    {
                        node = new ForecastHourlyValueNode(ctx, (y) => y.AtmosphericPressure);
                        break;
                    }
                case "openweathermap-forecast-wind-speed":
                    {
                        node = new ForecastHourlyValueNode(ctx, (y) => y.WindSpeed);
                        break;
                    }
                case "openweathermap-forecast-wind-direction":
                    {
                        node = new ForecastHourlyValueNode(ctx, (y) => y.WindDirection);
                        break;
                    }
                case "openweathermap-forecast-temperature":
                    {
                        node = new ForecastHourlyValueNode(ctx, (y) => y.Temperature);
                        break;
                    }
                case "openweathermap-forecast-clouds":
                    {
                        node = new ForecastHourlyValueNode(ctx, (y) => y.CloudPercentage);
                        break;
                    }
                case "openweathermap-forecast-clouds-description":
                    {
                        node = new ForecastHourlyValueNode(ctx, (y) => y.Description.First().Description);
                        break;
                    }
                case "openweathermap-forecast-precipitation":
                {
                    node = new ForecastHourlyValueNode(ctx, (y) => y.PrecipitationProbability);
                    break;
                    }
                case "openweathermap-forecast-from":
                {
                    node = new ForecastHourlyValueNode(ctx, (y) => DriverContext.TimeProvider.GetLocalNow().AddHours(_index).DateTime);
                    break;
                }
                case "openweathermap-forecast-rain":
                {
                    node = new ForecastHourlyValueNode(ctx, (y) => y.Rain?.PastHourVolume);
                    break;
                }
                case "openweathermap-forecast-snow":
                {
                    node = new ForecastHourlyValueNode(ctx, (y) => y.Snow?.PastHourVolume);
                    break;
                }
                case "openweathermap-forecast-temperature-perception":
                {
                    node = new ForecastHourlyValueNode(ctx, (y) => y.TemperaturePerception);
                    break;
                }
                case "openweathermap-forecast-uv-index":
                {
                    node = new ForecastHourlyValueNode(ctx, (y) => y.UVIndex);
                    break;
                }
            }
            if (node != null)
            {
                _nodes.Add(node);
            }

            return node;
        }
    }
}
