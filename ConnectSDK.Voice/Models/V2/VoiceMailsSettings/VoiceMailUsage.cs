namespace ConnectSDK.Voice.Models.V2.VoiceMailsSettings
{
    using System.Text.Json.Serialization;

    public class VoiceMailUsage
    {
        [JsonPropertyName("spaceUsedPercentage")]
        public int SpaceUsedPercentage { get; set; }
    }
}
