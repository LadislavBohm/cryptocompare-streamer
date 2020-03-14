using System;
using System.Collections.Generic;
using System.Text;
using CryptoCompare.Streamer.Test.Model;

namespace CryptoCompare.Streamer.Test.Helpers
{
    internal static class ReactiveTestExtensions
    {
        internal static Called<T> SubscribeCalled<T>(this IObservable<T> observable, Action<T> action = null)
        {
            return new Called<T>(observable, action);
        }
    }
}
