namespace ConnectSDK.Auth.Configurations
{
    using System;
    using ConnectSDK.Common;

    public class AuthorizationCodeConfiguration
    {
        public string AuthBaseUrl { get; }

        public string ClientId { get; }

        public string ClientSecret { get; }

        public string RedirectUrl { get; }

        public string DeviceId { get; }

        public AuthorizationCodeConfiguration(
            string authBaseUrl,
            string clientId,
            string clientSecret,
            string redirectUrl,
            string deviceId)
        {
            if (string.IsNullOrWhiteSpace(authBaseUrl)) throw new ArgumentNullException(nameof(authBaseUrl));
            if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException(nameof(clientId));
            if (string.IsNullOrWhiteSpace(clientSecret)) throw new ArgumentNullException(nameof(clientSecret));
            if (string.IsNullOrWhiteSpace(redirectUrl)) throw new ArgumentNullException(nameof(redirectUrl));
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentNullException(nameof(deviceId));

            this.AuthBaseUrl = authBaseUrl;
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.RedirectUrl = redirectUrl;
            this.DeviceId = deviceId;
        }
    }
}
