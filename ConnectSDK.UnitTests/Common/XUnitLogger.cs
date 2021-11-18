namespace ConnectSDK.UnitTests.Common
{
    using System;
    using Microsoft.Extensions.Logging;
    using Xunit.Abstractions;

    public sealed class XUnitLogger : ILogger
    {
        private readonly string name;

        private readonly ITestOutputHelper testOutputHelper;

        public XUnitLogger(string name, ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
            this.name = name;
            this.testOutputHelper = testOutputHelper;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return DisposableStub.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            try
            {
                this.testOutputHelper.WriteLine($"{this.name}|{logLevel}|{formatter(state, exception)}|{exception}");
            }
            catch (InvalidOperationException)
            {
                /* Ignore exception if the application tries to log after the test ends
                 * but before the ITestOutputHelper is detached, e.g. "There is no currently active test."
                 */
            }
        }

        private sealed class DisposableStub : IDisposable
        {
            public static readonly IDisposable Instance = new DisposableStub();

            private DisposableStub()
            {
            }

            public void Dispose()
            {
            }
        }
    }
}
