namespace ConnectSDK.AddressBook.Models.V3
{
    using System.Text.Json.Serialization;

    public class Contact
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("legacyId")]
        public string LegacyId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("managerId")]
        public string ManagerId { get; set; }

        [JsonPropertyName("avatarId")]
        public string AvatarId { get; set; }

        [JsonPropertyName("phoneNumbers")]
        public PhoneNumbers[] PhoneNumbers { get; set; }

        [JsonPropertyName("pbx")]
        public Pbx Pbx { get; set; }

        [JsonPropertyName("messaging")]
        public Messaging Messaging { get; set; }

        [JsonPropertyName("federation")]
        public Federation Federation { get; set; }
    }
}
