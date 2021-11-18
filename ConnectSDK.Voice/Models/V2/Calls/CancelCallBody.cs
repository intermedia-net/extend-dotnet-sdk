namespace ConnectSDK.Voice.Models.V2.Calls
{
    using System.Text.Json.Serialization;

    public class CancelCallBody
    {
        [JsonPropertyName("commandId")]
        public string CommandId { get; set; }

        [JsonPropertyName("skipToVoiceMail")]
        public bool SkipToVoiceMail { get; set; }
    }
}
