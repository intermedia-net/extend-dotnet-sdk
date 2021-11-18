namespace ConnectSDK.UnitTests.Common
{
    using System;
    using System.Collections.Concurrent;
    using Microsoft.Extensions.Logging;
    using Xunit.Abstractions;

    public sealed class XUnitLoggingProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, ILogger> loggers =
            new ConcurrentDictionary<string, ILogger>(StringComparer.OrdinalIgnoreCase);

        private readonly ITestOutputHelper testOutputHelper;

        private bool objectDisposed;

        public XUnitLoggingProvider(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (this.objectDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            return this.loggers.GetOrAdd(categoryName ?? string.Empty, key => new XUnitLogger(key, this.testOutputHelper));
        }

        public void Dispose()
        {
            if (!this.objectDisposed)
            {
                this.objectDisposed = true;
            }
        }
    }
}
