namespace ConnectSDK.Voice.Models.V2.VoiceMails
{
    using System.Text.Json.Serialization;

    public class SelectedIdsBody
    {
        [JsonPropertyName("ids")]
        public int[] Ids { get; set; }
    }
}
