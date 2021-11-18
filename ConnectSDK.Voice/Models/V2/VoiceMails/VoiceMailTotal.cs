namespace ConnectSDK.Voice.Models.V2.VoiceMails
{
    using System.Text.Json.Serialization;

    public class VoiceMailTotal
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
