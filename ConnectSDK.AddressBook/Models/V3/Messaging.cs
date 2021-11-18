namespace ConnectSDK.AddressBook.Models.V3
{
    using System.Text.Json.Serialization;

    public class Messaging
    {
        [JsonPropertyName("jid")]
        public string Jid { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
    }
}
