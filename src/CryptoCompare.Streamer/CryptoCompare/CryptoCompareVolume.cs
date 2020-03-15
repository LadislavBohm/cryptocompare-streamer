using CryptoCompare.Streamer.Model;

namespace CryptoCompare.Streamer.CryptoCompare
{
    internal static partial class CCC
    {
        internal static class Volume
        {
            internal static VolumeEvent Unpack(string dataString)
            {
                var parts = dataString.Split('~');
                return new VolumeEvent(parts[1], Utils.ParseDecimal(parts[2]));
            }
        }
    }
}
