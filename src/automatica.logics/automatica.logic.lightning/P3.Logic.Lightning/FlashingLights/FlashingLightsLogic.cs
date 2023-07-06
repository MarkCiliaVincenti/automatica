﻿using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Automatica.Core.Base.IO;
using Automatica.Core.EF.Models;
using Automatica.Core.Logic;
using Microsoft.Extensions.Logging;

namespace P3.Logic.Lightning.FlashingLights
{
    internal class FlashingLightsLogic : Automatica.Core.Logic.Logic
    {
        private bool _currentState;

        private readonly RuleInterfaceInstance _output;
        private readonly Timer _timer;
        
        private bool _timerRunning;

        public FlashingLightsLogic(ILogicContext context) : base(context)
        {
            _output = context.RuleInstance.RuleInterfaceInstance.SingleOrDefault(a =>
                a.This2RuleInterfaceTemplate == FlashingLightsLogicFactory.Output);

            var delay = context.RuleInstance.RuleInterfaceInstance.SingleOrDefault(a =>
                               a.This2RuleInterfaceTemplate == FlashingLightsLogicFactory.Delay);

            _timer = new Timer(delay.ValueInteger.Value);
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var setState = !_currentState;

            Context.Logger.LogInformation($"Reset light state to {setState}");

            Context.Dispatcher.DispatchValue(new LogicOutputChanged(_output, !_currentState).Instance, setState);

            _timer.Stop();
            _timerRunning = false;
        }

        protected override IList<ILogicOutputChanged> InputValueChanged(RuleInterfaceInstance instance, IDispatchable source, object value)
        {
            if (instance.This2RuleInterfaceTemplate == FlashingLightsLogicFactory.Trigger && (value is true))
            {
                var setState = !_currentState;

                Context.Logger.LogInformation($"Set light state to {setState}");

                Context.Dispatcher.DispatchValue(new LogicOutputChanged(_output, _currentState).Instance, setState);

                _currentState = setState;
                _timerRunning = true;
                _timer.Start();
            }
            else if (instance.This2RuleInterfaceTemplate == FlashingLightsLogicFactory.State)
            {
                if (!_timerRunning)
                {
                    _currentState = (bool)value;
                    Context.Logger.LogInformation($"Current state is {_currentState}");
                }
            }

            return new List<ILogicOutputChanged>();
        }
    }
}