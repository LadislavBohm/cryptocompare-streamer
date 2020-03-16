using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CryptoCompare.Streamer.Test.Model
{
    internal class Called<T> : CalledBase, IDisposable
    {
        private readonly IDisposable _subscription;
        
        internal Called(IObservable<T> observable, Action<T> action = null)
        {
            _subscription = observable.Subscribe(data =>
            {
                action?.Invoke(data);
                Increment();
            });
        }

        public void Dispose()
        {
            Debug.WriteLine("Disposing Called and it's subscription.");
            _subscription?.Dispose();
        }
    }
}
