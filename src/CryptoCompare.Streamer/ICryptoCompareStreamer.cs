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
        IObservable<TradeEvent> OnTrade { get; }
        IObservable<VolumeEvent> OnVolume { get; }

        Task StartAsync();
        Task StopAsync();

        IObservable<Unit> Subscribe<TSubscription>(TSubscription subscription) where TSubscription : ICryptoCompareSubscription;
        IObservable<Unit> Subscribe<TSubscription>(IEnumerable<TSubscription> subscriptions) where TSubscription : ICryptoCompareSubscription;
        IObservable<Unit> Subscribe<TSubscription>(params TSubscription[] subscriptions) where TSubscription : ICryptoCompareSubscription;
        

        IObservable<Unit> Unsubscribe<TSubscription>(TSubscription subscription) where TSubscription : ICryptoCompareSubscription;
        IObservable<Unit> Unsubscribe<TSubscription>(IEnumerable<TSubscription> subscriptions) where TSubscription : ICryptoCompareSubscription;
        IObservable<Unit> Unsubscribe<TSubscription>(params TSubscription[] subscriptions) where TSubscription : ICryptoCompareSubscription;

    }
}