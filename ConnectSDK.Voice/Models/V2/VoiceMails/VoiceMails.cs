namespace ConnectSDK.Voice.Models.V2.VoiceMails
{
    using System.Text.Json.Serialization;

    public class VoiceMails
    {
        [JsonPropertyName("records")]
        public VoiceMail[] Records { get; set; }
    }
}
