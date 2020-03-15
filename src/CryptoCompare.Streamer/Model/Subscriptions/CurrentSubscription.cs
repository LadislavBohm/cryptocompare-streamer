using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCompare.Streamer.Model.Subscriptions
{
    public class CurrentSubscription : ICryptoCompareSubscription
    {
        /// <summary>
        /// Creates instance from sub received from CryptoCompare REST API.
        /// </summary>
        /// <param name="sub">Expected format: 0~Bitstamp~BTC~USD</param>
        public CurrentSubscription(string sub)
        {
            if (string.IsNullOrEmpty(sub)) throw new ArgumentException("Value cannot be null or empty.", nameof(sub));
            if (sub.StartsWith(ICryptoCompareSubscription.CurrentPrefix))
                throw new ArgumentException($"Sub must start with '{ICryptoCompareSubscription.CurrentPrefix}'");

            var parts = sub.Split("~");
            if (parts.Length != 4) throw new ArgumentException("Sub is in invalid format.");

            Exchange = parts[1];
            FromCurrency = parts[2];
            ToCurrency = parts[3];
        }

        public CurrentSubscription(string exchange, string fromCurrency, string currency)
        {
            if (string.IsNullOrEmpty(exchange))
                throw new ArgumentException("Value cannot be null or empty.", nameof(exchange));
            if (string.IsNullOrEmpty(fromCurrency))
                throw new ArgumentException("Value cannot be null or empty.", nameof(fromCurrency));
            if (string.IsNullOrEmpty(currency))
                throw new ArgumentException("Value cannot be null or empty.", nameof(currency));

            Exchange = exchange;
            FromCurrency = fromCurrency;
            ToCurrency = currency;
        }


        public string Exchange { get; }
        public string FromCurrency { get; }
        public string ToCurrency { get; }

        public string Format() => $"{ICryptoCompareSubscription.CurrentPrefix}~{Exchange}~{FromCurrency}~{ToCurrency}";
    }
}
