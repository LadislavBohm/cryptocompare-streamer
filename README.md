![](cryptocompare-logo.jpg)

# CryptoCompare real-time API client written in C&#35;

High-performance C# client with flexible and easy to use API to connect to CryptoCompare streaming (real-time) API. No API key needed. Built on top of .NET Standard 2.1 and [Rx.Net](https://github.com/dotnet/reactive).

Currently only **trade** stream is available, **current** stream will be implemented shortly.

## Installation

```
PM> Install-Package CryptoCompare.Streamer
```

## Features

- Fully async and non-blocking API
- Subscribe/Unsubscribe to following streams: Trade, Current, Volume
- Flexible event handling thanks to [Rx.Net](https://github.com/dotnet/reactive) (see examples)

## Examples

### Basic subscription

```csharp
//don't forget to Dispose socket once work is done!
//optionally supply different URL or ILogger instance (default is NullLogger)
using var client = new CryptoCompareSocketClient();

//handle trades by subscription to OnTrade (same for OnCurrent and OnVolume)
var subscription = client.OnTrade.Subscribe(trade => Console.WriteLine(trade));

//start socket (needs to be called before subscribing)
await client.StartAsync();

//TRADE

//use 'sub' format to subscribe
client.Subscribe(new TradeSubscription("0~Bitfinex~BTC~USD"));
//or specify exchange currency pair
client.Subscribe(new TradeSubscription("Bitfinex", "BTC", "USD"));
//or multiple trade currency pairs
var subscriptions = new[] {"0~Bitfinex~BTC~USD", "0~Bittrex~BTC~USD", "0~Bitstamp~BTC~USD"};
client.Subscribe(subscriptions.Select(s => new TradeSubscription(s)));

//VOLUME

client.Subscribe(new VolumeSubscription("BTC"));
//or multiple volume currencies
client.Subscribe(new[] {"BTC", "ETC"}.Select(c => new VolumeSubscription(c)));

//CURRENT

client.Subscribe(new CurrentSubscription("Bitfinex", "BTC", "USD"));
//or multiple exchange currency pairs at once
var subscriptions = new[] { "0~Bitfinex~BTC~USD", "0~Bittrex~BTC~USD", "0~Bitstamp~BTC~USD" };
client.Subscribe(subscriptions.Select(s => new CurrentSubscription(s)));
```

### Unsubscription

```csharp
var client = new CryptoCompareSocketClient();
var subscription = client.OnTrade.Subscribe(trade => Console.WriteLine(trade));
await client.StartAsync();
var subscription = new TradeSubscription("0~Bitfinex~BTC~USD")
client.Subscribe(subscription);

//collect for 5 seconds
await Task.Delay(5000);
//dispose subscription basically "removes" OnTrade handler
//does NOT stop socket from collecting the data!
subscription.Dispose();

//socket actually stops receiving trades once message is processed
//if you need to stop receiving immediatelly, remove the handler as shown above
client.Unsubscribe(subscription);
```

### Stream processing

All streams are based on [Rx.Net](https://github.com/dotnet/reactive) but some simple examples are shown here.

```csharp
//filter only BUY trades and do not receive more than one every 100ms
var subscription = client.OnTrade
    .Where(t => t.Type == TradeType.Buy)
    .Throttle(TimeSpan.FromMilliseconds(100))
    .Subscribe(trade => Console.WriteLine(trade));

//buffer trades by 10 and process them in batch
var subscription = client.OnTrade
    .Buffer(10)
    .Subscribe(trades => ProcessBatch(trades));

//buffer trades for the last 500ms and process them in batch
var subscription = client.OnTrade
    .Buffer(TimeSpan.FromMilliseconds(500))
    .Subscribe(trades => ProcessBatch(trades));

//monitor stream and timeout if no trade is received within 10 seconds
var subscription = client.OnTrade
    .Timeout(TimeSpan.FromSeconds(10))
    .Subscribe(trade => Console.WriteLine(trade), (ex) =>
    {
        if (ex is TimeoutException)
        {
            //reconnect?
        }
    });
```

## To-do

- CCCAGG stream (could use some help in parsing this one)
- Socket state monitoring
- Auto-reconnection
- Code documentation
- More unit/integration tests
