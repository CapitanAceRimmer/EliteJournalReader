using System;
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

        public override JournalEventArgs FireEvent(object sender, JObject eventJObj)
        {
            var eventArgs = eventJObj.ToObject<TJournalEventArgs>();
            eventArgs.OriginalEvent = eventJObj;
            eventArgs.Timestamp = DateTime.Parse(eventJObj.Value<string>("timestamp"),
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

            eventArgs.PostProcess(eventJObj);


            Fired?.Invoke(sender, eventArgs);

            return eventArgs;
        }

        public override TJournalEventArgs GetEvent(JObject eventJObj)
        {
            var eventArgs = eventJObj.ToObject<TJournalEventArgs>();
            eventArgs.OriginalEvent = eventJObj;
            eventArgs.Timestamp = DateTime.Parse(eventJObj.Value<string>("timestamp"),
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
            eventArgs.PostProcess(eventJObj);

            return eventArgs as TJournalEventArgs;
        }

    }
}