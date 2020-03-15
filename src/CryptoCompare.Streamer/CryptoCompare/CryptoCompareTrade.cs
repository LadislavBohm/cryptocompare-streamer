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
            internal static IReadOnlyDictionary<TradeType, int> Flags { get; } = new Dictionary<TradeType, int>
            {
                { TradeType.Sell, 0x1 },
                { TradeType.Buy, 0x2 },
                { TradeType.Unknown, 0x3 }
            };
            
            internal static IReadOnlyDictionary<string, int> Fields { get; } = new Dictionary<string, int>
            {
                { nameof(TradeEvent.Type), 0x0 },
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

            public static string Pack(TradeEvent tradeEvent)
            {
                int mask = 0;
                var sb = new StringBuilder();
                
                PackField(nameof(tradeEvent.FromCurrency), tradeEvent.FromCurrency);
                PackField(nameof(tradeEvent.ToCurrency), tradeEvent.ToCurrency);
                PackField(nameof(tradeEvent.Type), tradeEvent.Type);
                PackField(nameof(tradeEvent.Exchange), tradeEvent.Exchange);
                PackField(nameof(tradeEvent.Flags), tradeEvent.Flags);
                PackField(nameof(tradeEvent.Id), tradeEvent.Id);
                PackField(nameof(tradeEvent.Timestamp), tradeEvent.Timestamp.Ticks); //TODO: check this
                PackField(nameof(tradeEvent.Quantity), tradeEvent.Quantity);
                PackField(nameof(tradeEvent.Price), tradeEvent.Price);
                PackField(nameof(tradeEvent.Total), tradeEvent.Total);
                
                void PackField<T>(string name, T value)
                {
                    sb.Append('~');
                    sb.Append(value);
                    mask |= Fields[name];
                }

                sb.Remove(0, 1);
                sb.Append("~");
                sb.Append(mask.ToString("X"));
                return sb.ToString();
            }

            public static TradeEvent Unpack(string data)
            {
                if (string.IsNullOrEmpty(data))
                    throw new ArgumentException("Value cannot be null or empty.", nameof(data));
                
                var values = data.Split("~");
                var mask = Convert.ToInt32(values[^1], 16);

                int currentField = 0;
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

                var type = GetFieldValue(Fields[nameof(TradeEvent.Type)]);
                var exchange = GetFieldValue(Fields[nameof(TradeEvent.Exchange)]);
                var fromCurrency = GetFieldValue(Fields[nameof(TradeEvent.FromCurrency)]);
                var toCurrency = GetFieldValue(Fields[nameof(TradeEvent.ToCurrency)]);
                var flags = GetFieldValue(Fields[nameof(TradeEvent.Flags)]);
                var id = GetFieldValue(Fields[nameof(TradeEvent.Id)]);
                var timestamp = GetFieldValue(Fields[nameof(TradeEvent.Timestamp)]);
                var quantity = GetFieldValue(Fields[nameof(TradeEvent.Quantity)]);
                var price = GetFieldValue(Fields[nameof(TradeEvent.Price)]);
                var total = GetFieldValue(Fields[nameof(TradeEvent.Total)]);

                var trade = new TradeEvent(
                    id, 
                    Utils.ConvertToDateTime(long.Parse(timestamp)), 
                    exchange, 
                    fromCurrency, 
                    toCurrency,
                    int.Parse(flags), 
                    Utils.ParseDecimal(price),
                    Utils.ParseDecimal(quantity),
                    Utils.ParseDecimal(total), 
                    ParseType(type)
                );

                return trade;
            }

            private static TradeType ParseType(string type)
            {
                if (!int.TryParse(type, out var intType))
                    return TradeType.Unknown;
                if ((intType & 1) == 1) return TradeType.Sell;
                if ((intType & 2) == 2) return TradeType.Buy;
                return TradeType.Unknown;
            }
        }
    }
}