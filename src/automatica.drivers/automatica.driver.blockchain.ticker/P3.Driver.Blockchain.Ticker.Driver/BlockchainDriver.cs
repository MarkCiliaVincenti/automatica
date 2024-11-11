﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Automatica.Core.Driver;
using Microsoft.Extensions.Logging;
using P3.Driver.Blockchain.Ticker.Driver.Bitcoin;
using P3.Driver.Blockchain.Ticker.Driver.Cardano;
using P3.Driver.Blockchain.Ticker.Driver.Ethereum;
using Timer = System.Threading.Timer;

namespace P3.Driver.Blockchain.Ticker.Driver
{
    internal class BlockchainDriver : DriverNoneAttributeBase
    {
        private Timer _timer;
      
        private readonly List<CoinNode> _nodes = new List<CoinNode>();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly ILogger _logger;
        private int _pollTime;

        public BlockchainDriver(IDriverContext driverContext) : base(driverContext)
        {
            _logger = driverContext.Logger;
        }

        public override Task<bool> Init(CancellationToken token = default)
        {
            var pollTime = GetPropertyValueInt("poll");
            _pollTime = pollTime;
            return base.Init(token);
        }

        private async void TimeElapsed(object state)
        {
            try
            {
                if (await _semaphore.WaitAsync(TimeSpan.FromSeconds(1)))
                {
                    await ReadValues();
                }
            }
            catch (Exception ex)
            {
                DriverContext.Logger.LogError(ex, "Error read values...");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public override async Task<bool> Start(CancellationToken token = default)
        {
            _timer = new Timer(TimeElapsed, this, _pollTime * 1000, _pollTime * 1000);

            _logger.LogInformation($"Start polling every {_pollTime}s");
            await ReadValues(token);

            return await base.Start(token);
        }

      
        protected override async Task<bool> Read(IReadContext readContext, CancellationToken token = new CancellationToken())
        {
            await ReadValues(token);
            return true;
        }

        private async Task ReadValues(CancellationToken token = default)
        {
            _logger.LogDebug($"Poll values...");

            foreach (var node in _nodes)
            {
                await node.Refresh(token);
            }
        }

        public override Task<bool> Stop(CancellationToken token = default)
        {
            _timer.Dispose();
            return base.Stop(token);
        }

        public override IDriverNode CreateDriverNode(IDriverContext ctx)
        {
            CoinNode node = null;
            switch(ctx.NodeInstance.This2NodeTemplateNavigation.Key)
            {
                case "blockchain-btc":
                    node = new BitcoinNode(ctx);
                    break;
                case "blockchain-eth":
                    node = new EthereumNode(ctx);
                    break;
                case "blockchain-ada":
                    node = new CardanoNode(ctx);
                    break;
            }

            if(node != null)
            {
                _nodes.Add(node);
            }

            return node;
        }
    }
}
