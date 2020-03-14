using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using CryptoCompare.Streamer.Model;
using CryptoCompare.Streamer.Utils;

namespace CryptoCompare.Streamer.Constants
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
                { nameof(Model.Trade.Type), 0x0 },
                { nameof(Model.Trade.Exchange), 0x0 },
                { nameof(Model.Trade.FromCurrency), 0x0 },
                { nameof(Model.Trade.ToCurrency), 0x0 },
                { nameof(Model.Trade.Flags), 0x0 },
                { nameof(Model.Trade.Id), 0x1 },
                { nameof(Model.Trade.Timestamp), 0x2 },
                { nameof(Model.Trade.Quantity), 0x4 },
                { nameof(Model.Trade.Price), 0x8 },
                { nameof(Model.Trade.Total), 0x10 }
            };

            public static string Pack(Model.Trade trade)
            {
                int mask = 0;
                var sb = new StringBuilder();
                
                PackField(nameof(trade.FromCurrency), trade.FromCurrency);
                PackField(nameof(trade.ToCurrency), trade.ToCurrency);
                PackField(nameof(trade.Type), trade.Type);
                PackField(nameof(trade.Exchange), trade.Exchange);
                PackField(nameof(trade.Flags), trade.Flags);
                PackField(nameof(trade.Id), trade.Id);
                PackField(nameof(trade.Timestamp), trade.Timestamp.Ticks); //TODO: check this
                PackField(nameof(trade.Quantity), trade.Quantity);
                PackField(nameof(trade.Price), trade.Price);
                PackField(nameof(trade.Total), trade.Total);
                
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

            public static Model.Trade Unpack(string tradeString)
            {
                if (string.IsNullOrEmpty(tradeString))
                    throw new ArgumentException("Value cannot be null or empty.", nameof(tradeString));

                var values = tradeString.Split("~");
                var mask = Convert.ToInt32(values[^1], 16);
                var trade = new Model.Trade(
                    ValidateOrThrow(Fields[nameof(Model.Trade.Id)], values[5]),
                    ValidateOrThrow(Fields[nameof(Model.Trade.Timestamp)], CryptoCompareUtils.ConvertToDateTime(long.Parse(values[6]))),
                    ValidateOrThrow(Fields[nameof(Model.Trade.Exchange)], values[1]),
                    ValidateOrThrow(Fields[nameof(Model.Trade.FromCurrency)], values[2]),
                    ValidateOrThrow(Fields[nameof(Model.Trade.ToCurrency)], values[3]),
                    ValidateOrThrow(Fields[nameof(Model.Trade.Flags)], int.Parse(values[4])),
                    ValidateOrThrow(Fields[nameof(Model.Trade.Price)], ParseDecimal(values[8])),
                    ValidateOrThrow(Fields[nameof(Model.Trade.Quantity)], ParseDecimal(values[7])),
                    ValidateOrThrow(Fields[nameof(Model.Trade.Total)], ParseDecimal(values[9])),
                    ValidateOrThrow(Fields[nameof(Model.Trade.Type)], ParseType(values[4])) //TODO: verify that this value should be parsed like this
                );
                
                T ValidateOrThrow<T>(int field, T value)
                {
                    if (field != 0 && (mask & field) != field) 
                        throw new ArgumentException($"Field value: {field} is invalid. Value: {value}");
                    return value;
                }

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

            private static decimal ParseDecimal(string value)
            {
                return decimal.Parse(value, NumberStyles.Float, null);
            }
        }
    }
}