namespace ConnectSDK.AddressBook.Models.V3
{
    using System.Text.Json.Serialization;

    public class Pbx
    {
        [JsonPropertyName("pbxId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public int PbxId { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("extension")]
        public string Extension { get; set; }
    }
}
