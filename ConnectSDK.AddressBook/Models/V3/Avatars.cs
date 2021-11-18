namespace ConnectSDK.AddressBook.Models.V3
{
    using System.Text.Json.Serialization;

    public class Avatars
    {
        [JsonPropertyName("results")]
        public Avatar[] Results { get; set; }
    }
}
