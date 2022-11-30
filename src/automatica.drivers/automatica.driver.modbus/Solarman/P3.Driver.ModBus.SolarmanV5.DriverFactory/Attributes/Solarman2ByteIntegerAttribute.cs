﻿using System;
using System.Threading.Tasks;
using Automatica.Core.Driver;
using P3.Driver.ModBusDriver.Master;

namespace P3.Driver.ModBus.SolarmanV5.DriverFactory.Attributes
{
    internal class Solarman2ByteIntegerAttribute : SolarmanAttrribute
    {
        public Solarman2ByteIntegerAttribute(IDriverContext driverContext, SolarmanDriver parent) : base(driverContext, parent)
        {
        }

        public override async Task<object> ConvertValue(ModBusRegisterValueReturn modbusReturn)
        {
            await Task.CompletedTask;

            int val = modbusReturn.Data[0];

            if (Offset > 0)
            {
                val -= Offset;
            }

            if (Scale > 1.0 || Scale < 1.0)
            {
                return Math.Round(val * Scale, 2);
            }


            return val;
        }
    }
}