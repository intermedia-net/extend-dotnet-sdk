namespace ConnectSDK.Auth
{
    using System;
    using System.Threading.Tasks;
    using ConnectSDK.Common;

    public class AuthorizationRouterTokenProvider : IAuthorizationTokenProvider
    {
        private readonly IAuthorizationTokenProvider clientCredentialTokenProvider;
        private readonly IAuthorizationTokenProvider authorizationCodeTokenProvider;

        public AuthorizationRouterTokenProvider(
            IAuthorizationTokenProvider clientCredentialTokenProvider,
            IAuthorizationTokenProvider authorizationCodeTokenProvider
        )
        {
            this.clientCredentialTokenProvider = 
                clientCredentialTokenProvider ?? throw new ArgumentNullException(nameof(clientCredentialTokenProvider));
            this.authorizationCodeTokenProvider =
                authorizationCodeTokenProvider ?? throw new ArgumentNullException(nameof(authorizationCodeTokenProvider));
        }

        public async Task<string> GetToken(string scope)
        {
            if (scope.StartsWith("api.service"))
            {
                return await this.clientCredentialTokenProvider.GetToken(scope);
            }
            return await this.authorizationCodeTokenProvider.GetToken(scope);
        }
    }
}
