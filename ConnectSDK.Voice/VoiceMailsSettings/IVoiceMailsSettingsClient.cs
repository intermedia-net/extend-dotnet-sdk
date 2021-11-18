namespace ConnectSDK.Voice.VoiceMailsSettings
{
    using System.Threading.Tasks;
    using ConnectSDK.Voice.Models.V2.VoiceMailsSettings;

    public interface IVoiceMailsSettingsClient
    {
        Task<byte[]> GetGreetingContent(Format format, Custom custom);

        Task UploadGreetingContent(byte[] bytearray);

        Task ResetGreetingContent();

        Task<UserSettings> GetUserSettings();

        Task UpdateUserSettings(UserSettings userSettings);

        Task<VoiceMailUsage> GetVoicemailUsage();
    }
}
