namespace ConnectSDK.Voice.Models.V2.Calls
{
    using System.Text.Json.Serialization;

    public class DevicesResponse
    {
        [JsonPropertyName("clickToCallDevices")]
        public Device[] ClickToCallDevices { get; set; }
    }
}
