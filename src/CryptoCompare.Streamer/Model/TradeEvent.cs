using System;
using System.Text;

namespace CryptoCompare.Streamer.Model
{
    public class TradeEvent
    {
        public TradeEvent(string id,
                     DateTime timestamp,
                     string exchange,
                     string fromCurrency,
                     string toCurrency,
                     TradeFlags flags,
                     decimal price,
                     decimal quantity,
                     decimal total)
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
        }
        
        public string Id { get; }
        public DateTime Timestamp { get; }
        public string Exchange { get; }
        public string FromCurrency { get; }
        public string ToCurrency { get; }
        public TradeFlags Flags { get; }
        public decimal Price { get; }
        public decimal Quantity { get; }
        public decimal Total { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Exchange);
            sb.Append(" | ");
            sb.Append(Flags);
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