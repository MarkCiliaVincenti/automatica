using Automatica.Core.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;
using P3.Driver.OpenWeather.Api.Models.OneCallModel;

namespace P3.Driver.OpenWeatherMap.DriverFactory
{
    internal abstract class OpenWeatherMapDriverBaseNode(IDriverContext driverContext)
        : DriverNotWriteableBase(driverContext)
    {
        internal abstract object GetValue(OneCallModel weatherResponse);

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
