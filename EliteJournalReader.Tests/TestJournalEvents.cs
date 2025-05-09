﻿using System;
using EliteJournalReader.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EliteJournalReader.Tests
{
    [TestClass]
    [TestCategory("Journal Events")]
    public class TestJournalEvents
    {
        private FakeJournalWatcher watcher;
        
        [TestInitialize]
        public void Initialize()
        {
            watcher = new FakeJournalWatcher();
            watcher.StartWatching();
        }

        [TestCleanup]
        public void CleanUp()
        {
            watcher.StopWatching();
            watcher = null;
        }

        [TestMethod]
        public void Test_FileheaderEvent()
        {
            FileheaderEvent.FileheaderEventArgs args = null;
            //{ "timestamp":"2017-12-30T13:36:28Z", "event":"Fileheader", "part":1, "language":"English\\UK", "gameversion":"2.4", "build":"r160439/r0 " }
            watcher.GetEvent<FileheaderEvent>().Fired += (s, e) => args = e;
            watcher.FireFakeEvent(@"{ ""timestamp"":""2017-12-30T13:36:28Z"", ""event"":""Fileheader"", ""part"":1, ""language"":""English\\UK"", ""gameversion"":""2.4"", ""build"":""r160439/r0 "" }");

            Assert.IsNotNull(args);
            Assert.AreEqual("English\\UK", args.Language);
        }

        [TestMethod]
        public void Test_DiedEvent_Single()
        {
            //{ "timestamp":"2016-12-05T19:14:42Z", "event":"Died", "KillerName":"Wightman", "KillerShip":"diamondback", "KillerRank":"Expert" }
            DiedEvent.DiedEventArgs args = null;
            watcher.GetEvent<DiedEvent>().Fired += (s, e) => args = e;
            watcher.FireFakeEvent(@"{ ""timestamp"":""2016-12-05T19:14:42Z"", ""event"":""Died"", ""KillerName"":""Wightman"", ""KillerShip"":""diamondback"", ""KillerRank"":""Expert"" }");

            Assert.IsNotNull(args);
            Assert.AreEqual(1, args.Killers.Length);
            Assert.AreEqual("Wightman", args.Killers[0].Name);
        }

        [TestMethod]
        public void Test_DiedEvent_Wing()
        {
            //{ "timestamp":"2016-12-14T21:55:24Z", "event":"Died", "Killers":[ { "Name":"Cmdr Ekol Tieja", "Ship":"federation_gunship", "Rank":"Elite" }, { "Name":"Cmdr Yellowboze", "Ship":"federation_gunship", "Rank":"Novice" } ] }
            DiedEvent.DiedEventArgs args = null;
            watcher.GetEvent<DiedEvent>().Fired += (s, e) => args = e;
            watcher.FireFakeEvent(@"{ ""timestamp"":""2016-12-14T21:55:24Z"", ""event"":""Died"", ""Killers"":[ { ""Name"":""Cmdr Ekol Tieja"", ""Ship"":""federation_gunship"", ""Rank"":""Elite"" }, { ""Name"":""Cmdr Yellowboze"", ""Ship"":""federation_gunship"", ""Rank"":""Novice"" } ] }");

            Assert.IsNotNull(args);
            Assert.AreEqual(2, args.Killers.Length);
            Assert.AreEqual("Cmdr Ekol Tieja", args.Killers[0].Name);
            Assert.AreEqual("Cmdr Yellowboze", args.Killers[1].Name);
        }


        [TestMethod]
        public void Test_Old_Style_ScanEvent()
        {
            //{ "timestamp":"2016-12-14T21:55:24Z", "event":"Died", "Killers":[ { "Name":"Cmdr Ekol Tieja", "Ship":"federation_gunship", "Rank":"Elite" }, { "Name":"Cmdr Yellowboze", "Ship":"federation_gunship", "Rank":"Novice" } ] }
            ScanEvent.ScanEventArgs args = null;
            watcher.GetEvent<ScanEvent>().Fired += (s, e) => args = e;
            string scanEvent = "{ \"timestamp\":\"2016-10-28T16:37:01Z\", \"event\":\"Scan\", \"BodyName\":\"Synuefe HD-J c25-3 1\", \"DistanceFromArrivalLS\":24.219051, \"TidalLock\":true, \"TerraformState\":\"\", \"PlanetClass\":\"High metal content body\", \"Atmosphere\":\"\", \"Volcanism\":\"\", \"MassEM\":0.046179, \"Radius\":2322928.750000, \"SurfaceGravity\":3.410974, \"SurfaceTemperature\":790.957092, \"SurfacePressure\":0.000000, \"Landable\":true, \"Materials\":{ \"iron\":22.0, \"nickel\":16.6, \"sulphur\":15.6, \"carbon\":13.1, \"chromium\":9.9, \"phosphorus\":8.4, \"zinc\":6.0, \"germanium\":4.6, \"cadmium\":1.7, \"niobium\":1.5, \"polonium\":0.6 }, \"SemiMajorAxis\":7260646400.000000, \"Eccentricity\":0.000006, \"OrbitalInclination\":0.000063, \"Periapsis\":182.233215, \"OrbitalPeriod\":415573.281250, \"RotationPeriod\":417737.500000 }";
            watcher.FireFakeEvent(scanEvent);

            Assert.IsNotNull(args);
            //Assert.AreEqual(2, args.Killers.Length);
            //Assert.AreEqual("Cmdr Ekol Tieja", args.Killers[0].Name);
            //Assert.AreEqual("Cmdr Yellowboze", args.Killers[1].Name);
        }


        [TestMethod]
        public void Test_EngineerApplyEvent()
        {
            //{ "timestamp":"2016-12-14T21:55:24Z", "event":"Died", "Killers":[ { "Name":"Cmdr Ekol Tieja", "Ship":"federation_gunship", "Rank":"Elite" }, { "Name":"Cmdr Yellowboze", "Ship":"federation_gunship", "Rank":"Novice" } ] }
            EngineerApplyEvent.EngineerApplyEventArgs args = null;
            watcher.GetEvent<EngineerApplyEvent>().Fired += (s, e) => args = e;
            string @event = "{ \"timestamp\":\"2018-02-25T18:00:02Z\", \"event\":\"EngineerApply\", \"Engineer\":\"Ram Tah\", \"Blueprint\":\"ChaffLauncher_ChaffCapacity\", \"Level\":3 }";
            watcher.FireFakeEvent(@event);

            Assert.IsNotNull(args);
            //Assert.AreEqual(2, args.Killers.Length);
            //Assert.AreEqual("Cmdr Ekol Tieja", args.Killers[0].Name);
            //Assert.AreEqual("Cmdr Yellowboze", args.Killers[1].Name);
        }

        [TestMethod]
        public void Test_NavRoute()
        {
            NavRouteEvent.NavRouteEventArgs args = null;
            watcher.GetEvent<NavRouteEvent>().Fired += (s, e) => args = e;
            watcher.FireFakeEvent(@"{ ""timestamp"":""2020-04-27T08:02:52Z"", ""event"":""NavRoute"", ""Route"":[ 
                { ""StarSystem"":""i Bootis"", ""SystemAddress"":1281787693419, ""StarPos"":[-22.37500,34.84375,4.00000], ""StarClass"":""G"" }, 
                { ""StarSystem"":""Acihaut"", ""SystemAddress"":11665802405289, ""StarPos"":[-18.50000,25.28125,-4.00000], ""StarClass"":""M"" }, 
                { ""StarSystem"":""LHS 455"", ""SystemAddress"":3686969379179, ""StarPos"":[-16.90625,10.21875,-3.43750], ""StarClass"":""DQ"" }, 
                { ""StarSystem"":""SPF-LF 1"", ""SystemAddress"":22661187052961, ""StarPos"":[2.90625,6.31250,-9.56250], ""StarClass"":""M"" }, 
                { ""StarSystem"":""Luyten's Star"", ""SystemAddress"":7268024264097, ""StarPos"":[6.56250,2.34375,-10.25000], ""StarClass"":""M"" }] }
                ");

            Assert.IsNotNull(args);
            Assert.AreEqual(5, args.Route.Length);
            Assert.AreEqual("i Bootis", args.Route[0].StarSystem);
            Assert.AreEqual("DQ", args.Route[2].StarClass);
            Assert.AreEqual(-10.25m, args.Route[4].StarPos.Z);
        }
    }
}
