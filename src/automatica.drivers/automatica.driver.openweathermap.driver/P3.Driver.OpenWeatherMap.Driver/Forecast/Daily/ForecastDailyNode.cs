using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Automatica.Core.Driver;
using P3.Driver.OpenWeather.Api.Models.OneCallModel;

namespace P3.Driver.OpenWeatherMap.DriverFactory.Forecast.Daily
{
    internal class ForecastDailyNode(IDriverContext driverContext) : OpenWeatherMapDriverBaseNode(driverContext)
    {
        private readonly List<ForecastDailyValueNode> _nodes = new();
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
                if (weatherResponse is { DailyForecasts: not null } && weatherResponse.DailyForecasts.Length > _index)
                {
                    var value = node.GetValue(weatherResponse.DailyForecasts[_index]);
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
            ForecastDailyValueNode node = null;
            switch (ctx.NodeInstance.This2NodeTemplateNavigation.Key)
            {
                case "openweathermap-forecast-humidity":
                    {
                        node = new ForecastDailyValueNode(ctx, (y) => y.HumidityPercentage);
                        break;
                    }
                case "openweathermap-forecast-pressure":
                    {
                        node = new ForecastDailyValueNode(ctx, (y) => y.AtmosphericPressure);
                        break;
                    }
                case "openweathermap-forecast-wind-speed":
                    {
                        node = new ForecastDailyValueNode(ctx, (y) => y.WindSpeed);
                        break;
                    }
                case "openweathermap-forecast-wind-direction":
                    {
                        node = new ForecastDailyValueNode(ctx, (y) => y.WindDirection);
                        break;
                    }
                case "openweathermap-forecast-temperature":
                    {
                        node = new ForecastDailyValueNode(ctx, (y) => y.Temperature.Day);
                        break;
                    }
                case "openweathermap-forecast-temperature-min":
                    {
                        node = new ForecastDailyValueNode(ctx, (y) => y.Temperature.Minimum);
                        break;
                    }
                case "openweathermap-forecast-temperature-max":
                    {
                        node = new ForecastDailyValueNode(ctx, (y) => y.Temperature.Maximum);
                        break;
                    }
                case "openweathermap-forecast-clouds":
                    {
                        node = new ForecastDailyValueNode(ctx, (y) => y.CloudPercentage);
                        break;
                    }
                case "openweathermap-forecast-clouds-description":
                    {
                        node = new ForecastDailyValueNode(ctx, (y) => y.Description.First().Description);
                        break;
                    }
                case "openweathermap-forecast-precipitation":
                    {
                        node = new ForecastDailyValueNode(ctx, (y) => y.PrecipitationProbability);
                        break;
                    }
                case "openweathermap-forecast-precipitation-description":
                    {
                        node = new ForecastDailyValueNode(ctx, (y) => y.Summary);
                        break;
                    }
                case "openweathermap-forecast-from":
                    {
                        node = new ForecastDailyValueNode(ctx, (y) => y.AnalysisDate);
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
