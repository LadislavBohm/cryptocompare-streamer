using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCompare.Streamer.Model
{
    public enum CurrentFlags
    {
        PriceUp = 0x1,
        PriceDown = 0x2,
        PriceUnchanged = 0x4,
        BidUp = 0x8,
        BidDown = 0x10,
        BidUnchanged = 0x20,
        OfferUp = 0x40,
        OfferDown = 0x80,
        OfferUnchanged = 0x100,
        AverageUp = 0x200,
        AverageDown = 0x400,
        AverageUnchanged = 0x800
    }
}
