using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCompare.Streamer.Model.Subscriptions
{
    public class VolumeSubscription : ICryptoCompareSubscription
    {
        public VolumeSubscription(string currency)
        {
            if (string.IsNullOrEmpty(currency))
                throw new ArgumentException("Value cannot be null or empty.", nameof(currency));
            Currency = currency;
        }

        public string Currency { get; }

        public string Format() => $"{ICryptoCompareSubscription.VolumePrefix}~{Currency}";
    }
}
