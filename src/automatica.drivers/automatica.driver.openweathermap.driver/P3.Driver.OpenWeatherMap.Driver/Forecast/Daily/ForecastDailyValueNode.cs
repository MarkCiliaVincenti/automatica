using Automatica.Core.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace P3.Driver.OpenWeatherMap.DriverFactory.Forecast.Daily
{
    internal class ForecastDailyValueNode(IDriverContext driverContext, Func<OpenWeather.Api.Models.OneCallModel.Daily, object> detailValueFunc) : DriverNotWriteableBase(driverContext)
    {
        internal object GetValue(OpenWeather.Api.Models.OneCallModel.Daily daily)
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
