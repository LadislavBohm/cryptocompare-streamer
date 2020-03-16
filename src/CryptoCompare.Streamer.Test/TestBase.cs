using System;
using System.Collections.Generic;
using System.Text;
using CryptoCompare.Streamer.Test.Helpers;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace CryptoCompare.Streamer.Test
{
    public abstract class TestBase
    {
        protected readonly ITestOutputHelper TestOutputHelper;

        protected TestBase(ITestOutputHelper testOutputHelper)
        {
            TestOutputHelper = testOutputHelper;
        }

        protected ILogger<T> CreateLogger<T>(LogLevel minLogLevel = LogLevel.Debug) => new XUnitLogger<T>(TestOutputHelper, minLogLevel);

        protected CryptoCompareSocketClient CreateClient(LogLevel minLogLevel = LogLevel.Debug)
        {
            return new CryptoCompareSocketClient(logger: CreateLogger<CryptoCompareSocketClient>(minLogLevel));
        }
    }
}
