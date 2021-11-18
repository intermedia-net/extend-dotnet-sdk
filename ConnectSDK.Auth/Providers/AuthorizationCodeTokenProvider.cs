namespace ConnectSDK.Auth.Providers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ConnectSDK.Auth.Configurations;
    using ConnectSDK.Auth.Exceptions;
    using ConnectSDK.Common;
    using IdentityModel.Client;

    public class AuthorizationCodeTokenProvider : IAuthorizationTokenProvider
    {
        private AuthorizationCodeConfiguration Config { get; }

        private string Token { get; set; }

        private string RefreshToken { get; set; }

        public static async Task<AuthorizationCodeTokenProvider> BuildTokenProviderAsync(AuthorizationCodeConfiguration config, string authCode)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (string.IsNullOrWhiteSpace(authCode)) throw new ArgumentNullException(nameof(authCode));

            var tokenProvider = new AuthorizationCodeTokenProvider(config);
            await tokenProvider.SetTokens(authCode);

            return tokenProvider;
        }
        private AuthorizationCodeTokenProvider(AuthorizationCodeConfiguration config)
        {
            this.Config = config;
        }

        public async Task<string> GetToken(string scope)
        {
            if (string.IsNullOrWhiteSpace(scope)) throw new ArgumentNullException(nameof(scope));

            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(this.Token);

            if (!IsScopeAvailable(decodedToken, scope))
            {
                throw new ScopeException(scope);
            }

            if (IsTokenExpired(decodedToken))
            {
                await this.RenewToken();
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

        private async Task RenewToken()
        {
            var client = new HttpClient();

            var response = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = $"{this.Config.AuthBaseUrl}/connect/token",

                ClientId = this.Config.ClientId,
                ClientSecret = this.Config.ClientSecret,

                RefreshToken = this.RefreshToken,
                Parameters =
                {
                    { "acr_values", $"deviceId:{this.Config.DeviceId}"}
                }
            });

            this.Token = response.AccessToken;
            this.RefreshToken = response.RefreshToken;
        }

        private async Task SetTokens(string authCode)
        {
            var client = new HttpClient();

            var response = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest
            {
                Address = $"{this.Config.AuthBaseUrl}/connect/token",

                ClientId = this.Config.ClientId,
                ClientSecret = this.Config.ClientSecret,

                Code = authCode,
                RedirectUri = this.Config.RedirectUrl,
                Parameters =
                {
                    { "acr_values", $"deviceId:{this.Config.DeviceId}"}
                }
            });

            this.Token = response.AccessToken;
            this.RefreshToken = response.RefreshToken;
        }
    }
}
