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
        private readonly ITestOutputHelper _testOutputHelper;

        protected TestBase(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        protected ILogger<T> CreateLogger<T>(LogLevel minLogLevel = LogLevel.Debug) => new XUnitLogger<T>(_testOutputHelper, minLogLevel);

        protected CryptoCompareSocketClient CreateClient(LogLevel minLogLevel = LogLevel.Debug)
        {
            return new CryptoCompareSocketClient(logger: CreateLogger<CryptoCompareSocketClient>(minLogLevel));
        }
    }
}
