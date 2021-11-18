namespace ConnectSDK
{
    using System;
    using System.Threading.Tasks;
    using ConnectSDK.AddressBook;
    using ConnectSDK.Analytics;
    using ConnectSDK.Common;
    using ConnectSDK.Meetings;
    using ConnectSDK.Voice;

    public class UnifiedClient
    {
        public UnifiedClient(IAuthorizationTokenProvider tokenProvider)
            : this(tokenProvider.GetToken)
        {
        }

        public UnifiedClient(IAuthorizationTokenProvider tokenProvider, ConnectSdkConfig config)
            : this(tokenProvider.GetToken, config)
        {
        }

        public UnifiedClient(Func<string, Task<string>> getToken)
        {
            if (getToken == null) throw new ArgumentNullException(nameof(getToken));

            this.VoiceClient = new VoiceClient(getToken);
            this.AnalyticsClient = new AnalyticsClient(getToken);
            this.AddressBookClient = new AddressBookClient(getToken);
            this.MeetingsClient = new MeetingsClient(getToken);
        }

        public UnifiedClient(Func<string, Task<string>> getToken, ConnectSdkConfig config)
        {
            if (getToken == null) throw new ArgumentNullException(nameof(getToken));
            if (config == null) throw new ArgumentNullException(nameof(config));

            this.VoiceClient = new VoiceClient(getToken, config);
            this.AnalyticsClient = new AnalyticsClient(getToken, config);
            this.AddressBookClient = new AddressBookClient(getToken, config);
            this.MeetingsClient = new MeetingsClient(getToken, config);
        }

        public IVoiceClient VoiceClient { get; }

        public IAnalyticsClient AnalyticsClient { get; }

        public IAddressBookClient AddressBookClient { get; }

        public IMeetingsClient MeetingsClient { get; }
    }
}
