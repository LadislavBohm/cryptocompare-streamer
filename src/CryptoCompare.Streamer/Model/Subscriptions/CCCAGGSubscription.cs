using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCompare.Streamer.Model.Subscriptions
{
    public class CCCAGGSubscription : ICryptoCompareSubscription
    {
        public CCCAGGSubscription(string fromCurrency, string currency)
        {
            if (string.IsNullOrEmpty(fromCurrency))
                throw new ArgumentException("Value cannot be null or empty.", nameof(fromCurrency));
            if (string.IsNullOrEmpty(currency))
                throw new ArgumentException("Value cannot be null or empty.", nameof(currency));
            FromCurrency = fromCurrency;
            ToCurrency = currency;
        }

        public string FromCurrency { get; }
        public string ToCurrency { get; }

        public string Format() => $"{ICryptoCompareSubscription.CCCAGGPrefix}~CCCAGG~{FromCurrency}~{ToCurrency}";
    }
}
