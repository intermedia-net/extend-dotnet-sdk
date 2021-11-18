namespace ConnectSDK.AddressBook.Models.V3
{
    using System.Text.Json.Serialization;

    public class Federation
    {
        [JsonPropertyName("accountGuid")]
        public string AccountGuid { get; set; }
    }
}
