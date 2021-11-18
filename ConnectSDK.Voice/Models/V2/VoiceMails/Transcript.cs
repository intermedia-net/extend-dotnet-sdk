namespace ConnectSDK.Voice.Models.V2.VoiceMails
{
    using System.Text.Json.Serialization;

    public class Transcript
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
