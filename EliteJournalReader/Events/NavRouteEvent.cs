using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EliteJournalReader.Events
{
    /// <summary>
    /// When plotting a multi-star route, the file �NavRoute.json� is written in the same directory as the journal, with a list of stars along that route
    /// Example:
    /// { "timestamp":"2020-04-27T08:02:52Z", "event":"Route", "Route":[ 
    ///     { "StarSystem":"i Bootis", "SystemAddress":1281787693419, "StarPos":[-22.37500,34.84375,4.00000], "StarClass":"G" }, 
    ///     { "StarSystem":"Acihaut", "SystemAddress":11665802405289, "StarPos":[-18.50000,25.28125,-4.00000], "StarClass":"M" }, 
    ///     { "StarSystem":"LHS 455", "SystemAddress":3686969379179, "StarPos":[-16.90625,10.21875,-3.43750], "StarClass":"DQ" }, 
    ///     { "StarSystem":"SPF-LF 1", "SystemAddress":22661187052961, "StarPos":[2.90625, 6.31250, -9.56250], "StarClass":"M" }, 
    ///     { "StarSystem":"Luyten's Star", "SystemAddress":7268024264097, "StarPos":[6.56250, 2.34375, -10.25000], "StarClass":"M" }
    /// ] }
    /// Note this may be changed for final 3.7 release to use tag name �SystemAddress� instead of �StarSystem� for better consistency
    /// </summary>
    public class NavRouteEvent : JournalEvent<NavRouteEvent.NavRouteEventArgs>
    {
        public NavRouteEvent() : base("NavRoute") { }

        public class NavRouteEventArgs : JournalEventArgs
        {
            public string StarSystem { get; set; }
            public RouteElement[] Route { get; set; }
            public long SystemAddress { get; set; }

            [JsonConverter(typeof(SystemPositionConverter))]
            public SystemPosition StarPos { get; set; }

            public string StarClass { get; set; }

            //public override void PostProcess(JObject evt, JournalWatcher journalWatcher)
            //{
            //    // The actual route is written to NavRoute.json, so let's try to read it
            //    try
            //    {
            //        string path = Path.Combine(journalWatcher.Path, "NavRoute.json");
            //        if (File.Exists(path))
            //        {
            //            string text = File.ReadAllText(path);
            //            var navRoute = JObject.Parse(text).ToObject<NavRouteEventArgs>();
            //            Route = navRoute.Route;
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        Trace.TraceWarning("Error reading NavRoute.json: " + e.Message);
            //        throw e;
            //    }
            //}
        }
    }
}