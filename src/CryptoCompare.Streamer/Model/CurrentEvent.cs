using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCompare.Streamer.Model
{
    public class CurrentEvent
    {
        public CurrentEvent(string exchange, string fromCurrency, string currency, CurrentFlags flags, decimal? price,
            decimal? bid, decimal? offer, DateTime? lastUpdate, decimal? average, decimal? lastVolume,
            decimal? lastVolumeTo, string lastTradeId, decimal? volumeHour, decimal? volumeHourTo,
            decimal? volume24Hour, decimal? volume24HourTo, decimal? openHour, decimal? highHour, decimal? lowHour,
            decimal? open24Hour, decimal? high24Hour, decimal? low24Hour, string lastMarket)
        {
            Exchange = exchange;
            FromCurrency = fromCurrency;
            ToCurrency = currency;
            Flags = flags;
            Price = price;
            Bid = bid;
            Offer = offer;
            LastUpdate = lastUpdate;
            Average = average;
            LastVolume = lastVolume;
            LastVolumeTo = lastVolumeTo;
            LastTradeId = lastTradeId;
            VolumeHour = volumeHour;
            VolumeHourTo = volumeHourTo;
            Volume24Hour = volume24Hour;
            Volume24HourTo = volume24HourTo;
            OpenHour = openHour;
            HighHour = highHour;
            LowHour = lowHour;
            Open24Hour = open24Hour;
            High24Hour = high24Hour;
            Low24Hour = low24Hour;
            LastMarket = lastMarket;
        }

        public string Exchange { get; }
        public string FromCurrency { get; }
        public string ToCurrency { get; }
        public CurrentFlags Flags { get; }
        public decimal? Price { get; }
        public decimal? Bid { get; }
        public decimal? Offer { get; }
        public DateTime? LastUpdate { get; }
        public decimal? Average { get; }
        public decimal? LastVolume { get; }
        public decimal? LastVolumeTo { get; }
        public string LastTradeId { get; }
        public decimal? VolumeHour { get; }
        public decimal? VolumeHourTo { get; }
        public decimal? Volume24Hour { get; }
        public decimal? Volume24HourTo { get; }
        public decimal? OpenHour { get; }
        public decimal? HighHour { get; }
        public decimal? LowHour { get; }
        public decimal? Open24Hour { get; }
        public decimal? High24Hour { get; }
        public decimal? Low24Hour { get; }
        public string LastMarket { get; }
    }
}
