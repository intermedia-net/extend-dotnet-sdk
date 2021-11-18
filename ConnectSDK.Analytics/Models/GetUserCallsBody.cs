namespace ConnectSDK.Analytics.Models
{
    using System.Text.Json.Serialization;

    public class GetUserCallsBody
    {
        public GetUserCallsBody(int[] userIds = null)
        {
            this.UserIds = userIds;
        }

        [JsonPropertyName("userIds")]
        public int[] UserIds { get; set; }
    }
}
