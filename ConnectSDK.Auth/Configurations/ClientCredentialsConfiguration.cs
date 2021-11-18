namespace ConnectSDK.Auth.Configurations
{
    using System;
    using ConnectSDK.Common;

    public class ClientCredentialsConfiguration
    {
        public string AuthBaseUrl { get; }

        public string ClientId { get; }

        public string ClientSecret { get; }

        public ClientCredentialsConfiguration(
            string authBaseUrl,
            string clientId,
            string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(authBaseUrl)) throw new ArgumentNullException(nameof(authBaseUrl));
            if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException(nameof(clientId));
            if (string.IsNullOrWhiteSpace(clientSecret)) throw new ArgumentNullException(nameof(clientSecret));

            this.AuthBaseUrl = authBaseUrl;
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
        }
    }
}
