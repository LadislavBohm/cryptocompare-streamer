﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using CryptoCompare.Streamer.Constants;
using CryptoCompare.Streamer.Model;
using CryptoCompare.Streamer.Model.Messages;
using CryptoCompare.Streamer.Model.Subscriptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Socket.Io.Client.Core;
using Socket.Io.Client.Core.Json;
using Socket.Io.Client.Core.Model;
using Socket.Io.Client.Core.Model.SocketEvent;
using Utf8Json.Resolvers;

namespace CryptoCompare.Streamer
{
    public class CryptoCompareSocketClient : ICryptoCompareStreamer, IDisposable
    {
        private readonly ILogger<CryptoCompareSocketClient> _logger;
        private readonly Uri _url;
        private readonly ISocketIoClient _client;

        private readonly ISubject<Trade> _tradeSubject = new Subject<Trade>();

        public CryptoCompareSocketClient(string url = "https://streamer.cryptocompare.com", ILogger<CryptoCompareSocketClient> logger = null)
        {
            _logger = logger ?? NullLogger<CryptoCompareSocketClient>.Instance;
            _url = new Uri(url);
            _client = new SocketIoClient();
            _client.On("m").Subscribe(OnMessage);
        }

        public IObservable<Trade> OnTrade => _tradeSubject.AsObservable();

        public Task StartAsync() => _client.OpenAsync(_url);

        public Task StopAsync() => _client.CloseAsync();

        public IObservable<Unit> Subscribe<TSubscription>(TSubscription subscription) where TSubscription : ICryptoCompareSubscription
        {
            if (subscription == null) throw new ArgumentNullException(nameof(subscription));
            return Subscribe(new[] {subscription});
        }

        public IObservable<Unit> Subscribe<TSubscription>(params TSubscription[] subscriptions) where TSubscription : ICryptoCompareSubscription
        {
            if (subscriptions == null) throw new ArgumentNullException(nameof(subscriptions));
            if (subscriptions.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(subscriptions));
            return Subscribe((IEnumerable<TSubscription>) subscriptions);
        }

        public IObservable<Unit> Subscribe<TSubscription>(IEnumerable<TSubscription> subscriptions) where TSubscription : ICryptoCompareSubscription
        {
            if (subscriptions == null) throw new ArgumentNullException(nameof(subscriptions));
            var subMessage = new SubAddMessage(subscriptions.Select(s => s.Format()));
            return _client.Emit("SubAdd", subMessage).Select(e => Unit.Default);
        }

        public IObservable<Unit> Unsubscribe<TSubscription>(TSubscription subscription) where TSubscription : ICryptoCompareSubscription
        {
            if (subscription == null) throw new ArgumentNullException(nameof(subscription));
            return Subscribe(new[] {subscription});
        }

        public IObservable<Unit> Unsubscribe<TSubscription>(params TSubscription[] subscriptions) where TSubscription : ICryptoCompareSubscription
        {
            if (subscriptions == null) throw new ArgumentNullException(nameof(subscriptions));
            if (subscriptions.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(subscriptions));
            return Subscribe((IEnumerable<TSubscription>) subscriptions);
        }


        public IObservable<Unit> Unsubscribe<TSubscription>(IEnumerable<TSubscription> subscriptions) where TSubscription : ICryptoCompareSubscription
        {
            if (subscriptions == null) throw new ArgumentNullException(nameof(subscriptions));
            var subMessage = new SubRemoveMessage(subscriptions.Select(s => s.Format()));
            return _client.Emit("SubRemove", subMessage).Select(a => Unit.Default);
        }

        
        private void OnMessage(EventMessageEvent e)
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                    _logger.LogDebug($"Received message: {e.FirstData}");

                var data = e.FirstData;
                if (!string.IsNullOrEmpty(data) && data.StartsWith('0'))
                {
                    _tradeSubject.OnNext(CCC.Trade.Unpack(data));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while processing message. Message data: {e.FirstData}");
            }
        }

        private string CreateSub(string exchange, string fromCurrency, string toCurrency) => $"~0~{exchange}~{fromCurrency}~{toCurrency}";

        public void Dispose()
        {
            _client?.Dispose();
            _tradeSubject.OnCompleted();
        }
    }
}