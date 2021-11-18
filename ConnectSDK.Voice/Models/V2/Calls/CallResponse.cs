namespace ConnectSDK.Voice.Models.V2.Calls
{
    using System.Text.Json.Serialization;

    public class CallResponse
    {
        [JsonPropertyName("commandId")]
        public string CommandId { get; set; }
    }
}
