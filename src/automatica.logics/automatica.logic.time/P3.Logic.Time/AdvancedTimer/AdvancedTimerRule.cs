﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Automatica.Core.Base.Calendar;
using Automatica.Core.Base.IO;
using Automatica.Core.EF.Models;
using Automatica.Core.Logic;
using Microsoft.Extensions.Logging;
using P3.Logic.Time.Timer;

namespace P3.Logic.Time.AdvancedTimer
{
    public class AdvancedTimerRule : Automatica.Core.Logic.Logic
    {
        private readonly RuleInterfaceInstance _timerProperty;
        private CalendarPropertyData _timerPropertyData;
        private readonly RuleInterfaceInstance _output;
        private readonly System.Timers.Timer _timer;
        private readonly object _lock = new object();
        

        private bool _value = false;

        public AdvancedTimerRule(ILogicContext context) : base(context)
        {
            _timerProperty = context.RuleInstance.RuleInterfaceInstance.SingleOrDefault(a =>
                a.This2RuleInterfaceTemplate == AdvancedTimerRuleFactory.RuleTimerParameter);

            _output = context.RuleInstance.RuleInterfaceInstance.SingleOrDefault(a =>
                a.This2RuleInterfaceTemplate == AdvancedTimerRuleFactory.RuleOutput);

            _timer = new System.Timers.Timer();

        }

        protected override Task<bool> Start(RuleInstance ruleInstance, CancellationToken token = new CancellationToken())
        {
            if (_timerProperty?.Value != null)
            {
                _timerPropertyData = (CalendarPropertyData)_timerProperty.Value;

            }
            else
            {
                Context.Logger.LogError("No or invalid timer property found");
                return Task.FromResult(false);
            }

            
            CalculateTickTime(true);
            


            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
            return base.Start(ruleInstance, token);
        }

        private void CalculateTickTime(bool isStartup=false)
        {
            if (_timerPropertyData == null || _timerPropertyData.Value == null)
            {
                return;
            }
            
            var tickTimes = new List<double>();
            var list = new List<(DateTime startTime, DateTime endTime)>();
            
            foreach (var entry in _timerPropertyData.Value)
            {
                list.Add(GetStartEndTime(entry));
            }
            
            list = list.OrderBy(a => a.startTime).ToList();

            if (list.Any())
            {
                var next = list.First();
                if (next.startTime.IsToday())
                {
                    CalculateTickTime(next.startTime, next.endTime, isStartup);
                }
            }

        }

        private void CalculateTickTime(DateTime start, DateTime end, bool isStartup = false)
        {
            var now = DateTime.Now;
            var nowTime = now.TimeOfDay;

            Context.Logger.LogInformation($"Now is {nowTime}");


            var startTime = start.TimeOfDay;
            var stopTime = end.TimeOfDay;


            var tickTime = startTime - nowTime;
            Context.Logger.LogDebug($"Start time is {startTime} endTime is {stopTime} difference is {tickTime}");
            double timerTickTime;

            if (tickTime.TotalMilliseconds < 0)
            {

                timerTickTime = (stopTime - nowTime).TotalMilliseconds;
                if (timerTickTime < 0)
                {
                    _timer.Interval = (new DateTime(now.Year, now.Month, now.Day, 23, 59, 59, 99).TimeOfDay - nowTime).TotalMilliseconds;
                    Context.Logger.LogDebug($"Timer {Context.RuleInstance.Name}: Next tick time is {_timer.Interval}ms at {stopTime}");
                    if (isStartup)
                    {
                        Context.Logger.LogInformation($"Start event, set value to {false}");
                        Context.Dispatcher.DispatchValue(new LogicOutputChanged(_output, false).Instance, false);
                    }
                    _value = false;
                    return;
                }
                if (isStartup)
                {
                    Context.Logger.LogInformation($"Start event, set value to {true}");
                    Context.Dispatcher.DispatchValue(new LogicOutputChanged(_output, true).Instance, true);
                }

                _value = true;
            }
            else
            {
                timerTickTime = tickTime.TotalMilliseconds;
                if (isStartup)
                {
                    Context.Logger.LogInformation($"Start event, set value to {false}");
                    Context.Dispatcher.DispatchValue(new LogicOutputChanged(_output, false).Instance, false);
                }

                _value = false;
            }

            _timer.Interval = timerTickTime;
            Context.Logger.LogDebug($"Timer {Context.RuleInstance.Name}: Next tick time is {_timer.Interval}ms at {startTime}");
            _timer.Start();
        }

        private (DateTime startTime, DateTime endTime) GetStartEndTime(CalendarPropertyDataEntry entry)
        {
            if (entry.AllDay)
            {
                return (DateTime.Now.StartOfDay(), DateTime.Now.EndOfDay());
            }

            if (String.IsNullOrEmpty(entry.RecurrenceRule))
            {
                return (entry.StartDate, entry.EndDate);
            }

            var rfcStart = new RFC2445Recur(entry.StartDate, entry.RecurrenceRule);

            if (!rfcStart.Freq.IsValid)
            {
                Context.Logger.LogWarning($"Rule is not valid {entry.RecurrenceRule}");
                return (DateTime.MaxValue, DateTime.MaxValue);
            }

            var startDates = rfcStart.Iterate(Direction.Forward);

            if (startDates != null)
            {
                startDates = startDates.Where(a => a > DateTime.Now).OrderBy(time => time);
            }

            var endDates = new RFC2445Recur(entry.EndDate, entry.RecurrenceRule).Iterate(Direction.Forward);
            if (endDates != null)
            {
                endDates = endDates.Where(a => a > DateTime.Now).OrderBy(time => time);
            }

            if (startDates != null && endDates != null && startDates.Any() && endDates.Any())
            {
                var startDate = startDates.FirstOrDefault();
                var endDate = endDates.FirstOrDefault();
                return (startDate, endDate);
            }
            return (DateTime.MaxValue, DateTime.MaxValue);
        }

        protected override Task<bool> Stop(RuleInstance ruleInstance, CancellationToken token = default)
        {
            _timer.Elapsed -= _timer_Elapsed;
            _timer.Stop();
            return base.Stop(ruleInstance, token);
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Stop();
            lock (_lock)
            {
                if (_timerPropertyData == null)
                {
                    return;
                }

                _value = !_value;
                Context.Dispatcher.DispatchValue(new LogicOutputChanged(_output, _value).Instance, _value);

                Context.Logger.LogInformation($"Tick received, set value to {_value}");

                CalculateTickTime();
            }
        }

        protected override IList<ILogicOutputChanged> InputValueChanged(RuleInterfaceInstance instance,
            IDispatchable source, object value)
        {

            return new List<ILogicOutputChanged>();
        }
    }
}
