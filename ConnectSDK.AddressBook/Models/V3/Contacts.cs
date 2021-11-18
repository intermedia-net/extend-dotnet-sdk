namespace ConnectSDK.AddressBook.Models.V3
{
    using System.Text.Json.Serialization;

    public class Contacts
    {
        [JsonPropertyName("results")]
        public Contact[] Results { get; set; }

        [JsonPropertyName("nextPageOffsetToken")]
        public string NextPageOffsetToken { get; set; }
    }
}
