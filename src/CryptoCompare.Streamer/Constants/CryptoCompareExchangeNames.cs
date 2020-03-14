using System.Collections.Generic;

namespace CryptoCompare.Streamer.Constants
{
    internal static partial class CCC
    {
        public static IReadOnlyDictionary<string, string> ExchangeNames { get; } = new Dictionary<string, string>
        {
            { "CCCAGG", "CryptoCompare Index" },
            { "BTCChina", "BTCC" }
        };
    }
}