namespace ConnectSDK.AddressBook.Models.V3
{
    using System.Text.Json.Serialization;

    public class PhoneNumbers
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("internationalFormatNumber")]
        public string InternationalFormatNumber { get; set; }

        [JsonPropertyName("codes")]
        public string[] Codes { get; set; }
    }
}
