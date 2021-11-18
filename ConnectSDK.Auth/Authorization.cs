namespace ConnectSDK.Auth
{
    using System;
    using System.Threading.Tasks;
    using ConnectSDK.Auth.Configurations;
    using ConnectSDK.Auth.Providers;
    using ConnectSDK.Common;

    public static class Authorization
    {
        public static IAuthorizationTokenProvider BuildAuthorizationTokenProvider(ClientCredentialsConfiguration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            return new ClientCredentialsTokenProvider(config);
        }

        public static async Task<IAuthorizationTokenProvider> BuildAuthorizationTokenProvider(AuthorizationCodeConfiguration config, string authCode)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (string.IsNullOrWhiteSpace(authCode)) throw new ArgumentNullException(nameof(authCode));

            return await AuthorizationCodeTokenProvider.BuildTokenProviderAsync(config, authCode);
        }
    }
}