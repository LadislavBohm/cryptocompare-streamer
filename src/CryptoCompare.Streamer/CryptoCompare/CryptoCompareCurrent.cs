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
            
            internal static bool TryUnpack(string data, out CurrentEvent current)
            {
                current = null;
                if (string.IsNullOrEmpty(data)) return false;

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
                if (!TryParseFlags(GetFieldValue(Fields[nameof(CurrentEvent.Flags)]), out var flags))
                    return false;
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.Price)]), out var price);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.Bid)]), out var bid);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.Offer)]), out var offer);
                Utils.TryConvertToDateTime(GetFieldValue(Fields[nameof(CurrentEvent.LastUpdate)]), out var lastUpdate);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.Average)]), out var average);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.LastVolume)]), out var lastVolume);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.LastVolumeTo)]), out var lastVolumeTo);
                var lastTradeId = GetFieldValue(Fields[nameof(CurrentEvent.LastTradeId)]);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.VolumeHour)]), out var volumeHour);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.VolumeHourTo)]), out var volumeHourTo);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.Volume24Hour)]), out var volume24Hour);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.Volume24HourTo)]), out var volume24HourTo);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.OpenHour)]), out var openHour);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.HighHour)]), out var highHour);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.LowHour)]), out var lowHour);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.Open24Hour)]), out var open24Hour);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.High24Hour)]), out var high24Hour);
                Utils.TryParseDecimalOrNull(GetFieldValue(Fields[nameof(CurrentEvent.Low24Hour)]), out var low24Hour);
                var lastMarket = GetFieldValue(Fields[nameof(CurrentEvent.LastMarket)]);

                current = new CurrentEvent(
                    exchange, 
                    fromCurrency, 
                    toCurrency, 
                    flags,
                    price,
                    bid,
                    offer,
                    lastUpdate,
                    average,
                    lastVolume,
                    lastVolumeTo,
                    lastTradeId,
                    volumeHour,
                    volumeHourTo,
                    volume24Hour,
                    volume24HourTo,
                    openHour,
                    highHour,
                    lowHour,
                    open24Hour,
                    high24Hour,
                    low24Hour,
                    lastMarket);

                return true;
            }

            private static bool TryParseFlags(string value, out CurrentFlags flags)
            {
                flags = default;
                if (string.IsNullOrEmpty(value)) return false;
                if (!int.TryParse(value, out var intFlags)) return false;
                flags = (CurrentFlags) intFlags;
                return true;
            }
        }
    }
}
