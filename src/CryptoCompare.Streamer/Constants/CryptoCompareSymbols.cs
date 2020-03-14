using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CryptoCompare.Streamer.Constants
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