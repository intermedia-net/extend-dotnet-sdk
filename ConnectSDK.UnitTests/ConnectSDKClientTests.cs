namespace ConnectSDK.UnitTests
{
    using Xunit.Abstractions;
    using ConnectSDK.UnitTests.Common;
    
    public class UniteClientTests : TestBase
    {
        // Subject of testing.
#pragma warning disable IDE0052 // Remove unread private members
        // ReSharper disable once NotAccessedField.Local
        private readonly UnifiedClient sut;
#pragma warning restore IDE0052 // Remove unread private members

        public UniteClientTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.sut = new UnifiedClient(tokenProvider);
        }
    }
}
