namespace ConnectSDK.Auth.Providers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ConnectSDK.Auth.Configurations;
    using ConnectSDK.Common;
    using Duende.IdentityModel.Client;

    public class ClientCredentialsTokenProvider : IAuthorizationTokenProvider
    {
        private ClientCredentialsConfiguration Config { get; }

        private string Token { get; set; }

        public ClientCredentialsTokenProvider(ClientCredentialsConfiguration config)
        {
            this.Config = config ?? throw new ArgumentNullException(nameof(config));
            if (config.ClientSecret == null) throw new ArgumentNullException(nameof(config.ClientSecret));
        }

        public async Task<string> GetToken(string scope)
        {
            if (string.IsNullOrWhiteSpace(scope)) throw new ArgumentNullException(nameof(scope));

            if (this.Token != null)
            {
                var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(this.Token);

                if (IsTokenExpired(decodedToken) || !IsScopeAvailable(decodedToken, scope))
                {
                    await this.SetToken(scope);
                }
            }
            else
            {
                await this.SetToken(scope);
            }

            return this.Token;
        }

        private static bool IsTokenExpired(JwtSecurityToken decodedToken)
        {
            var tokenExp = decodedToken.ValidTo;
            return DateTime.Compare(DateTime.UtcNow, tokenExp) >= 0;
        }

        private static bool IsScopeAvailable(JwtSecurityToken decodedToken, string scope)
        {
            var encodeScopes = decodedToken.Claims.Where(claim => claim.Type == "scope");
            return encodeScopes.Any(claim => claim.Value == scope);
        }

        private async Task SetToken(string scope)
        {
            var client = new HttpClient();
            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = $"{this.Config.AuthBaseUrl}/connect/token",
                ClientId = this.Config.ClientId,
                ClientSecret = this.Config.ClientSecret,
                Scope = scope
            });

            this.Token = response.AccessToken;
        }
    }
}
