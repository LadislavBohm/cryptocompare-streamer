using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using CryptoCompare.Streamer.Constants;
using CryptoCompare.Streamer.Model;
using CryptoCompare.Streamer.Model.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Socket.Io.Client.Core;
using Socket.Io.Client.Core.Model.SocketEvent;

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

        public IObservable<Unit> SubscribeToTrades(string exchange, string fromCurrency, string toCurrency) =>
            SubscribeToTrades(CreateSub(exchange, fromCurrency, toCurrency));

        public IObservable<Unit> SubscribeToTrades(string sub)
        {
            if (sub == null) throw new ArgumentNullException(nameof(sub));
            return SubscribeToTrades(new[] {sub});
        }

        public IObservable<Unit> SubscribeToTrades(params string[] subs)
        {
            if (subs.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(subs));
            return SubscribeToTrades((IEnumerable<string>)subs);
        }

        public IObservable<Unit> SubscribeToTrades(IEnumerable<string> subs)
        {
            if (subs == null) throw new ArgumentNullException(nameof(subs));
            return _client.Emit("SubAdd", new SubAddMessage(subs)).Select(e => Unit.Default);
        }

        public IObservable<Unit> UnsubscribeFromTrades(string exchange, string fromCurrency, string toCurrency) =>
            UnsubscribeFromTrades(CreateSub(exchange, fromCurrency, toCurrency));

        public IObservable<Unit> UnsubscribeFromTrades(string sub)
        {
            if (sub == null) throw new ArgumentNullException(nameof(sub));
            return UnsubscribeFromTrades(new[] {sub});
        }

        public IObservable<Unit> UnsubscribeFromTrades(params string[] subs)
        {
            if (subs == null) throw new ArgumentNullException(nameof(subs));
            if (subs.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(subs));
            return UnsubscribeFromTrades((IEnumerable<string>) subs);
        }

        public IObservable<Unit> UnsubscribeFromTrades(IEnumerable<string> subs)
        {
            if (subs == null) throw new ArgumentNullException(nameof(subs));
            return _client.Emit("SubRemove", new SubRemoveMessage(subs)).Select(e => Unit.Default);
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