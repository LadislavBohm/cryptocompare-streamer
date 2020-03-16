using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCompare.Streamer.Model.Subscriptions
{
    public interface ICryptoCompareSubscription
    {
        internal const string TradePrefix = "1";
        internal const string CurrentPrefix = "2";
        internal const string CCCAGGPrefix = "5";
        internal const string VolumePrefix = "11";

        string Format();
    }
}
