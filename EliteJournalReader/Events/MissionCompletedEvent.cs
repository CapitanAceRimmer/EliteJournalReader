using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace EliteJournalReader.Events
{
    //When Written: when a mission is completed
    //Parameters:
    //�	Name: mission type
    //�	Faction: faction name
    //Optional parameters (depending on mission type)
    //�	Commodity
    //�	Count
    //�	Target
    //�	TargetType
    //�	TargetFaction
    //�	Reward: value of reward
    //�	Donation: donation offered (for altruism missions)
    //�	PermitsAwarded:[] (names of any permits awarded, as a JSON array)
    public class MissionCompletedEvent : JournalEvent<MissionCompletedEvent.MissionCompletedEventArgs>
    {
        public MissionCompletedEvent() : base("MissionCompleted") { }

        public class MissionCompletedEventArgs : JournalEventArgs
        {
            public override void Initialize(JObject evt)
            {
                base.Initialize(evt);
                Name = evt.Value<string>("Name");
                Faction = evt.Value<string>("Faction");
                Commodity = evt.Value<string>("Commodity");
                Count = evt.Value<int?>("Count");
                Target = evt.Value<string>("Target");
                TargetType = evt.Value<string>("TargetType");
                TargetFaction = evt.Value<string>("TargetFaction");
                Reward = evt.Value<int?>("Reward") ?? 0;
                Donation = evt.Value<int?>("Donation");
                PermitsAwarded = evt.Value<JArray>("PermitsAwarded")?.Values<string>().ToArray();
                MissionId = evt.Value<int>("MissionID");
            }

            public string Name { get; set; }
            public string Faction { get; set; }
            public string Commodity { get; set; }
            public int? Count { get; set; }
            public string Target { get; set; }
            public string TargetType { get; set; }
            public string TargetFaction { get; set; }
            public int Reward { get; set; }
            public int? Donation { get; set; }
            public string[] PermitsAwarded { get; set; }
            public int MissionId { get; set; }
        }
    }
}
