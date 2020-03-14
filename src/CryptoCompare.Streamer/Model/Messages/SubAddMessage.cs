using System.Collections.Generic;
using System.Linq;

namespace CryptoCompare.Streamer.Model.Messages
{
    public class SubAddMessage
    {
        public SubAddMessage(string sub) : this(Enumerable.Repeat(sub, 1)) {}
        public SubAddMessage(IEnumerable<string> subs) : this(subs.ToArray()) {}
        public SubAddMessage(params string[] subs)
        {
            this.Subs = subs;
        }
        
        public IReadOnlyList<string> Subs { get; }
    }
}