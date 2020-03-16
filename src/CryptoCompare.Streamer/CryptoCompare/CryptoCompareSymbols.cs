using System.Collections.Generic;

namespace CryptoCompare.Streamer.CryptoCompare
{
    internal static partial class CCC
    {
        public static IReadOnlyDictionary<string, string> Symbols { get; } = new Dictionary<string, string>
        {
            { "BTC", "Ƀ" },
            { "LTC", "Ł" },
            { "DAO", "Ð" }
        };
    }
}