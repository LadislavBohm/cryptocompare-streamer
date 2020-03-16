using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCompare.Streamer.Model
{
    public class VolumeEvent
    {
        public VolumeEvent(string currency, decimal volume)
        {
            Currency = currency;
            Volume = volume;
        }

        public string Currency { get; }
        public decimal Volume { get; set; }

        public override string ToString()
        {
            return $"{Currency} - {Volume}";
        }
    }
}
