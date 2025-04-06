﻿using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace EliteJournalReader
{
    public abstract class JournalEvent
    {
        public string[] EventNames { get; }

        public string OriginalEvent { get; protected set; }

        protected JournalEvent(params string[] eventNames)
        {
            EventNames = eventNames;
        }

        public abstract JournalEventArgs FireEvent(object sender, JObject evt);

        public abstract JournalEventArgs GetEvent(JObject evt);
    }

    public abstract class JournalEvent<TJournalEventArgs> : JournalEvent
        where TJournalEventArgs : JournalEventArgs, new()
    {
        public event EventHandler<TJournalEventArgs> Fired;

        protected JournalEvent(params string[] eventNames) : base(eventNames)
        {
        }

        public void AddHandler(EventHandler<TJournalEventArgs> eventHandler) => Fired += eventHandler;

        public void RemoveHandler(EventHandler<TJournalEventArgs> eventHandler) => Fired -= eventHandler;

        public override JournalEventArgs FireEvent(object sender, JObject evt)
        {
            var eventArgs = evt.ToObject<TJournalEventArgs>();
            eventArgs.OriginalEvent = evt;
            eventArgs.Timestamp = DateTime.Parse(evt.Value<string>("timestamp"),
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

            eventArgs.PostProcess(evt);


            Fired?.Invoke(sender, eventArgs);

            return eventArgs;
        }

        public override JournalEventArgs GetEvent(JObject evt)
        {
            var eventArgs = evt.ToObject<TJournalEventArgs>();
            eventArgs.OriginalEvent = evt;
            eventArgs.Timestamp = DateTime.Parse(evt.Value<string>("timestamp"),
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

            eventArgs.PostProcess(evt);

           
            return eventArgs;
        }

    }
}