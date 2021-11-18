namespace ConnectSDK.Voice.VoiceMailsSettings
{
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using Common;
    using ConnectSDK.Voice.Models.V2.VoiceMailsSettings;

    internal class VoiceMailsSettingsClient : IVoiceMailsSettingsClient
    {
        private readonly IRequestFactory requestFactory = new RequestFactory();
        private readonly Func<string, Task<string>> getToken;

        public VoiceMailsSettingsClient(Func<string, Task<string>> getToken)
        {
            this.getToken = getToken;
        }

        public VoiceMailsSettingsClient(Func<string, Task<string>> getToken, ConnectSdkConfig config)
        {
            this.getToken = getToken;
            this.requestFactory.SetUserAgent(config.AppName, config.AppVersion);
        }

        public async Task<byte[]> GetGreetingContent(Format format, Custom custom)
        {
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = "/voice/v2/users/_me/voicemail/greeting";
            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            switch (format)
            {
                case Format.Mp3:
                    urlParams.Add("format", "mp3");
                    break;
                case Format.Ogg:
                    urlParams.Add("format", "ogg");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), "Not supported content format");
            }

            urlParams.Add("custom", ((int)custom).ToString());

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.GetAsync<byte[]>(fullUrl);
        }

        public async Task UploadGreetingContent(byte[] bytearray)
        {
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = "/voice/v2/users/_me/voicemail/greeting";

            await this.requestFactory.PostAsync(url, bytearray);
        }

        public async Task ResetGreetingContent()
        {
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = "/voice/v2/users/_me/voicemail/greeting";

            await this.requestFactory.DeleteAsync(url);
        }

        public async Task<UserSettings> GetUserSettings()
        {
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = "/voice/v2/users/_me/voicemail/settings";

            return await this.requestFactory.GetAsync<UserSettings>(url);
        }

        public async Task UpdateUserSettings(UserSettings userSettings)
        {
            if(userSettings == null){ throw new ArgumentNullException(nameof(userSettings)); }
            if(userSettings.Pin.Length != 4){ throw new ArgumentException("Not valid value. Pin need to have four digits", nameof(userSettings.Pin)); }
            
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = "/voice/v2/users/_me/voicemail/settings";

            await this.requestFactory.PostAsync(url, userSettings);
        }

        public async Task<VoiceMailUsage> GetVoicemailUsage()
        {
            var token = await this.getToken(Constants.VoiceScopes.ApiUserVoiceVoicemails);
            this.SetToken(token);

            var url = "/voice/v2/users/_me/voicemail/usage";

            return await this.requestFactory.GetAsync<VoiceMailUsage>(url);
        }

        private void SetToken(string token)
        {
            this.requestFactory.SetToken(token);
        }
    }
}
