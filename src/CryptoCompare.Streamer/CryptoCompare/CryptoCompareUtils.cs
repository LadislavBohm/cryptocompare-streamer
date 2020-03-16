using System;
using System.Globalization;

namespace CryptoCompare.Streamer.CryptoCompare
{
    internal static partial class CCC
    {
        internal static class Utils
        {
            internal static decimal ParseDecimal(string value)
            {
                return decimal.Parse(value, NumberStyles.Float, null);
            }

            internal static bool TryParseDecimal(string value, out decimal number)
            {
                return decimal.TryParse(value, NumberStyles.Float, null, out number);
            }

            internal static decimal? ParseDecimalOrNull(string value)
            {
                if (value == null) return null;
                return ParseDecimal(value);
            }

            internal static bool TryParseDecimalOrNull(string value, out decimal? number)
            {
                number = default;
                if (value == null) return false;
                if (!TryParseDecimal(value, out var result)) return false;
                number = result;
                return true;
            }

            internal static string ConvertToMegaBytes(long bytes) => $"{Math.Round((double)bytes / 1024 / 1024, 2)} MB";

            internal static DateTime ConvertToDateTime(long timestamp)
            {
                var date = new DateTime(1970, 01, 01, 0, 0, 0, DateTimeKind.Utc);
                return date.AddMilliseconds(timestamp * 1000);
            }

            internal static bool TryConvertToDateTime(string value, out DateTime date)
            {
                date = default;
                if (value == null) return false;
                if (!long.TryParse(value, out var timestamp)) return false;
                date = ConvertToDateTime(timestamp);
                return true;
            }
        }
    }
}
