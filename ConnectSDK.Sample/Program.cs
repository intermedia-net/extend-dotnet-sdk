namespace ConnectSDK.Sample
{
    using System;
    using System.Threading.Tasks;
    using ConnectSDK.Common.Exceptions;
    using ConnectSDK.Voice.Models.V2.Calls;
    using Newtonsoft.Json;

    internal class Program
    {
        private static async Task Main()
        {
            var tokenProvider = new SimpleAuthorizationTokenProvider(Guid.NewGuid().ToString("N"));
            var uniteClient = new UnifiedClient(tokenProvider);
            var callsClient = uniteClient.VoiceClient.CallsClient;

            try
            {
                Console.WriteLine("Receiving devices...");
                var devices = await callsClient.GetDevices();

                Console.WriteLine("Pick device (Input digit):");
                for (var i = 0; i < devices.ClickToCallDevices.Length; i++)
                {
                    Console.WriteLine($"[{i}] {devices.ClickToCallDevices[i].Name}");
                }

                var deviceIndex = int.Parse(Console.ReadLine() ?? "0");
                var deviceId = devices.ClickToCallDevices[deviceIndex].Id;

                Console.WriteLine("Creating subscription...");
                var subscription = await callsClient.CreateSubscription(HandleEvent);

                Console.WriteLine("Trying to make call...");
                var call = await callsClient.MakeCall(new MakeCallBody(deviceId, "5551000"));
                Console.WriteLine($"Call id is {call.CallId}");

                await Task.Delay(TimeSpan.FromSeconds(5));
                Console.WriteLine("Going to delete subscription...");
                await callsClient.DeleteSubscription(subscription.Id);
                await Task.Delay(TimeSpan.FromSeconds(20));
            }
            catch (ConnectSdkBaseException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void HandleEvent(CallEvent @event)
        {
            Console.WriteLine($"Event received: {JsonConvert.SerializeObject(@event, Formatting.Indented)}");
        }
    }
}
