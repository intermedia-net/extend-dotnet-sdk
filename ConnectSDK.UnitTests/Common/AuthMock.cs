namespace ConnectSDK.UnitTests.Common
{
    using System;
    using System.Threading.Tasks;
    using ConnectSDK.Common;

    public class AuthMock : IAuthorizationTokenProvider
    {
        public AuthMock(string token = null)
        {
            this.Token = token ?? Guid.NewGuid().ToString("N");
        }

        public string Token { get; set; }

        public Task<string> GetToken(string scope)
        {
            return Task.FromResult(this.Token);
        }
    }
}
