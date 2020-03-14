using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoCompare.Streamer.Model.Messages
{
    public class SubRemoveMessage
    {
        public SubRemoveMessage(string sub) : this(Enumerable.Repeat(sub, 1)) { }
        public SubRemoveMessage(IEnumerable<string> subs) : this(subs.ToArray()) { }
        public SubRemoveMessage(params string[] subs)
        {
            this.Subs = subs;
        }

        public IReadOnlyList<string> Subs { get; }
    }
}
