namespace ConnectSDK.Voice.Models.V2.VoiceMails
{
    using System.Text.Json.Serialization;

    public class SelectedVoiceMailsBody
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("ids")]
        public int[] Ids { get; set; }
    }
}
