namespace ConnectSDK.Voice.Models.V2.Calls
{
    using System.Text.Json.Serialization;

    public class MergeCallBody
    {
        public MergeCallBody(string mergeCallId, string commandId = null)
        {
            this.MergeCallId = mergeCallId;
            this.CommandId = commandId;
        }

        [JsonPropertyName("mergeCallId")]
        public string MergeCallId { get; set; }

        [JsonPropertyName("commandId")]
        public string CommandId { get; set; }
    }
}
