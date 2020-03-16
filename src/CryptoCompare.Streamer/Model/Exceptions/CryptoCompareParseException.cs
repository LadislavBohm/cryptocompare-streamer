using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCompare.Streamer.Model.Exceptions
{
    public class CryptoCompareParseException : Exception
    {
        public CryptoCompareParseException(string data, Exception inner) : base("Error while parsing data from CryptoCompare.", inner)
        {
            ParsedData = data;
        }

        public string ParsedData { get; }
    }
}
