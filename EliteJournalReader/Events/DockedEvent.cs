using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace EliteJournalReader.Events
{
    //When written: when landing at landing pad in a space station, outpost, or surface settlement
    //Parameters:
    //�	StationName: name of station
    //�	StationType: type of station
    //�	StarSystem: name of system
    //�	CockpitBreach:true (only if landing with breached cockpit)
    //�	Faction: station�s controlling faction
    //�	FactionState
    //�	Economy
    //�	Government
    //�	Security
    public class DockedEvent : JournalEvent<DockedEvent.DockedEventArgs>
    {
        public DockedEvent() : base("Docked") { }

        public class DockedEventArgs : JournalEventArgs
        {
            public override void Initialize(JObject evt)
            {
                base.Initialize(evt);
                StationName = evt.Value<string>("StationName");
                StationType = evt.Value<string>("StationType");
                CockpitBreach = evt.Value<bool?>("CockpitBreach");
                Faction = evt.Value<string>("Faction");
                FactionState = evt.Value<string>("FactionState");
                Economy = evt.Value<string>("Economy");
                Economy_Localised = evt.Value<string>("Economy_Localised");
                Government = evt.Value<string>("Government");
                Government_Localised = evt.Value<string>("Government_Localised");
                Security = evt.Value<string>("Security");
                Security_Localised = evt.Value<string>("Security_Localised");
            }

            public string StationName { get; set; }
            public string StationType { get; set; }
            public bool? CockpitBreach { get; set; }
            public string Faction { get; set; }
            public string FactionState { get; set; }
            public string Economy { get; set; }
            public string Economy_Localised { get; set; }
            public string Government { get; set; }
            public string Government_Localised { get; set; }
            public string Security { get; set; }
            public string Security_Localised { get; set; }
        }
    }
}
