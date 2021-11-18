namespace ConnectSDK.Voice.Models.V2.VoiceMailsSettings
{
    using System.Text.Json.Serialization;

    public class UserSettings
    {
        [JsonPropertyName("pin")]
        public string Pin { get; set; }

        [JsonPropertyName("hasCustomGreeting")]
        public bool HasCustomGreeting { get; set; }

        [JsonPropertyName("isTranscriptionPermitted")]
        public bool IsTranscriptionPermitted { get; set; }

        [JsonPropertyName("enableTranscription")]
        public bool EnableTranscription { get; set; }

        [JsonPropertyName("receiveEmailNotifications")]
        public bool ReceiveEmailNotifications { get; set; }

        [JsonPropertyName("emails")]
        public string[] Emails { get; set; }

        [JsonPropertyName("includeVoiceMail")]
        public bool IncludeVoiceMail { get; set; }
    }
}
