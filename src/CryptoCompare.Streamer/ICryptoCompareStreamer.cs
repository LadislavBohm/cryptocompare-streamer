using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using CryptoCompare.Streamer.Model;

namespace CryptoCompare.Streamer
{
    public interface ICryptoCompareStreamer
    {
        IObservable<Trade> OnTrade { get; }

        Task StartAsync();
        Task StopAsync();

        IObservable<Unit> SubscribeToTrades(string exchange, string fromCurrency, string toCurrency);
        IObservable<Unit> SubscribeToTrades(string sub);
        IObservable<Unit> SubscribeToTrades(IEnumerable<string> subs);
        IObservable<Unit> SubscribeToTrades(params string[] subs);

        IObservable<Unit> UnsubscribeFromTrades(string exchange, string fromCurrency, string toCurrency);
        IObservable<Unit> UnsubscribeFromTrades(string sub);
        IObservable<Unit> UnsubscribeFromTrades(IEnumerable<string> subs);
        IObservable<Unit> UnsubscribeFromTrades(params string[] subs);
    }
}