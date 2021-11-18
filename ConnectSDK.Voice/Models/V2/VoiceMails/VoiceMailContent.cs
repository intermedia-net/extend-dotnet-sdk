namespace ConnectSDK.Voice.Models.V2.VoiceMails
{
    using System.Text.Json.Serialization;

    public class VoiceMailContent
    {
        [JsonPropertyName("byte")]
        public byte Byte { get; set; }
    }
}
