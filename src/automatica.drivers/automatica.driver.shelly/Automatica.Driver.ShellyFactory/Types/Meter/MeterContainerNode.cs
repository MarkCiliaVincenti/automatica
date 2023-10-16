﻿using System.Threading;
using System.Threading.Tasks;
using Automatica.Core.Driver;
using Automatica.Driver.Shelly.Gen1.Dtos;

namespace Automatica.Driver.ShellyFactory.Types.Meter
{
    internal class MeterContainerNode : ShellyContainerNode<object>
    {
        public MeterContainerNode(IDriverContext driverContext, ShellyDriverDevice client) : base(driverContext, client)
        {
        }


        internal override Task<object> GetCorrespondingDataObject()
        {
            throw new System.NotImplementedException();
        }

        protected override Task Write(object value, IWriteContext writeContext, CancellationToken token = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        protected override Task<bool> Read(IReadContext readContext, CancellationToken token = new CancellationToken())
        {
            return Task.FromResult(false);
        }

        protected override IDriverNode CreateDriverNodeInternal(IDriverContext ctx)
        {
            var type = ctx.NodeInstance.This2NodeTemplateNavigation.Key;

            switch (type)
            {
                case "meter":
                    return new MeterContainerNode(ctx, Client);
                case "meter-power":
                    break;
                case "meter-overpower":
                    break;
                case "meter-is-valid":
                    break;
                case "meter-last-timestamp":
                    break;
                case "meter-total":
                    break;
            }

            return null;
        }
    }
}
