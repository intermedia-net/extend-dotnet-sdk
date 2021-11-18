namespace ConnectSDK.Voice
{
    using System;
    using System.Threading.Tasks;
    using ConnectSDK.Common;
    using ConnectSDK.Voice.CallRecordings;
    using ConnectSDK.Voice.Calls;
    using ConnectSDK.Voice.VoiceMails;
    using ConnectSDK.Voice.VoiceMailsSettings;

    public class VoiceClient : IVoiceClient
    {
        public VoiceClient(Func<string, Task<string>> getToken)
        {
            
            this.VoiceMailsClient = new VoiceMailsClient(getToken);
            this.VoiceMailsSettingsClient = new VoiceMailsSettingsClient(getToken);
            this.CallsClient = new CallsClient(getToken);
            this.CallRecordingsClient = new CallRecordingsClient(getToken);
        }
        
        public VoiceClient(Func<string, Task<string>> getToken, ConnectSdkConfig config)
        {
            if(config == null){ throw new ArgumentNullException(nameof(config)); }
            
            this.VoiceMailsClient = new VoiceMailsClient(getToken, config);
            this.VoiceMailsSettingsClient = new VoiceMailsSettingsClient(getToken, config);
            this.CallsClient = new CallsClient(getToken, config);
            this.CallRecordingsClient = new CallRecordingsClient(getToken, config);
        }

        public IVoiceMailsClient VoiceMailsClient { get; set; }

        public IVoiceMailsSettingsClient VoiceMailsSettingsClient { get; set; }

        public ICallsClient CallsClient { get; set; }

        public ICallRecordingsClient CallRecordingsClient { get; set; }
    }
}
