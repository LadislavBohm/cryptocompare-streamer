using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using CryptoCompare.Streamer.Model;
using CryptoCompare.Streamer.Model.Subscriptions;

namespace CryptoCompare.Streamer
{
    public interface ICryptoCompareStreamer
    {
        IObservable<Trade> OnTrade { get; }

        Task StartAsync();
        Task StopAsync();

        IObservable<Unit> Subscribe<TSubscription>(TSubscription subscription) where TSubscription : ICryptoCompareSubscription;
        IObservable<Unit> Subscribe<TSubscription>(IEnumerable<TSubscription> subscriptions) where TSubscription : ICryptoCompareSubscription;
        IObservable<Unit> Subscribe<TSubscription>(params TSubscription[] subscriptions) where TSubscription : ICryptoCompareSubscription;

        IObservable<Unit> Unsubscribe<TSubscription>(TSubscription subscription) where TSubscription : ICryptoCompareSubscription;
        IObservable<Unit> Unsubscribe<TSubscription>(IEnumerable<TSubscription> subscriptions) where TSubscription : ICryptoCompareSubscription;
        IObservable<Unit> Unsubscribe<TSubscription>(params TSubscription[] subscriptions) where TSubscription : ICryptoCompareSubscription;

        //IObservable<Unit> SubscribeToTrades(string exchange, string fromCurrency, string toCurrency);
        //IObservable<Unit> SubscribeToTrades(string sub);
        //IObservable<Unit> SubscribeToTrades(IEnumerable<string> subs);
        //IObservable<Unit> SubscribeToTrades(params string[] subs);

        //IObservable<Unit> UnsubscribeFromTrades(string exchange, string fromCurrency, string toCurrency);
        //IObservable<Unit> UnsubscribeFromTrades(string sub);
        //IObservable<Unit> UnsubscribeFromTrades(IEnumerable<string> subs);
        //IObservable<Unit> UnsubscribeFromTrades(params string[] subs);
    }
}