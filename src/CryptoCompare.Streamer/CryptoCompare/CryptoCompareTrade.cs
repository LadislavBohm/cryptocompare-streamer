using System;
using System.Collections.Generic;
using System.Text;
using CryptoCompare.Streamer.Model;

namespace CryptoCompare.Streamer.CryptoCompare
{
    internal static partial class CCC
    {
        internal static class Trade
        {
            internal static IReadOnlyDictionary<string, int> Fields { get; } = new Dictionary<string, int>
            {
                { nameof(TradeEvent.Exchange), 0x0 },
                { nameof(TradeEvent.FromCurrency), 0x0 },
                { nameof(TradeEvent.ToCurrency), 0x0 },
                { nameof(TradeEvent.Flags), 0x0 },
                { nameof(TradeEvent.Id), 0x1 },
                { nameof(TradeEvent.Timestamp), 0x2 },
                { nameof(TradeEvent.Quantity), 0x4 },
                { nameof(TradeEvent.Price), 0x8 },
                { nameof(TradeEvent.Total), 0x10 }
            };

            public static bool TryUnpack(string data, out TradeEvent trade)
            {
                trade = null;
                if (string.IsNullOrEmpty(data)) return false;

                var values = data.Split("~");
                var mask = Convert.ToInt32(values[^1], 16);

                //start from 1 to skip type that we are not interested in
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

                var exchange = GetFieldValue(Fields[nameof(TradeEvent.Exchange)]);
                var fromCurrency = GetFieldValue(Fields[nameof(TradeEvent.FromCurrency)]);
                var toCurrency = GetFieldValue(Fields[nameof(TradeEvent.ToCurrency)]);
                if (!TryGetFlags(GetFieldValue(Fields[nameof(TradeEvent.Flags)]), out var flags))
                    return false;
                var id = GetFieldValue(Fields[nameof(TradeEvent.Id)]);
                if (!Utils.TryConvertToDateTime(GetFieldValue(Fields[nameof(TradeEvent.Timestamp)]), out var timestamp))
                    return false;
                if (!Utils.TryParseDecimal(GetFieldValue(Fields[nameof(TradeEvent.Quantity)]), out var quantity))
                    return false;
                if (!Utils.TryParseDecimal(GetFieldValue(Fields[nameof(TradeEvent.Price)]), out var price))
                    return false;
                if (!Utils.TryParseDecimal(GetFieldValue(Fields[nameof(TradeEvent.Total)]), out var total))
                    return false;

                trade = new TradeEvent(id, timestamp, exchange, fromCurrency, toCurrency, flags, price, quantity, total);
                return true;
            }

            private static bool TryGetFlags(string value, out TradeFlags flags)
            {
                flags = default;
                if (value == null) return false;
                if (!int.TryParse(value, out var numberFlags)) return false;
                flags = (TradeFlags) numberFlags;
                return true;
            }
        }
    }
}