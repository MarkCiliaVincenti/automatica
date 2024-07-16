using Knx.Falcon.Logging;
using Microsoft.Extensions.Logging;

namespace P3.Driver.Knx.DriverFactory.Logging
{
    public class FalconLoggerFactory(ILogger logger) : IFalconLoggerFactory
    {
        public IFalconLogger GetLogger(string name)
        {
            return new FalconLogger(logger, name);
        }
    }
}
