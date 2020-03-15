using System;
using System.Collections.Generic;
using CryptoCompare.Streamer.Model;

namespace CryptoCompare.Streamer.CryptoCompare
{
    internal static partial class CCC
    {
        internal static class Current
        {
            internal static IReadOnlyDictionary<string, int> Fields { get; } = new Dictionary<string, int>
            {
                { nameof(CurrentEvent.Exchange), 0x0 },
                { nameof(CurrentEvent.FromCurrency), 0x0 },
                { nameof(CurrentEvent.ToCurrency), 0x0 },
                { nameof(CurrentEvent.Flags), 0x0 },
                { nameof(CurrentEvent.Price), 0x1 },
                { nameof(CurrentEvent.Bid), 0x2 },
                { nameof(CurrentEvent.Offer), 0x4 },
                { nameof(CurrentEvent.LastUpdate), 0x8 },
                { nameof(CurrentEvent.Average), 0x10 },
                { nameof(CurrentEvent.LastVolume), 0x20 },
                { nameof(CurrentEvent.LastVolumeTo), 0x40 },
                { nameof(CurrentEvent.LastTradeId), 0x80 },
                { nameof(CurrentEvent.VolumeHour), 0x100 },
                { nameof(CurrentEvent.VolumeHourTo), 0x200 },
                { nameof(CurrentEvent.Volume24Hour), 0x400 },
                { nameof(CurrentEvent.Volume24HourTo), 0x800 },
                { nameof(CurrentEvent.OpenHour), 0x1000 },
                { nameof(CurrentEvent.HighHour), 0x2000 },
                { nameof(CurrentEvent.LowHour), 0x4000 },
                { nameof(CurrentEvent.Open24Hour), 0x8000 },
                { nameof(CurrentEvent.High24Hour), 0x10000 },
                { nameof(CurrentEvent.Low24Hour), 0x20000 },
                { nameof(CurrentEvent.LastMarket), 0x40000 }
            };
            
            internal static CurrentEvent Unpack(string data)
            {
                if (string.IsNullOrEmpty(data))
                    throw new ArgumentException("Value cannot be null or empty.", nameof(data));

                var values = data.Split("~");
                var mask = Convert.ToInt32(values[^1], 16);

                //start from 1 to skip first type that we don't care about
                int currentField = 1;
                string GetFieldValue(int field)
                {
                    string value = null;
                    if (field == 0)
                    {
                        value = values[currentField];
                        currentField++;
                    }
                    else if ((mask & field) > 0)
                    {
                        value = values[currentField];
                        currentField++;
                    }
                    return value;
                }

                var exchange = GetFieldValue(Fields[nameof(CurrentEvent.Exchange)]);
                var fromCurrency = GetFieldValue(Fields[nameof(CurrentEvent.FromCurrency)]);
                var toCurrency = GetFieldValue(Fields[nameof(CurrentEvent.ToCurrency)]);
                var flags = GetFieldValue(Fields[nameof(CurrentEvent.Flags)]);
                var price = GetFieldValue(Fields[nameof(CurrentEvent.Price)]);
                var bid = GetFieldValue(Fields[nameof(CurrentEvent.Bid)]);
                var offer = GetFieldValue(Fields[nameof(CurrentEvent.Offer)]);
                var lastUpdate = GetFieldValue(Fields[nameof(CurrentEvent.LastUpdate)]);
                var average = GetFieldValue(Fields[nameof(CurrentEvent.Average)]);
                var lastVolume = GetFieldValue(Fields[nameof(CurrentEvent.LastVolume)]);
                var lastVolumeTo = GetFieldValue(Fields[nameof(CurrentEvent.LastVolumeTo)]);
                var lastTradeId = GetFieldValue(Fields[nameof(CurrentEvent.LastTradeId)]);
                var volumeHour = GetFieldValue(Fields[nameof(CurrentEvent.VolumeHour)]);
                var volumeHourTo = GetFieldValue(Fields[nameof(CurrentEvent.VolumeHourTo)]);
                var volume24Hour = GetFieldValue(Fields[nameof(CurrentEvent.Volume24Hour)]);
                var volume24HourTo = GetFieldValue(Fields[nameof(CurrentEvent.Volume24HourTo)]);
                var openHour = GetFieldValue(Fields[nameof(CurrentEvent.OpenHour)]);
                var highHour = GetFieldValue(Fields[nameof(CurrentEvent.HighHour)]);
                var lowHour = GetFieldValue(Fields[nameof(CurrentEvent.LowHour)]);
                var open24Hour = GetFieldValue(Fields[nameof(CurrentEvent.Open24Hour)]);
                var high24Hour = GetFieldValue(Fields[nameof(CurrentEvent.High24Hour)]);
                var low24Hour = GetFieldValue(Fields[nameof(CurrentEvent.Low24Hour)]);
                var lastMarket = GetFieldValue(Fields[nameof(CurrentEvent.LastMarket)]);

                var current = new CurrentEvent(
                    exchange, 
                    fromCurrency, 
                    toCurrency, 
                    (CurrentFlags) int.Parse(flags),
                    Utils.ParseDecimalOrNull(price),
                    Utils.ParseDecimalOrNull(bid),
                    Utils.ParseDecimalOrNull(offer),
                    lastUpdate == null ? null : (DateTime?)Utils.ConvertToDateTime(long.Parse(lastUpdate)),
                    Utils.ParseDecimalOrNull(average),
                    Utils.ParseDecimalOrNull(lastVolume),
                    Utils.ParseDecimalOrNull(lastVolumeTo),
                    lastTradeId,
                    Utils.ParseDecimalOrNull(volumeHour),
                    Utils.ParseDecimalOrNull(volumeHourTo),
                    Utils.ParseDecimalOrNull(volume24Hour),
                    Utils.ParseDecimalOrNull(volume24HourTo),
                    Utils.ParseDecimalOrNull(openHour),
                    Utils.ParseDecimalOrNull(highHour),
                    Utils.ParseDecimalOrNull(lowHour),
                    Utils.ParseDecimalOrNull(open24Hour),
                    Utils.ParseDecimalOrNull(high24Hour),
                    Utils.ParseDecimalOrNull(low24Hour),
                    lastMarket);

                return current;
            }
        }
    }
}
