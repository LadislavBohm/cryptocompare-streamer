﻿using System;
using System.Collections.Generic;
using System.Text;
using CryptoCompare.Streamer.Model;
using Xunit;
using CCC = CryptoCompare.Streamer.CryptoCompare.CCC;

namespace CryptoCompare.Streamer.Test
{
    public class TradeParsingTests
    {
        [Fact]
        public void Thore_UnknownTrade_ShouldParse()
        {
            var packed = "0~thore~BTC~USD~1~1583904070~4.98~7989~39785.22~1584196827~1859~7e";
            Assert.True(CCC.Trade.TryUnpack(packed, out var trade));

            Assert.Equal("thore", trade.Exchange);
            Assert.Equal("BTC", trade.FromCurrency);
            Assert.Equal("USD", trade.ToCurrency);
            Assert.Equal(TradeFlags.Unknown, trade.Flags);
            Assert.Null(trade.Id);
        }
    }
}
