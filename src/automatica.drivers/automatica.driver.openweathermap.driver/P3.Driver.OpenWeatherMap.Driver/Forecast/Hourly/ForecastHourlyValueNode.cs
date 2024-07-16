using System;
using System.Threading;
using System.Threading.Tasks;
using Automatica.Core.Driver;

namespace P3.Driver.OpenWeatherMap.DriverFactory.Forecast.Hourly
{
    internal class ForecastHourlyValueNode(IDriverContext driverContext, Func<OpenWeather.Api.Models.OneCallModel.Hourly, object> detailValueFunc) : DriverNotWriteableBase(driverContext)
    {
        internal object GetValue(OpenWeather.Api.Models.OneCallModel.Hourly daily)
        {
            return detailValueFunc.Invoke(daily);
        }

        protected override Task<bool> Read(IReadContext readContext, CancellationToken token = new CancellationToken())
        {
            return Parent.Read(token);
        }

        public override IDriverNode CreateDriverNode(IDriverContext ctx)
        {
            return null;
        }
    }
}
