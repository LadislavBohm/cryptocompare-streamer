using System.Runtime.InteropServices.ComTypes;
using CryptoCompare.Streamer.Model;

namespace CryptoCompare.Streamer.CryptoCompare
{
    internal static partial class CCC
    {
        internal static class Volume
        {
            internal static bool TryUnpack(string dataString, out VolumeEvent volume)
            {
                volume = default;
                if (string.IsNullOrEmpty(dataString)) return false;

                var parts = dataString.Split('~');
                if (parts.Length != 3) return false;

                volume = new VolumeEvent(parts[1], Utils.ParseDecimal(parts[2]));
                return true;
            }
        }
    }
}
