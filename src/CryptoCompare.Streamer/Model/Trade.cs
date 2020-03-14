using System;
using System.Text;

namespace CryptoCompare.Streamer.Model
{
    public class Trade
    {
        public Trade(string id,
                     DateTime timestamp,
                     string exchange,
                     string fromCurrency,
                     string toCurrency,
                     int flags,
                     decimal price,
                     decimal quantity,
                     decimal total,
                     TradeType type)
        {
            Id = id;
            Timestamp = timestamp;
            Exchange = exchange;
            FromCurrency = fromCurrency;
            ToCurrency = toCurrency;
            Flags = flags;
            Price = price;
            Quantity = quantity;
            Total = total;
            Type = type;
        }
        
        public string Id { get; internal set; }
        public DateTime Timestamp { get; internal set; }
        public string Exchange { get; internal set; }
        public string FromCurrency { get; internal set; }
        public string ToCurrency { get; internal set; }
        public int Flags { get; internal set; }
        public decimal Price { get; internal set; }
        public decimal Quantity { get; internal set; }
        public decimal Total { get; internal set; }
        public TradeType Type { get; internal set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Exchange);
            sb.Append(" | ");
            sb.Append(Type);
            sb.Append(" | P: ");
            sb.Append(Price);
            sb.Append(" | Q: ");
            sb.Append(Quantity);
            sb.Append(" | T: ");
            sb.Append(Total);

            return sb.ToString();
        }
    }
}