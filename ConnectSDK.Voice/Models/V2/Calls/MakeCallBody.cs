namespace ConnectSDK.Voice.Models.V2.Calls
{
    using System;
    using System.Text.Json.Serialization;

    public class MakeCallBody
    {
        public MakeCallBody(
            string deviceId,
            string phoneNumber,
            string mode = "placeCall",
            string callId = null,
            string commandId = null)
        {
            this.DeviceId = deviceId ?? throw new ArgumentNullException(nameof(deviceId));
            this.Mode = mode  ??  throw new ArgumentNullException(nameof(mode));
            this.PhoneNumber = phoneNumber;
            this.CallId = callId;
            this.CommandId = commandId;
        }

        [JsonPropertyName("deviceId")]
        public string DeviceId { get; set; }

        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("callId")]
        public string CallId { get; set; }

        [JsonPropertyName("commandId")]
        public string CommandId { get; set; }
    }
}
