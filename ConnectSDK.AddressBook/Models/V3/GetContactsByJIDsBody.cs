namespace ConnectSDK.AddressBook.Models.V3
{
    using System.Text.Json.Serialization;

    public class GetContactsByJIDsBody
    {
        [JsonPropertyName("jids")]
        public string[] JIDs { get; set; }
    }
}
