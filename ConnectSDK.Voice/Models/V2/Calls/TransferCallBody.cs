namespace ConnectSDK.Voice.Models.V2.Calls
{
    using System.Text.Json.Serialization;

    public class TransferCallBody
    {
        public TransferCallBody(string phoneNumber, string commandId = null)
        {
            this.PhoneNumber = phoneNumber;
            this.CommandId = commandId;
        }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("commandId")]
        public string CommandId { get; set; }
    }
}
