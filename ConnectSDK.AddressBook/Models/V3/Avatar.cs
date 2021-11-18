namespace ConnectSDK.AddressBook.Models.V3
{
    using System.Text.Json.Serialization;

    public class Avatar
    {
        [JsonPropertyName("avatarId")]
        public string AvatarId { get; set; }

        [JsonPropertyName("contactId")]
        public string ContactId { get; set; }

        [JsonPropertyName("avatar")]
        public byte[] AvatarFile { get; set; }
    }
}
