namespace ConnectSDK.Voice
{
    using ConnectSDK.Voice.CallRecordings;
    using ConnectSDK.Voice.Calls;
    using ConnectSDK.Voice.VoiceMails;
    using ConnectSDK.Voice.VoiceMailsSettings;

    public interface IVoiceClient
    {
        IVoiceMailsClient VoiceMailsClient { get; set; }

        IVoiceMailsSettingsClient VoiceMailsSettingsClient { get; set; }

        ICallsClient CallsClient { get; set; }

        ICallRecordingsClient CallRecordingsClient { get; set; }
    }
}
