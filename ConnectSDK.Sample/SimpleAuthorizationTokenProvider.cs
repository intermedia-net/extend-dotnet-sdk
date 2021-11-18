namespace ConnectSDK.Sample
{
    using System.Threading.Tasks;
    using ConnectSDK.Common;

    public class SimpleAuthorizationTokenProvider : IAuthorizationTokenProvider
    {
        public SimpleAuthorizationTokenProvider(string token)
        {
            this.Token = token;
        }

        public string Token { get; set; }

        public Task<string> GetToken(string scope)
        {
            return Task.FromResult(this.Token);
        }
    }
}
