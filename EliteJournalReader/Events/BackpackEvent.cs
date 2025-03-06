using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace EliteJournalReader.Events
{
    public class BackpackEvent : JournalEvent<BackpackEvent.BackpackEventArgs>
    {
        public BackpackEvent() : base("Backpack") { }

        public class BackpackEventArgs : JournalEventArgs
        {
            public Item[] Items { get; set; }
            public ScanItemComponent[] Components { get; set; }
            public Consumable[] Consumables { get; set; }
            public Data[] Data { get; set; }
        }
    }
}