using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCompare.Streamer.Model.Subscriptions
{
    public interface ICryptoCompareSubscription
    {
        char Prefix { get; }

        string Format();
    }
}
