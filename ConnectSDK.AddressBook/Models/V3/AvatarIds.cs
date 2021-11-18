namespace ConnectSDK.AddressBook.Models.V3
{
    using System.Text.Json.Serialization;

    public class AvatarIdsBody
    {
        [JsonPropertyName("avatarIds")]
        public string[] AvatarIds { get; set; }
    }
}
