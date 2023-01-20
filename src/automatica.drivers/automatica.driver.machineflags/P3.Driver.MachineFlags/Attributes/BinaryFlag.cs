﻿using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Automatica.Core.Base.IO;
using Automatica.Core.Driver;
using Microsoft.Extensions.Logging;

[assembly: InternalsVisibleTo("P3.Driver.MachineFlags.Tests")]

namespace P3.Driver.MachineFlags.Attributes
{
    public class BinaryFlag : DriverBase
    {

        private bool? _value;
        public BinaryFlag(IDriverContext ctx) : base(ctx)
        {

        }

        public override Task WriteValue(IDispatchable source, object value)
        {
           
            try
            {
                var bValue = Convert.ToBoolean(value);
                if (_value == bValue)
                {
                    return Task.CompletedTask;
                }

                _value = bValue;
                DriverContext.Logger.LogDebug($"WriteValue {bValue}");

                DispatchValue(bValue);
            }
            catch (Exception ex)
            {
                DriverContext.Logger.LogError(ex, $"Could not convert value to bool {ex}");
            }

           
            return Task.CompletedTask;
        }

        public override IDriverNode CreateDriverNode(IDriverContext ctx)
        {
            throw new InvalidOperationException();
        }
    }
}