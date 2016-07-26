using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace EliteJournalReader.Events
{
    //When Written: when selling goods in the market
    //Parameters:
    //�	Type: cargo type
    //�	Count: number of units
    //�	SellPrice: price per unit
    //�	TotalSale: total sale value
    //�	AvgPricePaid: average price paid
    //�	IllegalGoods: (not always present) whether goods are illegal here
    //�	StolenGoods: (not always present) whether goods were stolen
    //�	BlackMarket: (not always present) whether selling in a black market
    public class MarketSellEvent : JournalEvent<MarketSellEvent.MarketSellEventArgs>
    {
        public MarketSellEvent() : base("MarketSell") { }

        public class MarketSellEventArgs : JournalEventArgs
        {
            public override void Initialize(JObject evt)
            {
                base.Initialize(evt);
                Type = evt.Value<string>("Type");
                Count = evt.Value<int>("Count");
                SellPrice = evt.Value<int>("SellPrice");
                TotalSale = evt.Value<int>("TotalSale");
                AvgPricePaid = evt.Value<int>("AvgPricePaid");
                IllegalGoods = evt.Value<bool?>("IllegalGoods");
                StolenGoods = evt.Value<bool?>("StolenGoods");
                BlackMarket = evt.Value<bool?>("BlackMarket");
            }

            public string Type { get; set; }
            public int Count { get; set; }
            public int SellPrice { get; set; }
            public int TotalSale { get; set; }
            public int AvgPricePaid { get; set; }
            public bool? IllegalGoods { get; set; }
            public bool? StolenGoods { get; set; }
            public bool? BlackMarket { get; set; }
        }
    }
}
